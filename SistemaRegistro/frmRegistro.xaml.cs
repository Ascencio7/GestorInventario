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
using GestorInventario.DataAcces;
using GestorInventario.ModeloUsuario;
using GestorInventario.SistemaAdministrador;
using GestorInventario.SistemaLogin;


using System.Data;

using GestorInventario.ServiceUsuario;

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
            MostrarUsuarios();
            txtNombreUsuario.Focus();
        }


        #region Metodo Mostrar Usuarios
        void MostrarUsuarios()
        {
            using (SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    ConexionDB.AbrirConexion(conexion);

                    var usuarios = DatosUsuarios.MostrarUsuarios();

                    foreach(var usuario in usuarios )
                    {
                        usuario.Contra = "**********";
                    }

                    gridUsuariosAdmin.ItemsSource = usuarios;

                    txtNombreUsuario.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al mostrar los usuarios: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    LimpiarCampos();
                }
                finally
                {
                    ConexionDB.CerrarConexion(conexion);
                }
            }
        }
        #endregion



        #region Validar Formulario
        public void ValidarFormulario()
        {
            // Validaciones de campos
            if (string.IsNullOrWhiteSpace(txtNombreUsuario.Text))
            {
                MessageBox.Show("El campo 'Nombre de Usuario' es obligatorio.", "ATLAS CORP | CAMPOS VACIOS", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCorreoUsuario.Text))
            {
                MessageBox.Show("El campo 'Correo' es obligatorio.", "ATLAS CORP | CAMPOS VACIOS", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPasswordUsuario.Password))
            {
                MessageBox.Show("El campo 'Contraseña' es obligatorio.", "ATLAS CORP | CAMPOS VACIOS", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (cbRolesUsuarios.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un rol.", "ATLAS CORP | CAMPOS VACIOS", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (cbEstadosUsuarios.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un estado.", "ATLAS CORP | CAMPOS VACIOS", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }
        #endregion



        #region Limpiar Campos
        public void LimpiarCampos()
        {
            txtIDUsuario.Text = "";
            txtNombreUsuario.Text = "";
            txtCorreoUsuario.Text = "";
            txtPasswordUsuario.Clear();
            cbRolesUsuarios.Items.Clear();
            cbEstadosUsuarios.Items.Clear();
        }
        #endregion



        #region Metodo de Registro Usuario
        public string RegistroUsuario(string nombreUsuario, string correo, string password, int rolID, int estadoID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=VLADIMIR\\SQLEXPRESS ;Database=ATLAS_INVENTARIO ;Integrated Security=True;Encrypt=False"))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("spRegistrarUsuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Pasar parámetros al procedimiento almacenado
                        command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                        command.Parameters.AddWithValue("@Correo", correo);
                        command.Parameters.AddWithValue("@Contra", password); // Contraseña sin cifrar, el procedimiento lo hará
                        command.Parameters.AddWithValue("@RolID", rolID);
                        command.Parameters.AddWithValue("@EstadoID", estadoID);

                        command.ExecuteNonQuery();
                    }
                }
                return "Usuario registrado correctamente.";
            }
            catch (Exception ex)
            {
                return $"Se produjo un error inesperado: {ex.Message}";
            }
        }
        #endregion



        #region Botón Registrar Usuario
        private void btnRegistrarse_Click(object sender, RoutedEventArgs e)
        {
            ValidarFormulario();

            // Capturar los valores de los controles
            string nombreUsuario = txtNombreUsuario.Text.Trim();
            string correo = txtCorreoUsuario.Text.Trim();
            string password = txtPasswordUsuario.Password; // PasswordBox
            int rolID = cbRolesUsuarios.SelectedIndex + 1; // Ajustar si los valores son distintos a 1 y 2
            int estadoID = cbEstadosUsuarios.SelectedIndex + 1; // Ajustar si los valores son distintos a 1 y 2

            // Llamar al método de registro
            string resultado = RegistroUsuario(nombreUsuario, correo, password, rolID, estadoID);

            // Mostrar el resultado
            MessageBox.Show(resultado, "ATLAS CORP | USUARIO REGISTRADO", MessageBoxButton.OK, MessageBoxImage.Information);
            MostrarUsuarios();
            LimpiarCampos();
        }
        #endregion



        #region Metodo para Editar Usuario
        public string EditarUsuario(int userID, string nombreUsuario, string correo, string password, int rolID, int estadoID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=VLADIMIR\\SQLEXPRESS ;Database=ATLAS_INVENTARIO ;Integrated Security=True;Encrypt=False"))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("spEditarUsuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Pasar parámetros al procedimiento almacenado
                        command.Parameters.AddWithValue("@UserID", userID);
                        command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                        command.Parameters.AddWithValue("@Correo", correo);
                        command.Parameters.AddWithValue("@Contra", password); // Contraseña sin cifrar
                        command.Parameters.AddWithValue("@RolID", rolID);
                        command.Parameters.AddWithValue("@EstadoID", estadoID);

                        // Ejecutar el procedimiento
                        command.ExecuteNonQuery();
                    }
                }

                return "Usuario editado exitosamente.";
            }
            catch (SqlException ex)
            {
                // Devuelve el mensaje de error del servidor SQL
                return $"Error al editar el usuario: {ex.Message}";
            }
            catch (Exception ex)
            {
                // Devuelve un mensaje de error general
                return $"Se produjo un error inesperado: {ex.Message}";
            }
        }
        #endregion



        #region Botón Editar Usuario
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            // Validaciones de campos
            ValidarFormulario();

            // Capturar los valores de los controles
            int usuarioID = int.Parse(txtIDUsuario.Text);
            string nombreUsuario = txtNombreUsuario.Text.Trim();
            string correo = txtCorreoUsuario.Text.Trim();
            string password = txtPasswordUsuario.Password; // PasswordBox
            int rolID = cbRolesUsuarios.SelectedIndex + 1;
            int estadoID = cbEstadosUsuarios.SelectedIndex + 1;

            // Llamar al método de edición
            string resultado = EditarUsuario(usuarioID, nombreUsuario, correo, password, rolID, estadoID);

            // Mostrar el resultado
            MessageBox.Show(resultado, "ATLAS CORP | USUARIO EDITADO", MessageBoxButton.OK, MessageBoxImage.Information);
            MostrarUsuarios();
            LimpiarCampos();
            
        }
        #endregion



        #region Evento del GRID
        private void gridUsuariosAdmin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UsuariosModel usuarios = (UsuariosModel)gridUsuariosAdmin.SelectedItem;

            if(usuarios == null) return;

            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=VLADIMIR\\SQLEXPRESS ;Database=ATLAS_INVENTARIO ;Integrated Security=True;Encrypt=False"))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("spObtenerUsuarioContrasenaDesencriptada", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("UserID", usuarios.UserID);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                txtIDUsuario.Text = dr["UserID"].ToString();
                                txtNombreUsuario.Text = dr["NombreUsuario"].ToString();
                                txtCorreoUsuario.Text = dr["Correo"].ToString();
                                txtPasswordUsuario.Password = dr["ContrasenaDesencriptada"].ToString();
                                cbRolesUsuarios.Text = dr["RolID"].ToString();
                                cbEstadosUsuarios.Text = dr["EstadoID"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("No se encontraron resultados para el usuario seleccionado.", "ATLAS CORP | NO HAY DATOS DE USUARIO", MessageBoxButton.OK, MessageBoxImage.Warning);
                                LimpiarCampos();
                                MostrarUsuarios();
                            }
                        }
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos del usuario: {ex.Message}", "ATLAS CORP | ERROR AL CARGAR LOS DATOS", MessageBoxButton.OK, MessageBoxImage.Error);
                LimpiarCampos();
                MostrarUsuarios();
            }
        }
        #endregion



        #region Botón Cancelar
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("¿Desea cancelar la operación?", "ATLAS CORP | CANCELAR", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                LimpiarCampos();
                MostrarUsuarios();
            }
        }
        #endregion


        #region Botón de Volver
        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea regresar al menú principal de Administrador?", "ATLAS CORP | REGRESAR", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                frmInicioAdministrador inicioAdmin = new frmInicioAdministrador();
                this.Hide();
                inicioAdmin.Show();
            }
        }
        #endregion



        #region Colores de Botones
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

        private void btnEditar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnEditar.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnEditar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnEditar.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        private void btnCancelar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnCancelar.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnCancelar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnCancelar.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }
        #endregion



    }
}