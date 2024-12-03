using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace GestorInventario.DataAcces
{
    class ConexionDB
    {
        // Se obtiene la cadena de conexion desde setting del proyecto
        public static string ObtenerCadena()
        {
            return Properties.Settings.Default.conexionDB;
        }

        // Se obtiene la conexion
        public static SqlConnection ObtenerCnx()
        {
            SqlConnection connection = new SqlConnection(ObtenerCadena());
            return connection;
        }

        // Metodo para abrir la conexion si esta cerrada
        public static void AbrirConexion(SqlConnection connection)
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        // Metodo para cerrar la conexion
        public static void CerrarConexion(SqlConnection connection)
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}