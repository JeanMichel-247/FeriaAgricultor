using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FeriaAgricultor.Core.Models;
using Newtonsoft.Json;


namespace FeriaAgricultor.Core.Data
{
    public class RepositorioJson<T> : IRepositorio<T> where T : EntidadBase
    {
        private readonly string _rutaArchivo;
        private List<T> _datos;

        public RepositorioJson(string nombreArchivo)
        {
            // Define la ruta en la carpeta de ejecución
            _rutaArchivo = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, nombreArchivo);
            CargarDatos();
        }

        private void CargarDatos()
        {
            if (File.Exists(_rutaArchivo))
            {
                var json = File.ReadAllText(_rutaArchivo);
                _datos = JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
            }
            else
            {
                _datos = new List<T>();
            }
        }

        private void GuardarDatos()
        {
            var json = JsonConvert.SerializeObject(_datos, Formatting.Indented);
            File.WriteAllText(_rutaArchivo, json);
        }

        public List<T> ObtenerTodos()
        {
            return _datos;
        }

        public T ObtenerPorId(int id)
        {
            return _datos.FirstOrDefault(x => x.Id == id);
        }

        public void Agregar(T entidad)
        {
            if (entidad.Id == 0)
            entidad.Id = _datos.Any() ? _datos.Max(x => x.Id) + 1 : 1;
            _datos.Add(entidad);
            GuardarDatos();
        }

        public void Actualizar(T entidadModificada)
        {
            // 1. Buscamos el índice del objeto viejo en la lista
            var index = _datos.FindIndex(x => x.Id == entidadModificada.Id);

            if (index != -1)
            {
                // 2. Lo reemplazamos por el nuevo
                _datos[index] = entidadModificada;

                // 3. Guardamos en el archivo JSON
                GuardarDatos();
            }
        }

        public void Eliminar(int id)
        {
            var item = ObtenerPorId(id);
            if (item != null)
            {
                _datos.Remove(item);
                GuardarDatos();
            }
        }

    }
}
