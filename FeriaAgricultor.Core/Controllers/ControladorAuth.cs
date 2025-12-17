using FeriaAgricultor.Core.Data;
using FeriaAgricultor.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeriaAgricultor.Core.Controllers
{
    public class ControladorAuth
    {
        private readonly IRepositorio<Usuario> _repoUsuarios;

        // Inyección de dependencias manual
        public ControladorAuth(IRepositorio<Usuario> repoUsuarios)
        {
            _repoUsuarios = repoUsuarios;
        }

        public bool IniciarSesion(string correo, string clave)
        {
            var usuario = _repoUsuarios.ObtenerTodos()
                            .FirstOrDefault(u => u.Correo == correo && u.Clave == clave);

            if (usuario != null)
            {
                SesionUsuario.UsuarioActual = usuario;
                return true;
            }
            return false;
        }

        public void RegistrarUsuario(string nombre, string correo, string clave, bool esProductor)
        {
            // Validación básica
            if (_repoUsuarios.ObtenerTodos().Any(u => u.Correo == correo))
            {
                throw new Exception("El correo ya está registrado.");
            }

            var nuevoUsuario = new Usuario
            {
                NombreCompleto = nombre,
                Correo = correo,
                Clave = clave,
                EsProductor = esProductor
            };

            _repoUsuarios.Agregar(nuevoUsuario);
        }
    }
}
