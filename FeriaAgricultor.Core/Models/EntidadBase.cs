using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeriaAgricultor.Core.Models
{
    // Clase abstracta para garantizar que todos los objetos tengan ID
    public abstract class EntidadBase
    {
        public int Id { get; set; }
    }
}
