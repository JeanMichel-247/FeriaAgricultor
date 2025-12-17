using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeriaAgricultor.Core.Models
{
    // DTO simple para mostrar datos en la tabla
    public class FilaReporte
    {
        public string Etiqueta { get; set; } // Para los nombres
        public string Valor { get; set; }    // Para los valores
    }
}
