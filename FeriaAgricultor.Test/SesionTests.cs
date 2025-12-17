using Xunit;
using FeriaAgricultor.Core.Services;
using FeriaAgricultor.Core.Models;

namespace FeriaAgricultor.Tests
{
    public class SesionTests
    {
        [Fact]
        public void IniciarSesion_DeberiaGuardarUsuario()
        {
            // ARRANGE
            var servicio = new ServicioSesion();
            var usuario = new Usuario { Id = 1, NombreCompleto = "Juan" };

            // ACT
            servicio.IniciarSesion(usuario);

            // ASSERT
            Assert.True(servicio.EstaLogueado);
            Assert.Equal("Juan", servicio.UsuarioActual.NombreCompleto);
        }

        [Fact]
        public void CerrarSesion_DeberiaLimpiarUsuario()
        {
            // ARRANGE
            var servicio = new ServicioSesion();
            servicio.IniciarSesion(new Usuario());

            // ACT
            servicio.CerrarSesion();

            // ASSERT
            Assert.False(servicio.EstaLogueado);
            Assert.Null(servicio.UsuarioActual);
        }
    }
}