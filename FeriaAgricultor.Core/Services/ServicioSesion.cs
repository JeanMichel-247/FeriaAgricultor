using FeriaAgricultor.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeriaAgricultor.Core.Services
{
    public class ServicioSesion
    {
        // Propiedad para guardar el usuario de ESTA conexión específica
        public Usuario? UsuarioActual { get; private set; }

        public bool EstaLogueado => UsuarioActual != null;

        public void IniciarSesion(Usuario usuario)
        {
            UsuarioActual = usuario;
        }

        public void CerrarSesion()
        {
            UsuarioActual = null;
        }
    }
}
