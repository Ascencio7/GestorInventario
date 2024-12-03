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


using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

using GestorInventario.Reportes;

namespace GestorInventario.ReporteVista
{
    /// <summary>
    /// Lógica de interacción para UsuariosInactivosReporteAdmin.xaml
    /// </summary>
    public partial class UsuariosInactivosReporteAdmin : Window
    {
        public UsuariosInactivosReporteAdmin()
        {
            InitializeComponent();
        }

        private void btnGenerarReporte_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                rptUsuariosInactivos rpt = new rptUsuariosInactivos();
                frmUsuariosInactivosReporteVista visor = new frmUsuariosInactivosReporteVista();
                rpt.Load("@rptUsuariosInactivos.rpt");

                visor.crystalUsuariosInactivosReporteAdmin.ViewerCore.ReportSource = rpt;
                visor.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}
