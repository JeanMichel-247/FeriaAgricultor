using FeriaAgricultor.Controllers;
using FeriaAgricultor.Data;
using FeriaAgricultor.Models;
using FeriaAgricultor.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FeriaAgricultor.Views
{
    public partial class FrmCatalogo : Form
    {
        private readonly IRepositorio<Producto> _repoProductos;
        private readonly IRepositorio<Orden> _repoOrdenes;
        private readonly IRepositorio<Usuario> _repoUsuarios;

        private ControladorCarrito _controladorCarrito;

        // Constructor para recibir repoUsuarios
        public FrmCatalogo(IRepositorio<Producto> repoProductos, IRepositorio<Orden> repoOrdenes, IRepositorio<Usuario> repoUsuarios)
        {
            InitializeComponent();
            _repoProductos = repoProductos;
            _repoOrdenes = repoOrdenes;
            _repoUsuarios = repoUsuarios; // Se guarda la referencia

            _controladorCarrito = new ControladorCarrito(_repoProductos, _repoOrdenes, new ServicioEmailMock());
        }

        private void FrmCatalogo_Load(object sender, EventArgs e)
        {
            // 1. Primero cargamos las Ferias (Extraemos los nombres únicos)
            CargarFerias();

            // 2. Cargamos productores (inicialmente todos porque la feria es "Todas")
            CargarProductores();

            // 3. Cargamos productos
            CargarProductos();

            if (SesionUsuario.UsuarioActual.DireccionesGuardadas.Count > 0)
            {
                txtDireccion.Text = SesionUsuario.UsuarioActual.DireccionesGuardadas[0];
            }
        }

        private void CargarFerias()
        {
            var productores = _repoUsuarios.ObtenerTodos().FindAll(u => u.EsProductor);

            // Magia LINQ:
            // 1. Tomamos el nombre (ej: "Productor 1 - Feria Zapote")
            // 2. Cortamos por el guion '-'
            // 3. Tomamos la segunda parte (la feria)
            // 4. Distinct() elimina duplicados
            var listaFerias = productores
                .Select(u => u.NombreCompleto.Contains("-") ? u.NombreCompleto.Split('-')[1].Trim() : "Sin Feria")
                .Distinct()
                .ToList();

            // Agregamos opción por defecto
            listaFerias.Insert(0, "--- Todas las Ferias ---");

            cmbFeria.DataSource = listaFerias;
        }

        // Método para llenar el combobox de productores
        private void CargarProductores(string filtroFeria = "")
        {
            // 1. Obtener todos los productores
            var listaProductores = _repoUsuarios.ObtenerTodos().FindAll(u => u.EsProductor);

            // 2. Si hay una feria seleccionada (que no sea "Todas"), filtramos
            if (!string.IsNullOrEmpty(filtroFeria) && filtroFeria != "--- Todas las Ferias ---")
            {
                // Buscamos los que tengan ese nombre de feria en su nombre completo
                listaProductores = listaProductores.FindAll(u => u.NombreCompleto.Contains(filtroFeria));
            }

            // 3. Crear opción "Ver Todos" (para los productores de esa feria específica)
            var opcionTodos = new Usuario { Id = 0, NombreCompleto = "--- Ver Todos los Productores ---" };
            listaProductores.Insert(0, opcionTodos);

            // 4. Asignar al ComboBox
            // Importante: Desvinculamos antes para evitar errores de refresco
            cmbProductores.DataSource = null;
            cmbProductores.DataSource = listaProductores;
            cmbProductores.DisplayMember = "NombreCompleto";
            cmbProductores.ValueMember = "Id";
        }

        private void cmbFeria_SelectedIndexChanged(object sender, EventArgs e)
        {
            string feriaSeleccionada = cmbFeria.SelectedValue?.ToString();

            // Recargar el segundo combo basado en la elección del primero
            CargarProductores(feriaSeleccionada);

            // Recargar la tabla general
            CargarProductos(txtBuscar.Text);
        }

        // Método para cargar productos con filtros
        private void CargarProductos(string busqueda = "")
        {
            var listaProductos = _repoProductos.ObtenerTodos();
            var listaUsuarios = _repoUsuarios.ObtenerTodos();

            // 1. Aplicar Filtro por Texto (Nombre del producto)
            if (!string.IsNullOrEmpty(busqueda))
            {
                listaProductos = listaProductos.FindAll(p => p.Nombre.ToLower().Contains(busqueda.ToLower()));
            }

            // 2. Aplicar Filtro por Productor (ComboBox)
            if (cmbProductores.SelectedValue != null)
            {
                if (int.TryParse(cmbProductores.SelectedValue.ToString(), out int idProductor))
                {
                    if (idProductor > 0) // Si no es "Ver Todos"
                    {
                        listaProductos = listaProductos.FindAll(p => p.IdProductor == idProductor);
                    }
                }
            }

            // 3. EL CRUCE MÁGICO (JOIN): Unimos Productos con Usuarios para sacar el nombre
            var listadoVisual = from p in listaProductos
                                join u in listaUsuarios on p.IdProductor equals u.Id
                                select new
                                {
                                    Id = p.Id,                  // Necesario para la lógica
                                    Producto = p.Nombre,
                                    Precio = p.Precio,
                                    Unidad = p.Unidad,
                                    Stock = p.CantidadStock,
                                    Productor = u.NombreCompleto // <--- ¡AQUÍ ESTÁ LO QUE FALTABA!
                                };

            // 4. Asignar al Grid
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = listadoVisual.ToList();

            // 5. Ocultar columnas que no interesan al usuario
            if (dgvProductos.Columns["Id"] != null) dgvProductos.Columns["Id"].Visible = false;
        }


        private void cmbProductores_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Al cambiar el combo, recargamos la lista respetando lo que haya escrito en el buscador
            CargarProductos(txtBuscar.Text);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarProductos(txtBuscar.Text);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Validar selección
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto de la lista.");
                return;
            }

            // CAMBIO AQUÍ: Ya no podemos hacer cast a (Producto) directo.
            // Leemos el ID desde la celda oculta "Id".
            int idProductoSeleccionado = (int)dgvProductos.SelectedRows[0].Cells["Id"].Value;
            int cantidad = (int)numCantidad.Value;

            try
            {
                // El controlador buscará el producto real usando el ID
                _controladorCarrito.AgregarAlCarrito(idProductoSeleccionado, cantidad);

                RefrescarCarrito();
                MessageBox.Show("Producto agregado.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RefrescarCarrito()
        {
            var items = _controladorCarrito.ObtenerCarrito();

            dgvCarrito.DataSource = null;
            dgvCarrito.DataSource = items;

            // Formato moneda en el Label
            lblTotal.Text = $"Total: ₡{_controladorCarrito.CalcularTotal():N2}";
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("Por favor ingrese una dirección de entrega.");
                return;
            }

            try
            {
                // 1. Llamamos al método que devuelve la orden
                Orden nuevaOrden = _controladorCarrito.FinalizarCompra(txtDireccion.Text);

                // 2. Abrimos el formulario de Factura pasando esa orden
                var frmFactura = new FrmFactura(nuevaOrden);
                frmFactura.ShowDialog(); // ShowDialog obliga a ver la factura antes de volver

                // 3. Limpiamos la pantalla al volver
                RefrescarCarrito();
                CargarProductos(txtBuscar.Text); // Recargar productos para ver el stock actualizado
                txtDireccion.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al comprar: {ex.Message}");
            }
        }
    }
}
