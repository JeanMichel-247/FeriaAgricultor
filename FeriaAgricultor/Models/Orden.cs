using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeriaAgricultor.Models
{
    public class Orden : EntidadBase
    {
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public List<DetalleOrden> Detalles { get; set; } = new List<DetalleOrden>();
        public decimal Total { get; set; }
        public string DireccionEntrega { get; set; }
    }
}
