using Xunit;
using Moq;
using FeriaAgricultor.Core.Controllers;
using FeriaAgricultor.Core.Data;
using FeriaAgricultor.Core.Models;
using FeriaAgricultor.Core.Services;
using System.Collections.Generic;

namespace FeriaAgricultor.Tests
{
    public class ControladorCarritoTests
    {
        // Objetos simulados (Mocks)
        private readonly Mock<IRepositorio<Producto>> _mockRepoProductos;
        private readonly Mock<IRepositorio<Orden>> _mockRepoOrdenes;
        private readonly Mock<IServicioEmail> _mockEmail;
        private readonly ServicioSesion _sesion;
        private readonly ControladorCarrito _controlador;

        public ControladorCarritoTests()
        {
            // 1. SETUP: Preparamos el entorno falso antes de cada prueba
            _mockRepoProductos = new Mock<IRepositorio<Producto>>();
            _mockRepoOrdenes = new Mock<IRepositorio<Orden>>();
            _mockEmail = new Mock<IServicioEmail>();
            _sesion = new ServicioSesion(); // La sesión la usamos real porque es simple

            // Inicializamos el controlador con los objetos falsos
            _controlador = new ControladorCarrito(
                _mockRepoProductos.Object,
                _mockRepoOrdenes.Object,
                _mockEmail.Object,
                _sesion
            );
        }

        [Fact]
        public void AgregarAlCarrito_DeberiaAgregarItem_SiHayStock()
        {
            // ARRANGE (Preparar)
            var productoId = 1;
            var producto = new Producto { Id = 1, Nombre = "Papa", Precio = 500, CantidadStock = 10 };

            // Le decimos al mock: "Cuando te pidan el ID 1, devuelve este producto"
            _mockRepoProductos.Setup(r => r.ObtenerPorId(productoId)).Returns(producto);

            // ACT (Actuar)
            _controlador.AgregarAlCarrito(productoId, 2);

            // ASSERT (Verificar)
            var carrito = _controlador.ObtenerCarrito();
            Assert.Single(carrito); // Verifica que hay 1 línea en el carrito
            Assert.Equal(2, carrito[0].Cantidad); // Verifica la cantidad
            Assert.Equal(1000, _controlador.CalcularTotal()); // 500 * 2 = 1000
        }

        [Fact]
        public void AgregarAlCarrito_DeberiaLanzarExcepcion_SiNoHayStockSuficiente()
        {
            // ARRANGE
            var productoId = 1;
            var producto = new Producto { Id = 1, Nombre = "Tomate", CantidadStock = 5 };
            _mockRepoProductos.Setup(r => r.ObtenerPorId(productoId)).Returns(producto);

            // ACT & ASSERT
            // Intentamos agregar 10 cuando solo hay 5. Esperamos un error.
            var excepcion = Assert.Throws<System.Exception>(() => _controlador.AgregarAlCarrito(productoId, 10));

            Assert.Contains("Stock insuficiente", excepcion.Message);
        }

        [Fact]
        public void FinalizarCompra_DeberiaCrearOrdenYDescontarStock_SiUsuarioLogueado()
        {
            // ARRANGE
            // 1. Logueamos al usuario falsamente
            var usuario = new Usuario { Id = 99, NombreCompleto = "Test User", Correo = "test@feria.com" };
            _sesion.IniciarSesion(usuario);

            // 2. Preparamos producto
            var producto = new Producto { Id = 1, Nombre = "Yuca", Precio = 100, CantidadStock = 20 };
            _mockRepoProductos.Setup(r => r.ObtenerPorId(1)).Returns(producto);

            // 3. Agregamos al carrito
            _controlador.AgregarAlCarrito(1, 5); // Compramos 5 yucas

            // ACT
            var orden = _controlador.FinalizarCompra("San José Centro");

            // ASSERT
            Assert.NotNull(orden);
            Assert.Equal(500, orden.Total); // 5 * 100

            // Verificamos que se llamó al método Agregar del repositorio de Órdenes
            _mockRepoOrdenes.Verify(r => r.Agregar(It.IsAny<Orden>()), Times.Once);

            // Verificamos que se actualizó el stock del producto (20 - 5 = 15)
            Assert.Equal(15, producto.CantidadStock);
            _mockRepoProductos.Verify(r => r.Actualizar(It.IsAny<Producto>()), Times.Once);
        }

        [Fact]
        public void FinalizarCompra_DeberiaFallar_SiNoHayUsuarioLogueado()
        {
            // ARRANGE
            // No iniciamos sesión (ServicioSesion está vacío)
            var producto = new Producto { Id = 1, CantidadStock = 10 };
            _mockRepoProductos.Setup(r => r.ObtenerPorId(1)).Returns(producto);
            _controlador.AgregarAlCarrito(1, 1);

            // ACT & ASSERT
            Assert.Throws<System.Exception>(() => _controlador.FinalizarCompra("Casa"));
        }
    }
}