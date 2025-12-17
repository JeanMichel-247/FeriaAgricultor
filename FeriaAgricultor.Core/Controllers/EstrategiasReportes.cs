using FeriaAgricultor.Core.Data;
using FeriaAgricultor.Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeriaAgricultor.Core.Controllers
{
    // ---------------- ESTRATEGIA 1: Gastos por Mes ----------------
    public class EstrategiaGastosPorMes : IEstrategiaReporte
    {
        public List<FilaReporte> GenerarReporte(List<Orden> ordenes, int idUsuario)
        {
            // 1. Filtrar órdenes del usuario
            var misOrdenes = ordenes.Where(o => o.IdUsuario == idUsuario);

            // 2. Agrupar por Mes y Año
            var agrupado = misOrdenes
                .GroupBy(o => new { o.Fecha.Month, o.Fecha.Year })
                .Select(g => new FilaReporte
                {
                    Etiqueta = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month)} {g.Key.Year}",
                    Valor = $"₡{g.Sum(o => o.Total):N2}"
                })
                .ToList();

            return agrupado;
        }
    }

    // ---------------- ESTRATEGIA 2: Productos Más Comprados ----------------
    public class EstrategiaProductosTop : IEstrategiaReporte
    {
        public List<FilaReporte> GenerarReporte(List<Orden> ordenes, int idUsuario)
        {
            // 1. Filtrar mis órdenes
            var misOrdenes = ordenes.Where(o => o.IdUsuario == idUsuario);

            // 2. Aplanar la lista (De Orden a detalles)
            var todosLosItems = misOrdenes.SelectMany(o => o.Detalles);

            // 3. Agrupar por Nombre de producto
            var agrupado = todosLosItems
                .GroupBy(d => d.NombreProducto)
                .Select(g => new FilaReporte
                {
                    Etiqueta = g.Key,
                    Valor = $"{g.Sum(d => d.Cantidad)} Unidades"
                })
                .OrderByDescending(x => x.Valor) // Ordenar del más comprado al menos
                .ToList();

            return agrupado;
        }
    }

    // ---------------- ESTRATEGIA 3: Inventario Restante ----------------
    public class EstrategiaInventario : IEstrategiaReporte
    {
        private readonly IRepositorio<Producto> _repoProductos;

        // Inyectamos el repositorio de productos porque este reporte NO usa órdenes
        public EstrategiaInventario(IRepositorio<Producto> repoProductos)
        {
            _repoProductos = repoProductos;
        }

        public List<FilaReporte> GenerarReporte(List<Orden> ordenes, int idUsuario)
        {
            // 1. Obtenemos todos los productos
            var productos = _repoProductos.ObtenerTodos();

            // 2. (Opcional) Si el usuario es productor, filtramos solo SUS productos
            // Si quieres ver todo el inventario global, quita este if.
            if (SesionUsuario.UsuarioActual.EsProductor)
            {
                productos = productos.Where(p => p.IdProductor == SesionUsuario.UsuarioActual.Id).ToList();
            }

            // 3. Transformamos a filas de reporte
            var reporte = productos
                .OrderBy(p => p.CantidadStock) // Ordenamos del que tiene menos stock al que tiene más
                .Select(p => new FilaReporte
                {
                    Etiqueta = p.Nombre, // Nombre del producto
                    Valor = $"{p.CantidadStock} {p.Unidad}" // Ej: "5 Kg"
                })
                .ToList();

            return reporte;
        }
    }
    // ---------------- ESTRATEGIA 4: Historial Completo de Órdenes ----------------
    public class EstrategiaHistorial : IEstrategiaReporte
    {
        private readonly IRepositorio<Usuario> _repoUsuarios;

        // Inyectamos el repo de usuarios para poder buscar los nombres de los clientes
        public EstrategiaHistorial(IRepositorio<Usuario> repoUsuarios)
        {
            _repoUsuarios = repoUsuarios;
        }

        public List<FilaReporte> GenerarReporte(List<Orden> ordenes, int idUsuario)
        {
            var usuarios = _repoUsuarios.ObtenerTodos();
            IEnumerable<Orden> ordenesFiltradas = ordenes;

            // Lógica de negocio:
            // - Si soy Admin, veo TODAS las órdenes del sistema.
            // - Si soy Cliente, veo solo mis órdenes.
            if (!SesionUsuario.UsuarioActual.EsProductor)
            {
                ordenesFiltradas = ordenes.Where(o => o.IdUsuario == idUsuario);
            }

            // Cruzamos Ordenes con Usuarios para obtener el nombre del cliente
            var reporte = from o in ordenesFiltradas
                          join u in usuarios on o.IdUsuario equals u.Id
                          orderby o.Fecha descending // Las más recientes primero
                          select new FilaReporte
                          {
                              // En la etiqueta ponemos Fecha e ID
                              Etiqueta = $"{o.Fecha.ToShortDateString()} - Orden #{o.Id}",

                              // En el valor ponemos el Monto y el Nombre del Cliente
                              Valor = $"₡{o.Total:N2} - Cliente: {u.NombreCompleto}"
                          };

            return reporte.ToList();
        }
    }
}
