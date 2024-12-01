using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GestorInventario.DataAcces;
using System.Windows.Controls;

using GestorInventario.ModeloProducto;

namespace GestorInventario.ServiceProducto
{
    public class DatosProductos
    {
        public DatosProductos() { }

        public static List<ProductosModel> MostrarProductos()
        {
            List<ProductosModel> lstProductos = new List<ProductosModel>();
            SqlConnection conexion = null;

            try
            {
                conexion = ConexionDB.ObtenerCnx();
                ConexionDB.AbrirConexion(conexion);

                using (var command = conexion.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "MostrarProductos";

                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read()) // Iterar sobre cada fila del lector
                        {
                            ProductosModel productosModel = new ProductosModel()
                            {
                                ProductoID = int.Parse(dr["ProductoID"].ToString()),
                                Nombre_Producto = dr["Nombre_Producto"].ToString(),
                                Precio_Producto = decimal.Parse(dr["Precio_Producto"].ToString()),
                                Cantidad_Producto = int.Parse(dr["Cantidad_Producto"].ToString()),
                                Proveedor = dr["Proveedor"].ToString(),
                                Total_Producto = decimal.Parse(dr["Total_Producto"].ToString())
                            };
                            lstProductos.Add(productosModel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar los datos: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close(); // Cerrar la conexión
                }
            }
            return lstProductos;
        }
    }
}