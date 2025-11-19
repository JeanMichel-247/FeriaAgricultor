using FeriaAgricultor.Models;
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
    public partial class FrmFactura : Form
    {
        private Orden _orden;

        // El constructor recibe la orden que acabamos de crear
        public FrmFactura(Orden orden)
        {
            InitializeComponent();
            _orden = orden;
        }

        private void FrmFactura_Load(object sender, EventArgs e)
        {
            // Cargar datos de cabecera
            lblNumeroOrden.Text = $"Orden #{_orden.Id}";
            lblFecha.Text = $"Fecha: {_orden.Fecha}";

            // Usamos la sesión global para sacar el nombre del cliente
            lblCliente.Text = $"Cliente: {SesionUsuario.UsuarioActual.NombreCompleto}";

            // Cargar la grilla
            dgvDetalleFactura.DataSource = _orden.Detalles;

            // Formato visual de la grilla 
            if (dgvDetalleFactura.Columns["IdProducto"] != null) dgvDetalleFactura.Columns["IdProducto"].Visible = false;

            // Total
            lblTotalFactura.Text = $"Total a pagar: ₡{_orden.Total:N2}";
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
