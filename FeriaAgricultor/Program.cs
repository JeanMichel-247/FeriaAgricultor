using System;
using System.Windows.Forms;
using FeriaAgricultor.Data;
using FeriaAgricultor.Models;
using FeriaAgricultor.Views; 

namespace FeriaAgricultor
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 1. Inicialización de repositorios
            var repoUsuarios = new RepositorioJson<Usuario>("usuarios.json");
            var repoProductos = new RepositorioJson<Producto>("productos.json");
            var repoOrdenes = new RepositorioJson<Orden>("ordenes.json");

            // 2. Carga de datos iniciales
            InicializadorDatos.Inicializar(repoUsuarios, repoProductos, repoOrdenes);


            // 3. Lanzar el login
            Application.Run(new FrmLogin(repoUsuarios, repoProductos, repoOrdenes));
        }
    }
}