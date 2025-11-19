using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeriaAgricultor.Services
{
    // Esta clase simula el envío real.
    public class ServicioEmailMock : IServicioEmail
    {
        public void EnviarCorreo(string destinatario, string asunto, string cuerpo)
        {
            // Simplemente escribimos en la consola de depuración 
            Debug.WriteLine($"[SIMULACIÓN EMAIL] A: {destinatario} | Asunto: {asunto} | Mensaje: {cuerpo}");
        }
    }
}
