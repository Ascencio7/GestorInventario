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
            if(MessageBox.Show("¿Desea regresar al menú principal del Administrador?", "ATLAS CORP | REGRESAR", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
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
            if (MessageBox.Show("¿Desea cancelar la operación?", "ATLAS CORP | PRODUCTOS", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                limpiar();
                MostrarLosProductos();
            }
        }


        #endregion


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




    }
}