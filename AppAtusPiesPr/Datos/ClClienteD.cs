using AppAtusPiesPr.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace AppAtusPiesPr.Datos
{
    public class ClClienteD
    {
        private ClConexion conexion = new ClConexion();

        public int MtdRegistrarCliente(ClUsuarioE cliente)
        {
            int idCliente = 0;
            try
            {
                using (SqlConnection con = conexion.MtdAbrirConexion())
                using (SqlCommand cmd = new SqlCommand("spRegistrarCliente", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@documento", cliente.Documento);
                    cmd.Parameters.AddWithValue("@nombres", cliente.Nombres);
                    cmd.Parameters.AddWithValue("@apellidos", cliente.Apellidos);
                    cmd.Parameters.AddWithValue("@email", cliente.Email);
                    cmd.Parameters.AddWithValue("@password", cliente.Password);
                    cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                    cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);

                    idCliente = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar cliente: " + ex.Message);
            }
            return idCliente;
        }

        public ClUsuarioE MtdIngreso(ClUsuarioE objDatos)
        {
            ClConexion objConexion = new ClConexion();
            SqlCommand comando = new SqlCommand("SpValidarLogin", objConexion.MtdAbrirConexion())
            {
                CommandType = CommandType.StoredProcedure
            };

            // Agregar parámetros de entrada
            comando.Parameters.AddWithValue("@documento", objDatos.Documento);
            comando.Parameters.AddWithValue("@password", objDatos.Password);

            // Configurar parámetros de salida
            SqlParameter rolParameter = new SqlParameter("@rol", SqlDbType.VarChar, 50)
            {
                Direction = ParameterDirection.Output
            };
            comando.Parameters.Add(rolParameter);

            SqlParameter nombresParameter = new SqlParameter("@nombres", SqlDbType.VarChar, 100)
            {
                Direction = ParameterDirection.Output
            };
            comando.Parameters.Add(nombresParameter);

            SqlParameter apellidosParameter = new SqlParameter("@apellidos", SqlDbType.VarChar, 100)
            {
                Direction = ParameterDirection.Output
            };
            comando.Parameters.Add(apellidosParameter);

            SqlParameter mensajeParameter = new SqlParameter("@mensaje", SqlDbType.VarChar, 255)
            {
                Direction = ParameterDirection.Output
            };
            comando.Parameters.Add(mensajeParameter);

            // Ejecutar el procedimiento almacenado
            comando.ExecuteNonQuery();

            // Cerrar la conexión
            objConexion.MtdCerrarConexion();

            // Procesar los resultados
            ClUsuarioE obDatosUsuario = null;
            string mensaje = mensajeParameter.Value.ToString();

            if (string.IsNullOrEmpty(mensaje))
            {
                // Crear el objeto usuario si no hay mensaje de error
                obDatosUsuario = new ClUsuarioE
                {
                    Documento = objDatos.Documento,
                    Nombres = nombresParameter.Value.ToString(),
                    Apellidos = apellidosParameter.Value.ToString(),
                    Email = objDatos.Email, // Suponiendo que el email se asigna igual al documento
                    Password = objDatos.Password,
                    Rol = rolParameter.Value.ToString()
                };
            }
            else if (mensaje == "Su cuenta aún está en proceso de activación.")
            {
                // Lanzar una excepción si la cuenta no está activada
                throw new Exception(mensaje);
            }

            return obDatosUsuario;
        }
    }
}