using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeriaAgricultor.Models
{
    // Mantiene la información del usuario logueado en memoria
    public static class SesionUsuario
    {
        public static Usuario UsuarioActual { get; set; }

        public static void CerrarSesion()
        {
            UsuarioActual = null;
        }
    }
}
