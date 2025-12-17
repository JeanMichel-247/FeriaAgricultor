using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeriaAgricultor.Core.Models
{
    public class DetalleOrden
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } // Guardamos el nombre por si luego cambia en el catálogo
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
