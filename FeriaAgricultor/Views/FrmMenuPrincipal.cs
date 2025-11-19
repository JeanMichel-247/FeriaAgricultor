using FeriaAgricultor.Data;
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
    public partial class FrmMenuPrincipal : Form
    {
        // Referencias para pasarlas a las ventanas hijas
        private readonly IRepositorio<Usuario> _repoUsuarios;
        private readonly IRepositorio<Producto> _repoProductos;
        private readonly IRepositorio<Orden> _repoOrdenes;

        public FrmMenuPrincipal(IRepositorio<Usuario> repoUsuarios, IRepositorio<Producto> repoProductos, IRepositorio<Orden> repoOrdenes)
        {
            InitializeComponent();
            _repoUsuarios = repoUsuarios;
            _repoProductos = repoProductos;
            _repoOrdenes = repoOrdenes;
        }

        private void FrmMenuPrincipal_Load(object sender, EventArgs e)
        {
            if (SesionUsuario.UsuarioActual != null)
            {
                lblBienvenida.Text = $"Bienvenido, {SesionUsuario.UsuarioActual.NombreCompleto}";

                if (SesionUsuario.UsuarioActual.EsProductor)
                {
                    // btnGestionInventario.Visible = true;
                }
            }
        }

        private void menuCatalogo_Click(object sender, EventArgs e)
        {
            // Ahora pasamos también _repoUsuarios
            var frmCatalogo = new FrmCatalogo(_repoProductos, _repoOrdenes, _repoUsuarios);
            frmCatalogo.ShowDialog();
        }

        private void menuSalir_Click(object sender, EventArgs e)
        {
            SesionUsuario.CerrarSesion();
            this.Close();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            // Solo necesitamos el repositorio de ordenes para los reportes
            var frm = new FrmReportes(_repoOrdenes, _repoProductos);
            frm.ShowDialog();
        }
    }
}
