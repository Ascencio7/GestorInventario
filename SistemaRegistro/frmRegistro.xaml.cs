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
using GestorInventario.SistemaAdministrador;
using GestorInventario.SistemaLogin;

namespace GestorInventario.SistemaRegistro
{
    /// <summary>
    /// Lógica de interacción para frmRegistro.xaml
    /// </summary>
    public partial class frmRegistro : Window
    {
        public frmRegistro()
        {
            InitializeComponent();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultado = MessageBox.Show("¿Desea regresar?", "ATLAS CORP | Registro", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                frmInicioAdministrador inicioAdmin = new frmInicioAdministrador();
                this.Hide();
                inicioAdmin.Show();
            }
        }

        private void btnVolver_MouseEnter(object sender, MouseEventArgs e)
        {
            btnVolver.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnVolver_MouseLeave(object sender, MouseEventArgs e)
        {
            btnVolver.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        private void btnRegistrarse_MouseEnter(object sender, MouseEventArgs e)
        {
            btnRegistrarse.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));

        }

        private void btnRegistrarse_MouseLeave(object sender, MouseEventArgs e)
        {
            btnRegistrarse.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original

        }
    }
}
