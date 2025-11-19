using FeriaAgricultor.Data;
using FeriaAgricultor.Models;
using FeriaAgricultor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeriaAgricultor.Controllers
{
    public class ControladorCarrito
    {
        private readonly IRepositorio<Producto> _repoProductos;
        private readonly IRepositorio<Orden> _repoOrdenes;
        private readonly IServicioEmail _servicioEmail;

        // El carrito temporal en memoria antes de pagar
        private List<DetalleOrden> _carritoTemporal;

        public ControladorCarrito(IRepositorio<Producto> repoProductos, IRepositorio<Orden> repoOrdenes, IServicioEmail servicioEmail)
        {
            _repoProductos = repoProductos;
            _repoOrdenes = repoOrdenes;
            _servicioEmail = servicioEmail;
            _carritoTemporal = new List<DetalleOrden>();
        }

        public void AgregarAlCarrito(int idProducto, int cantidad)
        {
            var producto = _repoProductos.ObtenerPorId(idProducto);
            if (producto == null) throw new Exception("Producto no encontrado.");

            if (producto.CantidadStock < cantidad)
                throw new Exception($"Stock insuficiente. Solo quedan {producto.CantidadStock}.");

            // Verificar si ya está en el carrito para sumar cantidad
            var itemExistente = _carritoTemporal.FirstOrDefault(d => d.IdProducto == idProducto);
            if (itemExistente != null)
            {
                if (producto.CantidadStock < (itemExistente.Cantidad + cantidad))
                    throw new Exception("No hay suficiente stock para agregar más cantidad.");

                itemExistente.Cantidad += cantidad;
            }
            else
            {
                _carritoTemporal.Add(new DetalleOrden
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
            return _carritoTemporal;
        }

        public decimal CalcularTotal()
        {
            return _carritoTemporal.Sum(x => x.Subtotal);
        }

        public void VaciarCarrito()
        {
            _carritoTemporal.Clear();
        }

        public Orden FinalizarCompra(string direccionEntrega)
        {
            if (!_carritoTemporal.Any()) throw new Exception("El carrito está vacío.");
            if (SesionUsuario.UsuarioActual == null) throw new Exception("Debe iniciar sesión.");

            // 1. Crear la Orden
            var orden = new Orden
            {
                IdUsuario = SesionUsuario.UsuarioActual.Id,
                Fecha = DateTime.Now,
                Detalles = new List<DetalleOrden>(_carritoTemporal), // Copia de la lista
                Total = CalcularTotal(),
                DireccionEntrega = direccionEntrega
            };

            // 2. Descontar Inventario 
            foreach (var item in _carritoTemporal)
            {
                var producto = _repoProductos.ObtenerPorId(item.IdProducto);
                producto.CantidadStock -= item.Cantidad;
                _repoProductos.Actualizar(producto);
            }

            // 3. Guardar Orden
            _repoOrdenes.Agregar(orden);

            // 4. Limpiar carrito
            VaciarCarrito();

            return orden;
        }
    }
}
