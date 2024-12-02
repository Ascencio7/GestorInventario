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

using GestorInventario.ReporteVista;
using GestorInventario.SistemaAdministrador;

namespace GestorInventario.ReporteVista
{
    /// <summary>
    /// Lógica de interacción para menuReportes.xaml
    /// </summary>
    public partial class menuReportes : Window
    {
        public menuReportes()
        {
            InitializeComponent();
        }

        private void btnReporteUsuariosActivosAdmin_Click(object sender, RoutedEventArgs e)
        {
            UsuariosActivosReporteAdmin formUsuariosRpt = new UsuariosActivosReporteAdmin();
            formUsuariosRpt.Show();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("¿Desea regresar al menú del Administrador?", "ATLAS CORP | REGRESAR", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                frmInicioAdministrador frmInicioAdministrador = new frmInicioAdministrador();
                this.Hide();
                frmInicioAdministrador.Show();
            }
        }
    }
}