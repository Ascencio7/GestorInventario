using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    /// Lógica de interacción para UsuariosActivosReporteAdmin.xaml
    /// </summary>
    public partial class UsuariosActivosReporteAdmin : Window
    {
        public UsuariosActivosReporteAdmin()
        {
            InitializeComponent();
        }

        private void btnGenerarReporte_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                rptUsuarios rpt = new rptUsuarios();
                frmUsuariosActivosReporteVista visor = new frmUsuariosActivosReporteVista();    
                rpt.Load("@rptUsuarios.rpt");

                visor.crystalUsuariosActivosReporteAdmin.ViewerCore.ReportSource = rpt;
                visor.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}