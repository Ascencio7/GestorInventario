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

        private void frPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            frPrincipal.NavigationService.Navigate(new Uri("SistemaAdministrador/pageBienvenidoAdmin.xaml", UriKind.Relative));

        }

        private void btnGestionRegistroAdmin_Click(object sender, RoutedEventArgs e)
        {
            frmRegistro formRegistro = new frmRegistro();
            this.Hide();
            formRegistro.Show();
        }
    }
}
