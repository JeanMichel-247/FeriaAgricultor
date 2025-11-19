using FeriaAgricultor.Controllers;
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
    public partial class FrmReportes : Form
    {
        private readonly ControladorReportes _controlador;

        // Necesitamos acceso a los productos para pasárselo a la estrategia
        private readonly IRepositorio<Producto> _repoProductos;

        public FrmReportes(IRepositorio<Orden> repoOrdenes, IRepositorio<Producto> repoProductos)
        {
            InitializeComponent();
            _repoProductos = repoProductos; // Guardamos la referencia
            _controlador = new ControladorReportes(repoOrdenes);
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            // Selección de Estrategia
            switch (cmbTipoReporte.SelectedIndex)
            {
                case 0: // Gastos Mensuales
                    _controlador.EstablecerEstrategia(new EstrategiaGastosPorMes());
                    break;
                case 1: // Productos Favoritos
                    _controlador.EstablecerEstrategia(new EstrategiaProductosTop());
                    break;
                case 2: // Inventario
                    _controlador.EstablecerEstrategia(new EstrategiaInventario(_repoProductos));
                    break;
                default:
                    MessageBox.Show("Seleccione un tipo de reporte.");
                    return;
            }

            // 2. Ejecutar
            var datos = _controlador.ObtenerReporte();

            // 3. Mostrar
            dgvResultados.DataSource = null;
            dgvResultados.DataSource = datos;

            if (datos.Count == 0)
            {
                MessageBox.Show("No hay datos para generar este reporte.");
            }
        }
    }
}
