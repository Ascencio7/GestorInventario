using GestorInventario.DataAcces;
using GestorInventario.ServiceUsuario;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Lógica de interacción para UsuariosInactivosAdmin.xaml
    /// </summary>
    public partial class UsuariosInactivosAdmin : Window
    {
        public UsuariosInactivosAdmin()
        {
            InitializeComponent();
            MostrarUsuariosInactivos();
        }


        #region Mostrar Usuarios Inactivos

        void MostrarUsuariosInactivos()
        {
            using (SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    ConexionDB.AbrirConexion(conexion);

                    var usuarios = DatosUsuarios.MostrarUsuariosInactivos();

                    foreach (var usuario in usuarios)
                    {
                        usuario.Contra = "**********";
                    }

                    gridUsuariosInactivosAdmin.ItemsSource = usuarios;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al mostrar los usuarios: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    ConexionDB.CerrarConexion(conexion);
                }
            }
        }
        #endregion


        #region Boton Volver
        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("¿Desea regresar?", "ATLAS CORP | REGRESAR", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                frmInicioAdministrador formInicioAdmin = new frmInicioAdministrador();
                this.Hide();
                formInicioAdmin.Show();
            }
        }
        #endregion


        #region Colores del Boton
        private void btnVolver_MouseEnter(object sender, MouseEventArgs e)
        {
            btnVolver.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnVolver_MouseLeave(object sender, MouseEventArgs e)
        {
            btnVolver.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }
        #endregion


    }
}