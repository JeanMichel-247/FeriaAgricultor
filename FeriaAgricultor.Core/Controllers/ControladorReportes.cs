using FeriaAgricultor.Core.Data;
using FeriaAgricultor.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeriaAgricultor.Core.Controllers
{
    public class ControladorReportes
    {
        private readonly IRepositorio<Orden> _repoOrdenes;
        private IEstrategiaReporte _estrategiaActual;

        public ControladorReportes(IRepositorio<Orden> repoOrdenes)
        {
            _repoOrdenes = repoOrdenes;
        }

        // Método para cambiar la estrategia dinámicamente (Runtime)
        public void EstablecerEstrategia(IEstrategiaReporte estrategia)
        {
            _estrategiaActual = estrategia;
        }

        public List<FilaReporte> ObtenerReporte()
        {
            if (_estrategiaActual == null)
                return new List<FilaReporte>();

            var todasLasOrdenes = _repoOrdenes.ObtenerTodos();
            var idUsuario = SesionUsuario.UsuarioActual.Id;

            // El controlador delega el cálculo a la estrategia
            return _estrategiaActual.GenerarReporte(todasLasOrdenes, idUsuario);
        }
    }
}
