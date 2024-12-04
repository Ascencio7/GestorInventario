using GestorInventario.DataAcces;
using GestorInventario.ModeloProducto;
using GestorInventario.ServiceProducto;
using GestorInventario.SistemaAdministrador;
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

namespace GestorInventario.SistemaAuxiliar
{
    /// <summary>
    /// Lógica de interacción para ProductosAuxiliar.xaml
    /// </summary>
    public partial class ProductosAuxiliar : Window
    {
        public ProductosAuxiliar()
        {
            InitializeComponent();
            MostrarLosProductosAuxiliar();
            txtNombreProductoAuxiliar.Focus();
        }



        #region Limpiar los campos
        void limpiar()
        {
            txtProductoIDAuxiliar .Text = "";
            txtNombreProductoAuxiliar.Text = "";
            txtPrecioProductoAuxiliar.Text = "";
            txtCantidadProductoAuxiliar.Text = "";
            txtNombreProveedorAuxiliar.Text = "";
        }
        #endregion


        #region Colores de Botones
        private void btnRegresarAuxiliar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnRegresarAuxiliar.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnRegresarAuxiliar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnRegresarAuxiliar.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        private void btnRegistrarProductoAuxiliar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnRegistrarProductoAuxiliar.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnRegistrarProductoAuxiliar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnRegistrarProductoAuxiliar.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        private void btnEditarProductoAuxiliar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnEditarProductoAuxiliar.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnEditarProductoAuxiliar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnEditarProductoAuxiliar.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }

        private void btnCancelarProductoAuxiliar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnCancelarProductoAuxiliar.Background = new SolidColorBrush(Color.FromRgb(41, 16, 153));
        }

        private void btnCancelarProductoAuxiliar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnCancelarProductoAuxiliar.Background = new SolidColorBrush(Color.FromRgb(5, 135, 137)); // Color original
        }
        #endregion



        #region Mostrar Productos
        void MostrarLosProductosAuxiliar()
        {
            using (SqlConnection conexion = ConexionDB.ObtenerCnx())
            {
                try
                {
                    ConexionDB.AbrirConexion(conexion);
                    var productos = DatosProductos.MostrarProductos();
                    gridProductosAuxiliar.ItemsSource = productos;
                }
                catch (Exception ex)
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



        #region Botón de Regresar
        private void btnRegresarAuxiliar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea regresar al menú principal del Auxiliar?", "ATLAS CORP | REGRESAR", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                frmInicioAuxiliar frmInicioAuxiliar = new frmInicioAuxiliar();
                this.Hide();
                frmInicioAuxiliar.Show();
            }
        }
        #endregion



        #region Evento del GRID
        private void gridProductosAuxiliar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductosModel productos = (ProductosModel)gridProductosAuxiliar.SelectedItem;
            if (productos == null) return;

            try
            {
                // Asignar los valores a los controles
                txtProductoIDAuxiliar.Text = productos.ProductoID.ToString();
                txtNombreProductoAuxiliar.Text = productos.Nombre_Producto;
                txtPrecioProductoAuxiliar.Text = productos.Precio_Producto.ToString("F2");
                txtCantidadProductoAuxiliar.Text = productos.Cantidad_Producto.ToString();
                txtNombreProveedorAuxiliar.Text = productos.Proveedor.ToString();


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



        #region Botón de Cancelar
        private void btnCancelarProductoAuxiliar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar la operación?", "ATLAS CORP | PRODUCTOS", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                limpiar();
                MostrarLosProductosAuxiliar();
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



        #region Botón de Registrar Productos
        private void btnRegistrarProductoAuxiliar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string nombreProducto = txtNombreProductoAuxiliar.Text.Trim();
                decimal precioProducto = Convert.ToDecimal(txtPrecioProductoAuxiliar.Text);
                int cantidadProducto = Convert.ToInt32(txtCantidadProductoAuxiliar.Text);
                string proveedor = txtNombreProveedorAuxiliar.Text.Trim();

                InsertarProducto(nombreProducto, precioProducto, cantidadProducto, proveedor);

                MessageBox.Show("Producto registrado exitosamente.", "ATLAS CORP | PRODUCTO REGISTRADO", MessageBoxButton.OK, MessageBoxImage.Information);
                MostrarLosProductosAuxiliar();
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
                    MessageBox.Show($"Error al editar el producto: {ex.Message}", "ATLAS CORP | ERROR AL EDITAR EL PRODUCTO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion



        #region Botón de Editar Productos
        private void btnEditarProductoAuxiliar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int productoID = Convert.ToInt32(txtProductoIDAuxiliar.Text);
                string nombreProducto = txtNombreProductoAuxiliar.Text.Trim();
                decimal precioProducto = Convert.ToDecimal(txtPrecioProductoAuxiliar.Text);
                int cantidadProducto = Convert.ToInt32(txtCantidadProductoAuxiliar.Text);
                string proveedor = txtNombreProveedorAuxiliar.Text.Trim();

                // Llamar al método de edición
                EditarProducto(productoID, nombreProducto, precioProducto, cantidadProducto, proveedor);

                MessageBox.Show("Producto actualizado exitosamente.", "ATLAS CORP | PRODUCTO MODIFICADO CORRECTAMENTE", MessageBoxButton.OK, MessageBoxImage.Information);
                MostrarLosProductosAuxiliar();
                limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al editar producto: {ex.Message}", "ATLAS CORP | ERROR AL EDITAR EL PRODUCTO", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion



        #region Eventos ENTER
        private void txtNombreProductoAuxiliar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    // Recopilar datos de los controles de la interfaz
                    string nombreProducto = txtNombreProductoAuxiliar.Text.Trim();
                    decimal precioProducto = Convert.ToDecimal(txtPrecioProductoAuxiliar.Text);
                    int cantidadProducto = Convert.ToInt32(txtCantidadProductoAuxiliar.Text);
                    string proveedor = txtNombreProveedorAuxiliar.Text.Trim();

                    // Llamar al método InsertarProducto
                    InsertarProducto(nombreProducto, precioProducto, cantidadProducto, proveedor);

                    // Mostrar un mensaje de éxito
                    MessageBox.Show("Producto registrado exitosamente.", "ATLAS CORP | PRODUCTO REGISTRADO", MessageBoxButton.OK, MessageBoxImage.Information);

                    MostrarLosProductosAuxiliar();
                    limpiar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al registrar producto: {ex.Message}", "ATLAS CORP | ERROR AL REGISTRAR EL PRODUCTO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void txtPrecioProductoAuxiliar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    // Recopilar datos de los controles de la interfaz
                    string nombreProducto = txtNombreProductoAuxiliar.Text.Trim();
                    decimal precioProducto = Convert.ToDecimal(txtPrecioProductoAuxiliar.Text);
                    int cantidadProducto = Convert.ToInt32(txtCantidadProductoAuxiliar.Text);
                    string proveedor = txtNombreProveedorAuxiliar.Text.Trim();

                    // Llamar al método InsertarProducto
                    InsertarProducto(nombreProducto, precioProducto, cantidadProducto, proveedor);

                    // Mostrar un mensaje de éxito
                    MessageBox.Show("Producto registrado exitosamente.", "ATLAS CORP | PRODUCTO REGISTRADO", MessageBoxButton.OK, MessageBoxImage.Information);

                    MostrarLosProductosAuxiliar();
                    limpiar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al registrar producto: {ex.Message}", "ATLAS CORP | ERROR AL REGISTRAR EL PRODUCTO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void txtCantidadProductoAuxiliar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    // Recopilar datos de los controles de la interfaz
                    string nombreProducto = txtNombreProductoAuxiliar.Text.Trim();
                    decimal precioProducto = Convert.ToDecimal(txtPrecioProductoAuxiliar.Text);
                    int cantidadProducto = Convert.ToInt32(txtCantidadProductoAuxiliar.Text);
                    string proveedor = txtNombreProveedorAuxiliar.Text.Trim();

                    // Llamar al método InsertarProducto
                    InsertarProducto(nombreProducto, precioProducto, cantidadProducto, proveedor);

                    // Mostrar un mensaje de éxito
                    MessageBox.Show("Producto registrado exitosamente.", "ATLAS CORP | PRODUCTO REGISTRADO", MessageBoxButton.OK, MessageBoxImage.Information);

                    MostrarLosProductosAuxiliar();
                    limpiar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al registrar producto: {ex.Message}", "ATLAS CORP | ERROR AL REGISTRAR EL PRODUCTO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void txtNombreProveedorAuxiliar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    // Recopilar datos de los controles de la interfaz
                    string nombreProducto = txtNombreProductoAuxiliar.Text.Trim();
                    decimal precioProducto = Convert.ToDecimal(txtPrecioProductoAuxiliar.Text);
                    int cantidadProducto = Convert.ToInt32(txtCantidadProductoAuxiliar.Text);
                    string proveedor = txtNombreProveedorAuxiliar.Text.Trim();

                    // Llamar al método InsertarProducto
                    InsertarProducto(nombreProducto, precioProducto, cantidadProducto, proveedor);

                    // Mostrar un mensaje de éxito
                    MessageBox.Show("Producto registrado exitosamente.", "ATLAS CORP | PRODUCTO REGISTRADO", MessageBoxButton.OK, MessageBoxImage.Information);

                    MostrarLosProductosAuxiliar();
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