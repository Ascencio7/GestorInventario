﻿using System;
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





        #endregion


        #region Limpiar Campos

        public void LimpiarCampos()
        {
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

        private void btnRegistrarse_Click(object sender, RoutedEventArgs e)
        {
            // Validaciones de campos
            if (string.IsNullOrWhiteSpace(txtNombreUsuario.Text))
            {
                MessageBox.Show("El campo 'Nombre de Usuario' es obligatorio.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCorreoUsuario.Text))
            {
                MessageBox.Show("El campo 'Correo' es obligatorio.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPasswordUsuario.Password))
            {
                MessageBox.Show("El campo 'Contraseña' es obligatorio.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (cbRolesUsuarios.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un rol.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (cbEstadosUsuarios.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un estado.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Capturar los valores de los controles
            string nombreUsuario = txtNombreUsuario.Text.Trim();
            string correo = txtCorreoUsuario.Text.Trim();
            string password = txtPasswordUsuario.Password; // PasswordBox
            int rolID = cbRolesUsuarios.SelectedIndex + 1; // Ajustar si los valores son distintos a 1 y 2
            int estadoID = cbEstadosUsuarios.SelectedIndex + 1; // Ajustar si los valores son distintos a 1 y 2

            // Llamar al método de registro
            string resultado = RegistroUsuario(nombreUsuario, correo, password, rolID, estadoID);

            // Mostrar el resultado
            MessageBox.Show(resultado, "Resultado", MessageBoxButton.OK, MessageBoxImage.Information);
            LimpiarCampos();
            MostrarUsuarios();  
        }



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

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            // Validaciones de campos
            if (string.IsNullOrWhiteSpace(txtNombreUsuario.Text))
            {
                MessageBox.Show("El campo 'Nombre de Usuario' es obligatorio.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCorreoUsuario.Text))
            {
                MessageBox.Show("El campo 'Correo' es obligatorio.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPasswordUsuario.Password))
            {
                MessageBox.Show("El campo 'Contraseña' es obligatorio.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (cbRolesUsuarios.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un rol.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (cbEstadosUsuarios.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un estado.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Capturar los valores de los controles
            int usuarioID = int.Parse(""); // Asume que tienes un campo para capturar el ID del usuario
            string nombreUsuario = txtNombreUsuario.Text.Trim();
            string correo = txtCorreoUsuario.Text.Trim();
            string password = txtPasswordUsuario.Password; // PasswordBox
            int rolID = cbRolesUsuarios.SelectedIndex + 1; // Ajustar según los valores de tu ComboBox
            int estadoID = cbEstadosUsuarios.SelectedIndex + 1; // Ajustar según los valores de tu ComboBox

            // Llamar al método de edición
            string resultado = EditarUsuario(userID, nombreUsuario, correo, password, rolID, estadoID);

            // Mostrar el resultado
            MessageBox.Show(resultado, "Resultado", MessageBoxButton.OK, MessageBoxImage.Information);
            LimpiarCampos();
        }

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
                                txtNombreUsuario.Text = dr["NombreUsuario"].ToString();
                                txtCorreoUsuario.Text = dr["Correo"].ToString();
                                txtPasswordUsuario.Password = dr["ContrasenaDesencriptada"].ToString();
                                cbRolesUsuarios.Text = dr["RolID"].ToString();
                                cbEstadosUsuarios.Text = dr["EstadoID"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("No se encontraron resultados para el usuario seleccionado.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                                LimpiarCampos();
                            }
                        }
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos del usuario: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                LimpiarCampos();
            }
        }
    }
}