using EjemploClase9WCF.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Services.Description;

namespace EjemploClase9WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        // Insert normal por codificacion
        public string IngresarPersona(Persona userInfo)
        {
            string Message;
            string cadena = ConfigurationManager.ConnectionStrings["MiConexionSQL"].ConnectionString;
            SqlConnection con = new SqlConnection(cadena);
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Persona]([nombre],[apellido1],[apellido2],[fechaNacimiento],[email],[edad]) VALUES(@nombre,@apellido1,@apellido2,@fechaNacimiento,@email,@edad)", con);
            cmd.Parameters.AddWithValue("@nombre", userInfo.Nombre);
            cmd.Parameters.AddWithValue("@apellido1", userInfo.Apellido1);
            cmd.Parameters.AddWithValue("@apellido2", userInfo.Apellido2);
            cmd.Parameters.AddWithValue("@fechaNacimiento", userInfo.FechaNacimiento);
            cmd.Parameters.AddWithValue("@email", userInfo.CorreoElectronico);
            cmd.Parameters.AddWithValue("@edad", userInfo.Edad);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                Message = "La persona: " + userInfo.Nombre + " " + userInfo.Apellido1 + " " + userInfo.Apellido2 + " fue agregada exitosamente";
            }
            else
            {
                Message = "Se presentó un inconveniente al ingresar la persona: " + userInfo.Nombre + " " + userInfo.Apellido1 + " " + userInfo.Apellido2;
            }
            con.Close();
            return Message;
        }

        public string IngresarPersonaSP(Persona userInfo)
        {
            string Message;
            string cadena = ConfigurationManager.ConnectionStrings["MiConexionSQL"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cadena))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("sp_InsertarPersona", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", userInfo.Nombre);
                    cmd.Parameters.AddWithValue("@apellido1", userInfo.Apellido1);
                    cmd.Parameters.AddWithValue("@apellido2", userInfo.Apellido2);
                    cmd.Parameters.AddWithValue("@fechaNacimiento", userInfo.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@email", userInfo.CorreoElectronico);
                    cmd.Parameters.AddWithValue("@edad", userInfo.Edad);
                    int result = cmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        Message = $"La persona: {userInfo.Nombre} {userInfo.Apellido1} {userInfo.Apellido2} fue agregada exitosamente";
                    }
                    else
                    {
                        Message = $"Se presentó un inconveniente al ingresar la persona: {userInfo.Nombre} {userInfo.Apellido1} {userInfo.Apellido2}";
                    }
                }
            }
            return Message;
        }

        public string ObtenerNombrePersonaPorId(int idPersona)
        {
            string cadena = ConfigurationManager.ConnectionStrings["MiConexionMySQL"].ConnectionString;
            string nombreCompleto = "";
            using (MySqlConnection con = new MySqlConnection(cadena))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT nombre,apellido1,apellido2 FROM persona WHERE personaID = @id", con);
                cmd.Parameters.AddWithValue("@id", idPersona);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string nombre = reader["nombre"].ToString();
                        string apellido1 = reader["apellido1"].ToString();
                        string apellido2 = reader["apellido2"].ToString();
                        nombreCompleto = $"{nombre} {apellido1} {apellido2}";
                    }
                }
            }
            return nombreCompleto ?? "No se encontró la persona con el ID especificado.";
        }
    }
}