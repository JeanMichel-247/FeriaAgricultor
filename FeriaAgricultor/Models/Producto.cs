using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeriaAgricultor.Models
{
    public enum UnidadMedida
    {
        Unidad,
        Kg,
        Paquete,
        Caja
    }

    public class Producto : EntidadBase
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public UnidadMedida Unidad { get; set; }
        public int CantidadStock { get; set; }
        public int IdProductor { get; set; } // Relación con el productor

        // Propiedad de solo lectura para mostrar en listas 
        public string DescripcionVisual => $"{Nombre} ({Unidad}) - ₡{Precio}";
    }
}
