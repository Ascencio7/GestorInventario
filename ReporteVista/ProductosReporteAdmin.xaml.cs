using GestorInventario.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GestorInventario.ReporteVista
{
    /// <summary>
    /// Lógica de interacción para ProductosReporteAdmin.xaml
    /// </summary>
    public partial class ProductosReporteAdmin : Window
    {
        public ProductosReporteAdmin()
        {
            InitializeComponent();
        }


        #region Generar Reporte de Productos
        private void btnGenerarReporte_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                rptProductos rpt = new rptProductos();
                frmProductosReporteVista visor = new frmProductosReporteVista();
                rpt.Load("@rptProductos.rpt");

                visor.crystalProductosReporteAdmin.ViewerCore.ReportSource = rpt;
                visor.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte: " + ex.Message, "ATLAS CORP | ERROR AL GENERAR EL REPORTE", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion


    }
}