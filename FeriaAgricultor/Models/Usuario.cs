using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeriaAgricultor.Models
{
    public class Usuario : EntidadBase
    {
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }

        // Lista de direcciones guardadas 
        public List<string> DireccionesGuardadas { get; set; } = new List<string>();

        public bool EsProductor { get; set; } = false;
    }
}
