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
    /// Lógica de interacción para ProductosAdmin.xaml
    /// </summary>
    public partial class ProductosAdmin : Window
    {
        public ProductosAdmin()
        {
            InitializeComponent();
            txtNombreProductoAdmin.Focus();
        }

        private void btnRegresarAdmin_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("¿Desea regresar al menú principal del Administrador?", "ATLAS CORP | REGRESAR", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                frmInicioAdministrador formPrincipal = new frmInicioAdministrador();
                this.Hide();
                formPrincipal.Show();
            }
        }
    }
}