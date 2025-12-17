using FeriaAgricultor.Core.Data;
using FeriaAgricultor.Core.Models;
using FeriaAgricultor.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeriaAgricultor.Core.Controllers
{
    public class ControladorCarrito
    {
        private readonly IRepositorio<Producto> _repoProductos;
        private readonly IRepositorio<Orden> _repoOrdenes;
        private readonly IServicioEmail _servicioEmail;
        private readonly ServicioSesion _sesion; // <--- Nueva dependencia

        // Lista temporal de items en memoria (Funciona porque el servicio es SCOPED)
        private List<DetalleOrden> _carrito = new List<DetalleOrden>();

        // Constructor actualizado: Recibe ServicioSesion
        public ControladorCarrito(
            IRepositorio<Producto> repoProductos,
            IRepositorio<Orden> repoOrdenes,
            IServicioEmail servicioEmail,
            ServicioSesion sesion) // <--- Inyección aquí
        {
            _repoProductos = repoProductos;
            _repoOrdenes = repoOrdenes;
            _servicioEmail = servicioEmail;
            _sesion = sesion;
        }

        public void AgregarAlCarrito(int idProducto, int cantidad)
        {
            var producto = _repoProductos.ObtenerPorId(idProducto);
            if (producto == null) throw new Exception("Producto no encontrado");

            if (producto.CantidadStock < cantidad)
                throw new Exception($"Stock insuficiente. Solo quedan {producto.CantidadStock}");

            var itemExistente = _carrito.FirstOrDefault(d => d.IdProducto == idProducto);
            if (itemExistente != null)
            {
                if (producto.CantidadStock < (itemExistente.Cantidad + cantidad))
                    throw new Exception("No hay suficiente stock para agregar más.");

                itemExistente.Cantidad += cantidad;
            }
            else
            {
                _carrito.Add(new DetalleOrden
                {
                    IdProducto = producto.Id,
                    NombreProducto = producto.Nombre,
                    PrecioUnitario = producto.Precio,
                    Cantidad = cantidad
                });
            }
        }

        public List<DetalleOrden> ObtenerCarrito()
        {
            return _carrito;
        }

        public decimal CalcularTotal()
        {
            return _carrito.Sum(x => x.PrecioUnitario * x.Cantidad);
        }

        public Orden FinalizarCompra(string direccionEntrega)
        {
            // 1. VERIFICACIÓN ACTUALIZADA: Usamos _sesion en lugar de SesionUsuario estático
            if (!_sesion.EstaLogueado)
                throw new Exception("Debe iniciar sesión para comprar.");

            if (_carrito.Count == 0) throw new Exception("El carrito está vacío.");

            // 2. Crear la Orden
            var nuevaOrden = new Orden
            {
                IdUsuario = _sesion.UsuarioActual.Id, // <--- Usamos el usuario de la sesión web
                Fecha = DateTime.Now,
                Total = CalcularTotal(),
                DireccionEntrega = direccionEntrega,
                Detalles = new List<DetalleOrden>(_carrito)
            };

            // 3. Guardar y Descontar Stock
            _repoOrdenes.Agregar(nuevaOrden);

            foreach (var item in _carrito)
            {
                var producto = _repoProductos.ObtenerPorId(item.IdProducto);
                producto.CantidadStock -= item.Cantidad;
                _repoProductos.Actualizar(producto);
            }

            // 4. Enviar Correo
            _servicioEmail.EnviarCorreo(
                _sesion.UsuarioActual.Correo,
                "Confirmación de Compra",
                $"Su orden #{nuevaOrden.Id} ha sido procesada exitosamente por un total de ₡{nuevaOrden.Total}"
            );

            // 5. Limpiar carrito
            _carrito.Clear();

            return nuevaOrden;
        }
    }
}