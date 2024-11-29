using GestorInventario.ModeloUsuario;

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




namespace GestorInventario.ServiceUsuario
{
    public class DatosUsuarios
    {
        public DatosUsuarios() { }



        #region CN a BD MUA
        public static List<UsuariosModel> MostrarUsuarios()
        {
            List<UsuariosModel> lstUsuarios = new List<UsuariosModel>();
            SqlConnection conexion = null;

            try
            {
                conexion = ConexionDB.ObtenerCnx();
                ConexionDB.AbrirConexion(conexion);

                using(var command = conexion.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "MostrarUsuarios";

                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            UsuariosModel modeloUsuario = new UsuariosModel()
                            {
                                UserID = int.Parse(dr["UserID"].ToString()),
                                NombreUsuario = dr["NombreUsuario"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Contra = dr["Contra"].ToString(),
                                Rol = dr["Rol"].ToString(),
                                Estado = dr["Estado"].ToString()

                            };

                            lstUsuarios.Add(modeloUsuario);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar usuarios: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close(); // Cerrar la conexión
                }
            }
            return lstUsuarios;
        }
        #endregion


        #region CN a BD MUI
        public static List<UsuariosModel> MostrarUsuariosInactivos()
        {
            List<UsuariosModel> lstUsuarios = new List<UsuariosModel>();
            SqlConnection conexion = null;

            try
            {
                conexion = ConexionDB.ObtenerCnx();
                ConexionDB.AbrirConexion(conexion);

                using (var command = conexion.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "MostrarUsuariosInactivos";

                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            UsuariosModel modeloUsuario = new UsuariosModel()
                            {
                                UserID = int.Parse(dr["UserID"].ToString()),
                                NombreUsuario = dr["NombreUsuario"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Contra = dr["Contra"].ToString(),
                                Rol = dr["Rol"].ToString(),
                                Estado = dr["Estado"].ToString()

                            };

                            lstUsuarios.Add(modeloUsuario);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar usuarios: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close(); // Cerrar la conexión
                }
            }
            return lstUsuarios;
        }
        #endregion


    }
}