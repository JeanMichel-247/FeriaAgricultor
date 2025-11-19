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
            CargarProductores(); 
            CargarProductos();

            if (SesionUsuario.UsuarioActual.DireccionesGuardadas.Count > 0)
            {
                txtDireccion.Text = SesionUsuario.UsuarioActual.DireccionesGuardadas[0];
            }
        }

        // Método para llenar el combobox de productores
        private void CargarProductores()
        {
            // 1. Obtener solo los usuarios que son productores
            var listaProductores = _repoUsuarios.ObtenerTodos().FindAll(u => u.EsProductor);

            // 2. Crear una opción  para ver todos
            var opcionTodos = new Usuario { Id = 0, NombreCompleto = "--- Ver Todos ---" };
            listaProductores.Insert(0, opcionTodos);

            // 3. Asignar al ComboBox
            cmbProductores.DataSource = listaProductores;
            cmbProductores.DisplayMember = "NombreCompleto"; // Lo que se ve
            cmbProductores.ValueMember = "Id"; // El valor oculto
        }

        // Método para cargar productos con filtros
        private void CargarProductos(string busqueda = "")
        {
            var lista = _repoProductos.ObtenerTodos();

            // 1. Filtro por nombre
            if (!string.IsNullOrEmpty(busqueda))
            {
                lista = lista.FindAll(p => p.Nombre.ToLower().Contains(busqueda.ToLower()));
            }

            // 2. Filtro por productor 
            // Verificamos si el combo ya tiene algo seleccionado y si no es "Ver Todos"
            if (cmbProductores.SelectedValue != null)
            {
                // Aseguramos que sea int
                if (int.TryParse(cmbProductores.SelectedValue.ToString(), out int idProductor))
                {
                    if (idProductor > 0) // Si no es la opción 0 
                    {
                        lista = lista.FindAll(p => p.IdProductor == idProductor);
                    }
                }
            }

            dgvProductos.DataSource = null;
            dgvProductos.DataSource = lista;

            // Ocultar columnas técnicas
            if (dgvProductos.Columns["Id"] != null) dgvProductos.Columns["Id"].Visible = false;
            if (dgvProductos.Columns["IdProductor"] != null) dgvProductos.Columns["IdProductor"].Visible = false;
            if (dgvProductos.Columns["DescripcionVisual"] != null) dgvProductos.Columns["DescripcionVisual"].Visible = false;
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

            // Obtener el objeto seleccionado
            var productoSeleccionado = (Producto)dgvProductos.SelectedRows[0].DataBoundItem;
            int cantidad = (int)numCantidad.Value;

            try
            {
                // Delegar al controlador
                _controladorCarrito.AgregarAlCarrito(productoSeleccionado.Id, cantidad);

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
