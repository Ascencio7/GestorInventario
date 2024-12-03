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


        #region Colores Botones
        private void btnReporteUsuariosActivosAdmin_MouseEnter(object sender, MouseEventArgs e)
        {
            btnReporteUsuariosActivosAdmin.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnReporteUsuariosActivosAdmin_MouseLeave(object sender, MouseEventArgs e)
        {
            btnReporteUsuariosActivosAdmin.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        private void btnReporteUsuariosInactivosAdmin_MouseEnter(object sender, MouseEventArgs e)
        {
            btnReporteUsuariosInactivosAdmin.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnReporteUsuariosInactivosAdmin_MouseLeave(object sender, MouseEventArgs e)
        {
            btnReporteUsuariosInactivosAdmin.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        private void btnRegresar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnRegresar.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnRegresar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnRegresar.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }
        #endregion


        #region Boton Regresar
        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea regresar al menú del Administrador?", "ATLAS CORP | REGRESAR", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                frmInicioAdministrador frmInicioAdministrador = new frmInicioAdministrador(); // Ventana principal
                this.Hide();
                frmInicioAdministrador.Show();
            }
        }
        #endregion



        #region Boton Reporte Usuario Activo
        private void btnReporteUsuariosActivosAdmin_Click(object sender, RoutedEventArgs e)
        {
            UsuariosActivosReporteAdmin formUsuariosRpt = new UsuariosActivosReporteAdmin(); // Ventana donde esta el visor de Crystal Report
            formUsuariosRpt.Show();
        }
        #endregion


        #region Boton Reporte Usuario Inactivo
        private void btnReporteUsuariosInactivosAdmin_Click(object sender, RoutedEventArgs e)
        {
            UsuariosInactivosReporteAdmin formUsuariosInactivosRpt = new UsuariosInactivosReporteAdmin(); // Ventana donde esta el visor de Crystal Report
            formUsuariosInactivosRpt.Show();
        }
        #endregion




    }
}