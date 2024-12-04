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

using GestorInventario.SistemaLogin;
using GestorInventario.SistemaAdministrador;
using GestorInventario.SistemaRegistro;
using GestorInventario.Reportes;
using GestorInventario.DataAcces;
using GestorInventario.ModeloUsuario;
using GestorInventario.ModeloProducto;
using GestorInventario.ModeloProveedor;
using GestorInventario.ServiceUsuario;
using GestorInventario.ServiceProducto;
using GestorInventario.ReporteVista;


namespace GestorInventario.SistemaAuxiliar
{
    /// <summary>
    /// Lógica de interacción para frmInicioAuxiliar.xaml
    /// </summary>
    public partial class frmInicioAuxiliar : Window
    {
        public frmInicioAuxiliar()
        {
            InitializeComponent();
        }



        #region Colores Botones
        private void btnReportesAuxiliar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnReportesAuxiliar.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnReportesAuxiliar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnReportesAuxiliar.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        private void btnSalirAuxiliar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnSalirAuxiliar.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnSalirAuxiliar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnSalirAuxiliar.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        private void btnCerrarAplicacionAuxiliar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnCerrarAplicacionAuxiliar.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnCerrarAplicacionAuxiliar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnCerrarAplicacionAuxiliar.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        private void btnGestionProductosAuxiliar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnGestionProductosAuxiliar.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnGestionProductosAuxiliar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnGestionProductosAuxiliar.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }
        #endregion



        #region Page de Bienvenida
        private void frPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(new Uri("SistemaAuxiliar/pageBienvenidaAuxiliar.xaml", UriKind.Relative));
        }
        #endregion



        #region Botón de Productos
        private void btnGestionProductosAuxiliar_Click(object sender, RoutedEventArgs e)
        {
            ProductosAuxiliar formProductosAuxiliar = new ProductosAuxiliar();
            this.Hide();
            formProductosAuxiliar.Show();
        }
        #endregion



        #region Boton Salir al Login
        private void btnSalirAuxiliar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea regresar al Login?", "ATLAS CORP | SALIR AL LOGIN", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                MessageBox.Show("Redireccionando al Inicio de Sesión", "ATLAS CORP | INICIO DE SESIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                Login formLogin = new Login();
                this.Hide();
                formLogin.Show();
            }
        }
        #endregion



        #region Boton Cerrar la Aplicación
        private void btnCerrarAplicacionAuxiliar_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("¿Desea cerrar la aplicación desde el Sistema Auxiliar?", "ATLAS CORP | CERRAR APLICACIÓN", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                MessageBox.Show("Cerrando la aplicación desde el Sistema Auxiliar.", "ATLAS CORP | CERRANDO APLICACIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }
        #endregion



        #region Botón de Reportes
        private void btnReportesAuxiliar_Click(object sender, RoutedEventArgs e)
        {
            menuReportesAuxiliar formMenuReportesAuxiliar = new menuReportesAuxiliar();
            this.Hide();
            formMenuReportesAuxiliar.Show();
        }
        #endregion


    }
}