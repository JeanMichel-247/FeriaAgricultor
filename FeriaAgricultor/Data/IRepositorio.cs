using FeriaAgricultor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeriaAgricultor.Data
{
    public interface IRepositorio<T> where T : EntidadBase
    {
        List<T> ObtenerTodos();
        T ObtenerPorId(int id);
        void Agregar(T entidad);
        void Actualizar(T entidad);
        void Eliminar(int id);
    }
}
