using GestorInventario.SistemaRegistro;
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
using GestorInventario.SistemaLogin;

namespace GestorInventario.SistemaAdministrador
{
    /// <summary>
    /// Lógica de interacción para frmInicioAdministrador.xaml
    /// </summary>
    public partial class frmInicioAdministrador : Window
    {
        public frmInicioAdministrador()
        {
            InitializeComponent();
        }


        #region Colores Botones
        private void btnGestionRegistroAdmin_MouseEnter(object sender, MouseEventArgs e)
        {
            btnGestionRegistroAdmin.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnGestionRegistroAdmin_MouseLeave(object sender, MouseEventArgs e)
        {
            btnGestionRegistroAdmin.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        private void btnGestionUsuariosInactivosAdmin_MouseEnter(object sender, MouseEventArgs e)
        {
            btnGestionUsuariosInactivosAdmin.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnGestionUsuariosInactivosAdmin_MouseLeave(object sender, MouseEventArgs e)
        {
            btnGestionUsuariosInactivosAdmin.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        private void btnSalirAdmin_MouseEnter(object sender, MouseEventArgs e)
        {
            btnSalirAdmin.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnSalirAdmin_MouseLeave(object sender, MouseEventArgs e)
        {
            btnSalirAdmin.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        private void btnCerrarAplicacionAdmin_MouseEnter(object sender, MouseEventArgs e)
        {
            btnCerrarAplicacionAdmin.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnCerrarAplicacionAdmin_MouseLeave(object sender, MouseEventArgs e)
        {
            btnCerrarAplicacionAdmin.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        private void btnGestionProductosAdmin_MouseEnter(object sender, MouseEventArgs e)
        {
            btnGestionProductosAdmin.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnGestionProductosAdmin_MouseLeave(object sender, MouseEventArgs e)
        {
            btnGestionProductosAdmin.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        private void btnReportesAdmin_MouseEnter(object sender, MouseEventArgs e)
        {
            btnReportesAdmin.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnReportesAdmin_MouseLeave(object sender, MouseEventArgs e)
        {
            btnReportesAdmin.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }
        #endregion



        #region Page de bienvenida
        private void frPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(new Uri("SistemaAdministrador/pageBienvenidoAdmin.xaml", UriKind.Relative));
        }
        #endregion



        #region Boton de Registrar Usuarios
        private void btnGestionRegistroAdmin_Click(object sender, RoutedEventArgs e)
        {
            frmRegistro formRegistro = new frmRegistro();
            this.Hide();
            formRegistro.Show();
        }
        #endregion

        

        #region Boton Gestion Usuario Inactivo
        private void btnGestionUsuariosInactivosAdmin_Click(object sender, RoutedEventArgs e)
        {
            UsuariosInactivosAdmin formUsuariosInactivos = new UsuariosInactivosAdmin();
            this.Hide();
            formUsuariosInactivos.Show();
        }
        #endregion



        #region Boton Gestion Productos
        private void btnGestionProductosAdmin_Click(object sender, RoutedEventArgs e)
        {
            ProductosAdmin formProductosAdmin = new ProductosAdmin();
            this.Hide();
            formProductosAdmin.Show();
        }
        #endregion



        #region Boton Reportes
        private void btnReportesAdmin_Click(object sender, RoutedEventArgs e)
        {
            menuReportes formMenuReportes = new menuReportes();
            this.Hide();
            formMenuReportes.Show();
        }
        #endregion



        #region Boton Salir al Login
        private void btnSalirAdmin_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("¿Desea salir del Sistema Administrador?", "ATLAS CORP | SISTEMA ADMINISTRADOR", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                MessageBox.Show("Redireccionando al Inicio de Sesión", "ATLAS CORP", MessageBoxButton.OK,MessageBoxImage.Information);
                Login formLogin = new Login();
                this.Hide();
                formLogin.Show();
            }
        }
        #endregion



        #region Boton Cerrar la Aplicacion
        private void btnCerrarAplicacionAdmin_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("¿Desea cerrar la aplicación?", "ATLAS CORP | CERRAR APLICACIÓN", MessageBoxButton.YesNo, MessageBoxImage.Question)== MessageBoxResult.Yes)
            {
                MessageBox.Show("Cerrando la aplicación desde el Sistema Administrador.", "ATLAS CORP | CERRANDO APLICACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }

        #endregion

    }
}