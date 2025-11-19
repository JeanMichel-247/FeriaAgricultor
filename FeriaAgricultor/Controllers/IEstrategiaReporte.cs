using FeriaAgricultor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeriaAgricultor.Controllers
{
    public interface IEstrategiaReporte
    {
        // La interfaz define que toda estrategia recibe la lista completa de órdenes
        // y devuelve una lista de filas procesadas.
        List<FilaReporte> GenerarReporte(List<Orden> ordenes, int idUsuario);
    }
}
