using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FeriaAgricultor.Models;
using Newtonsoft.Json;


namespace FeriaAgricultor.Data
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
            // Simula un Auto incremental de base de datos
            entidad.Id = _datos.Any() ? _datos.Max(x => x.Id) + 1 : 1;
            _datos.Add(entidad);
            GuardarDatos();
        }

        public void Actualizar(T entidad)
        {
            var indice = _datos.FindIndex(x => x.Id == entidad.Id);
            if (indice != -1)
            {
                _datos[indice] = entidad;
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
