using FeriaAgricultor.Controllers;
using FeriaAgricultor.Data;
using FeriaAgricultor.Models;
using System;
using System.Windows.Forms;

namespace FeriaAgricultor.Views 
{
    public partial class FrmLogin : Form
    {
        private readonly ControladorAuth _controladorAuth;

        // Guardamos referencias a los repos para pasarlos al menú principal
        private readonly IRepositorio<Producto> _repoProductos;
        private readonly IRepositorio<Orden> _repoOrdenes;
        private readonly IRepositorio<Usuario> _repoUsuarios;

        // Constructor que RECIBE las dependencias
        public FrmLogin(IRepositorio<Usuario> repoUsuarios, IRepositorio<Producto> repoProductos, IRepositorio<Orden> repoOrdenes)
        {
            InitializeComponent();

            _repoUsuarios = repoUsuarios;
            _repoProductos = repoProductos;
            _repoOrdenes = repoOrdenes;

            // Inicializamos el controlador
            _controladorAuth = new ControladorAuth(_repoUsuarios);
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string correo = txtCorreo.Text;
            string clave = txtClave.Text;

            if (_controladorAuth.IniciarSesion(correo, clave))
            {
                // Login exitoso
                // Ocultamos el login y abrimos el menú principal
                this.Hide();
                var frmMenu = new FrmMenuPrincipal(_repoUsuarios, _repoProductos, _repoOrdenes);
                frmMenu.ShowDialog(); // Bloqueante hasta que se cierre el menú
                this.Close(); // Cierra la app completa al volver
            }
            else
            {
                lblMensaje.Text = "Credenciales inválidas. Intente de nuevo.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
