
using System.Data.SqlClient;

namespace WebAppUsuarios.Models
{
    public class AccesoDatos
    {
        //almacenar la cadena de conexion a la base de datos
        private readonly string _conexion;

        public AccesoDatos(IConfiguration configuracion)
        {
            _conexion = configuracion.GetConnectionString("Conexion");
        }

        //Metodo que busca crear el usuario
        public void AgregarUsuario(usuarios nuevoUsuario)
        {
            using (SqlConnection con = new SqlConnection(_conexion))
            {
                try
                {
                    string query = "Exec spCrearUsuario @pNombre, @pEmail";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@pNombre", nuevoUsuario.nombre);
                        cmd.Parameters.AddWithValue("@pEmail", nuevoUsuario.email);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al registrar el usuario" + ex.Message);
                }
            }
        }
    }
}
