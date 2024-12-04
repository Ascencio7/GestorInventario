using GestorInventario.DataAcces;
using GestorInventario.ModeloUsuario;
using GestorInventario.ServiceUsuario;
using System;
using System.Collections.Generic;
using System.Data;
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


        #region Colores del Boton
        private void btnVolver_MouseEnter(object sender, MouseEventArgs e)
        {
            btnVolver.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnVolver_MouseLeave(object sender, MouseEventArgs e)
        {
            btnVolver.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        private void btnActivarUsuarioAdmin_MouseEnter(object sender, MouseEventArgs e)
        {
            btnActivarUsuarioAdmin.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnActivarUsuarioAdmin_MouseLeave(object sender, MouseEventArgs e)
        {
            btnActivarUsuarioAdmin.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        private void btnCancelarActivarUsuarioAdmin_MouseEnter(object sender, MouseEventArgs e)
        {
            btnCancelarActivarUsuarioAdmin.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnCancelarActivarUsuarioAdmin_MouseLeave(object sender, MouseEventArgs e)
        {
            btnCancelarActivarUsuarioAdmin.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        #endregion



        #region Limpiar Campos
        void limpiarCampos()
        {
            txtIDUsuarioInactivoAdmin.Text = "";
            txtNombreUsuarioInactivoAdmin.Text = "";
            txtCorreoUsuarioInactivoAdmin.Text = "";
            txtContraUsuarioInactivoAdmin.Text = "";
            txtRolUsuarioInactivoAdmin.Text = "";
            txtEstadoUsuarioInactivoAdmin.Text = "";
        }
        #endregion



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
                    MessageBox.Show("Ocurrió un error al mostrar los usuarios: " + ex.Message, "ATLAS CORP | ERROR AL CARGAR LOS USUARIOS", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    ConexionDB.CerrarConexion(conexion);
                }
            }
        }
        #endregion



        #region Evento GRID
        private void gridUsuariosInactivosAdmin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UsuariosModel usuarios = (UsuariosModel)gridUsuariosInactivosAdmin.SelectedItem;

            if (usuarios == null) return;

            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=VLADIMIR\\SQLEXPRESS ;Database=ATLAS_INVENTARIO ;Integrated Security=True;Encrypt=False"))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("ObtenerUsuarioContrasenaDesencriptada", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("UserID", usuarios.UserID);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                txtIDUsuarioInactivoAdmin.Text = dr["UserID"].ToString();
                                txtNombreUsuarioInactivoAdmin.Text = dr["NombreUsuario"].ToString();
                                txtCorreoUsuarioInactivoAdmin.Text = dr["Correo"].ToString();
                                txtContraUsuarioInactivoAdmin.Text = dr["ContrasenaDesencriptada"].ToString();
                                txtRolUsuarioInactivoAdmin.Text = dr["Rol"].ToString();
                                txtEstadoUsuarioInactivoAdmin.Text = dr["Estado"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("No se encontraron resultados para el usuario seleccionado.", "ATLAS CORP  | SIN DATOS QUE MOSTRAR", MessageBoxButton.OK, MessageBoxImage.Warning);
                                limpiarCampos();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos del usuario: {ex.Message}", "ATLAS CORP | ERROR AL CARGAR LOS DATOS", MessageBoxButton.OK, MessageBoxImage.Error);
                limpiarCampos();
            }
        }
        #endregion



        #region Boton Volver
        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea regresar al menú principal de Administrador?", "ATLAS CORP | REGRESAR AL MENÚ PRINCIPAL", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                frmInicioAdministrador formInicioAdmin = new frmInicioAdministrador();
                this.Hide();
                formInicioAdmin.Show();
            }
        }
        #endregion



        #region Botón Activar Usuario
        private void btnActivarUsuarioAdmin_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el usuario seleccionado desde el DataGrid
            UsuariosModel usuarioSeleccionado = (UsuariosModel)gridUsuariosInactivosAdmin.SelectedItem;

            // Validar que haya un usuario seleccionado
            if (usuarioSeleccionado == null)
            {
                MessageBox.Show("Por favor, selecciona un usuario para activar.", "ATLAS CORP | DATOS NO SELECCIONADOS", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=VLADIMIR\\SQLEXPRESS;Database=ATLAS_INVENTARIO;Integrated Security=True;Encrypt=False"))
                {
                    connection.Open();

                    // Llamar al procedimiento almacenado para activar al usuario
                    using (SqlCommand cmd = new SqlCommand("ActivarUsuario", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", usuarioSeleccionado.UserID);

                        // Ejecutar la consulta
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Usuario activado correctamente.", "ATLAS CORP | USUARIO ACTIVADO", MessageBoxButton.OK, MessageBoxImage.Information);
                            MostrarUsuariosInactivos();
                            limpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo activar el usuario. Inténtalo de nuevo.", "ATLAS CORP | ERROR AL ACTIVAR EL USUARIO", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al activar el usuario: {ex.Message}", "ATLAS CORP | ERROR AL ACTIVAR EL USUARIO", MessageBoxButton.OK, MessageBoxImage.Error);
                MostrarUsuariosInactivos();
                limpiarCampos();
            }
        }
        #endregion



        #region Botón Cancelar
        private void btnCancelarActivarUsuarioAdmin_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("¿Desea cancelar la activación del usuario seleccionado?", "ATLAS CORP | CANCELAR LA OPERACIÓN", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                limpiarCampos();
                MostrarUsuariosInactivos();
            }
        }
        #endregion


    }
}