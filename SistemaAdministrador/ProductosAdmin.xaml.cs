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



using GestorInventario.DataAcces;
using GestorInventario.ModeloProducto;
using GestorInventario.ServiceProducto;
using GestorInventario.ModeloProveedor;

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
            MostrarLosProductos();
            txtNombreProductoAdmin.Focus();
        }


        #region Colores Botones
        private void btnRegresarAdmin_MouseEnter(object sender, MouseEventArgs e)
        {
            btnRegresarAdmin.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnRegresarAdmin_MouseLeave(object sender, MouseEventArgs e)
        {
            btnRegresarAdmin.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        private void btnRegistrarProductoAdmin_MouseEnter(object sender, MouseEventArgs e)
        {
            btnRegistrarProductoAdmin.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnRegistrarProductoAdmin_MouseLeave(object sender, MouseEventArgs e)
        {
            btnRegistrarProductoAdmin.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        private void btnEditarProductoAdmin_MouseEnter(object sender, MouseEventArgs e)
        {
            btnEditarProductoAdmin.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnEditarProductoAdmin_MouseLeave(object sender, MouseEventArgs e)
        {
            btnEditarProductoAdmin.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        private void btnCancelarProductoAdmin_MouseEnter(object sender, MouseEventArgs e)
        {
            btnCancelarProductoAdmin.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnCancelarProductoAdmin_MouseLeave(object sender, MouseEventArgs e)
        {
            btnCancelarProductoAdmin.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        #endregion



        #region Limpiar los campos
        void limpiar()
        {
            txtProductoIDAdmin.Text = "";
            txtNombreProductoAdmin.Text = "";
            txtPrecioProductoAdmin.Text = "";
            txtCantidadProductoAdmin.Text = "";
            txtNombreProveedorAdmin.Text = "";
        }
        #endregion



        #region Mostrar Productos
        void MostrarLosProductos()
        {
            using(SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    ConexionDB.AbrirConexion(conexion);
                    var productos = DatosProductos.MostrarProductos();
                    gridProductosAdmin.ItemsSource = productos;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al mostrar los productos: " + ex.Message, "ATLAS CORP | ERROR AL CARGAR LOS DATOS", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally 
                { 
                    ConexionDB.CerrarConexion(conexion);
                }
            }
        }
        #endregion



        #region Boton Regresar
        private void btnRegresarAdmin_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("¿Desea regresar al menú principal del Administrador?", "ATLAS CORP | REGRESANDO AL MENÚ PRINCIPAL", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                frmInicioAdministrador formPrincipal = new frmInicioAdministrador();
                this.Hide();
                formPrincipal.Show();
            }
        }
        #endregion



        #region Evento del GRID
        private void gridProductosAdmin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductosModel productos = (ProductosModel)gridProductosAdmin.SelectedItem;
            if (productos == null) return;

            try
            {
                // Asignar los valores a los controles
                txtProductoIDAdmin.Text = productos.ProductoID.ToString();
                txtNombreProductoAdmin.Text = productos.Nombre_Producto;
                txtPrecioProductoAdmin.Text = productos.Precio_Producto.ToString("F2");
                txtCantidadProductoAdmin.Text = productos.Cantidad_Producto.ToString();
                txtNombreProveedorAdmin.Text = productos.Proveedor.ToString();
                
                
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error de formato: " + ex.Message, "ATLAS CORP | ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "ATLAS CORP | ERROR AL CARGAR LOS DATOS", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion



        #region Boton de Cancelar
        private void btnCancelarProductoAdmin_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar la operación?", "ATLAS CORP | CANCELAR LA OPERACIÓN", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                limpiar();
                MostrarLosProductos();
            }
        }
        #endregion



        #region Metodo Insertar Producto
        public void InsertarProducto(string nombreProducto, decimal precioProducto, int cantidadProducto, string proveedor)
        {
            string connectionString = "Data Source=VLADIMIR\\SQLEXPRESS;Database=ATLAS_INVENTARIO;Integrated Security=True;Encrypt=False";

            // Establece la conexión con la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "InsertarProducto"; // Nombre del procedimiento almacenado
                SqlCommand command = new SqlCommand(query, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Agregar parámetros
                command.Parameters.AddWithValue("@Nombre_Producto", nombreProducto);
                command.Parameters.AddWithValue("@Precio_Producto", precioProducto);
                command.Parameters.AddWithValue("@Cantidad_Producto", cantidadProducto);
                command.Parameters.AddWithValue("@Proveedor", proveedor);

                try
                {
                    // Abrir conexión y ejecutar el comando
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Captura de errores
                    MessageBox.Show($"Error al insertar el producto: {ex.Message}");
                }
            }
        }
        #endregion



        #region Boton Insertar Productos
        private void btnRegistrarProductoAdmin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string nombreProducto = txtNombreProductoAdmin.Text.Trim();
                decimal precioProducto = Convert.ToDecimal(txtPrecioProductoAdmin.Text);
                int cantidadProducto = Convert.ToInt32(txtCantidadProductoAdmin.Text);
                string proveedor = txtNombreProveedorAdmin.Text.Trim();

                InsertarProducto(nombreProducto,precioProducto,cantidadProducto,proveedor);

                MessageBox.Show("Producto registrado exitosamente.", "ATLAS CORP | PRODUCTO REGISTRADO", MessageBoxButton.OK, MessageBoxImage.Information);
                MostrarLosProductos();
                limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar producto: {ex.Message}", "ATLAS CORP | ERROR AL REGISTRAR EL PRODUCTO", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion



        #region Metodo Editar Producto
        public void EditarProducto(int productoID, string nombreProducto, decimal precioProducto, int cantidadProducto, string proveedor)
        {
            string connectionString = "Data Source=VLADIMIR\\SQLEXPRESS;Database=ATLAS_INVENTARIO;Integrated Security=True;Encrypt=False";

            // Establece la conexión con la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "EditarProducto"; // Nombre del procedimiento almacenado
                SqlCommand command = new SqlCommand(query, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Agregar parámetros
                command.Parameters.AddWithValue("@ProductoID", productoID);
                command.Parameters.AddWithValue("@Nombre_Producto", nombreProducto);
                command.Parameters.AddWithValue("@Precio_Producto", precioProducto);
                command.Parameters.AddWithValue("@Cantidad_Producto", cantidadProducto);
                command.Parameters.AddWithValue("@Proveedor", proveedor);

                try
                {
                    // Abrir conexión y ejecutar el comando
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Producto actualizado exitosamente.", "ATLAS CORP | PRODUCTO MODIFICADO CORRECTAMENTE", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    // Captura de errores
                    MessageBox.Show($"Error al editar el producto: {ex.Message}", "ATLAS CORP | ERROR AL MODIFICAR EL PRODUCTO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion



        #region Boton Editar Producto
        private void btnEditarProductoAdmin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int productoID = Convert.ToInt32(txtProductoIDAdmin.Text);
                string nombreProducto = txtNombreProductoAdmin.Text.Trim();
                decimal precioProducto = Convert.ToDecimal(txtPrecioProductoAdmin.Text);
                int cantidadProducto = Convert.ToInt32(txtCantidadProductoAdmin.Text);
                string proveedor = txtNombreProveedorAdmin.Text.Trim();

                // Llamar al método de edición
                EditarProducto(productoID, nombreProducto, precioProducto, cantidadProducto, proveedor);

                MessageBox.Show("Producto actualizado exitosamente.", "ATLAS CORP | PRODUCTO MODIFICADO CORRECTAMENTE", MessageBoxButton.OK, MessageBoxImage.Information);
                MostrarLosProductos();
                limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al editar producto: {ex.Message}", "ATLAS CORP | ERROR AL MODIFICAR EL PRODUCTO", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion



        #region Eventos ENTER
        private void txtNombreProductoAdmin_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                try
                {
                    // Recopilar datos de los controles de la interfaz
                    string nombreProducto = txtNombreProductoAdmin.Text.Trim();
                    decimal precioProducto = Convert.ToDecimal(txtPrecioProductoAdmin.Text);
                    int cantidadProducto = Convert.ToInt32(txtCantidadProductoAdmin.Text);
                    string proveedor = txtNombreProveedorAdmin.Text.Trim();

                    // Llamar al método InsertarProducto
                    InsertarProducto(nombreProducto, precioProducto, cantidadProducto, proveedor);

                    // Mostrar un mensaje de éxito
                    MessageBox.Show("Producto registrado exitosamente.", "ATLAS CORP | PRODUCTO REGISTRADO", MessageBoxButton.OK, MessageBoxImage.Information);

                    MostrarLosProductos();
                    limpiar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al registrar producto: {ex.Message}", "ATLAS CORP | ERROR AL REGISTRAR EL PRODUCTO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void txtPrecioProductoAdmin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    // Recopilar datos de los controles de la interfaz
                    string nombreProducto = txtNombreProductoAdmin.Text.Trim();
                    decimal precioProducto = Convert.ToDecimal(txtPrecioProductoAdmin.Text);
                    int cantidadProducto = Convert.ToInt32(txtCantidadProductoAdmin.Text);
                    string proveedor = txtNombreProveedorAdmin.Text.Trim();

                    // Llamar al método InsertarProducto
                    InsertarProducto(nombreProducto, precioProducto, cantidadProducto, proveedor);

                    // Mostrar un mensaje de éxito
                    MessageBox.Show("Producto registrado exitosamente.", "ATLAS CORP | PRODUCTO REGISTRADO", MessageBoxButton.OK, MessageBoxImage.Information);

                    MostrarLosProductos();
                    limpiar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al registrar producto: {ex.Message}", "ATLAS CORP | ERROR AL REGISTRAR EL PRODUCTO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void txtCantidadProductoAdmin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    // Recopilar datos de los controles de la interfaz
                    string nombreProducto = txtNombreProductoAdmin.Text.Trim();
                    decimal precioProducto = Convert.ToDecimal(txtPrecioProductoAdmin.Text);
                    int cantidadProducto = Convert.ToInt32(txtCantidadProductoAdmin.Text);
                    string proveedor = txtNombreProveedorAdmin.Text.Trim();

                    // Llamar al método InsertarProducto
                    InsertarProducto(nombreProducto, precioProducto, cantidadProducto, proveedor);

                    // Mostrar un mensaje de éxito
                    MessageBox.Show("Producto registrado exitosamente.", "ATLAS CORP | PRODUCTO REGISTRADO", MessageBoxButton.OK, MessageBoxImage.Information);

                    MostrarLosProductos();
                    limpiar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al registrar producto: {ex.Message}", "ATLAS CORP | ERROR AL REGISTRAR EL PRODUCTO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void txtNombreProveedorAdmin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    // Recopilar datos de los controles de la interfaz
                    string nombreProducto = txtNombreProductoAdmin.Text.Trim();
                    decimal precioProducto = Convert.ToDecimal(txtPrecioProductoAdmin.Text);
                    int cantidadProducto = Convert.ToInt32(txtCantidadProductoAdmin.Text);
                    string proveedor = txtNombreProveedorAdmin.Text.Trim();

                    // Llamar al método InsertarProducto
                    InsertarProducto(nombreProducto, precioProducto, cantidadProducto, proveedor);

                    // Mostrar un mensaje de éxito
                    MessageBox.Show("Producto registrado exitosamente.", "ATLAS CORP | PRODUCTO REGISTRADO", MessageBoxButton.OK, MessageBoxImage.Information);

                    MostrarLosProductos();
                    limpiar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al registrar producto: {ex.Message}", "ATLAS CORP | ERROR AL REGISTRAR EL PRODUCTO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion



    }
}