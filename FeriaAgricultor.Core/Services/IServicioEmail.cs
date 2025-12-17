using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeriaAgricultor.Core.Services
{
    public interface IServicioEmail
    {
        void EnviarCorreo(string destinatario, string asunto, string cuerpo);
    }
}
