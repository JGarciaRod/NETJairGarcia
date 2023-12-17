using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Persona
    {
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApelldioMaterno { get; set; }
        public string Correo { get; set; }
        public string Imagen { get; set; }

        public List<object> Personas { get; set; }

        public static Result GetAll()
        {
            Result result = new Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConecctionString()))
                {
                    string query = "PersonaGetAll";

                    SqlCommand cmd = new SqlCommand(query,context);

                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataTable TablePeronas =  new DataTable();

                    adapter.Fill(TablePeronas);

                    if(TablePeronas.Columns.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (DataRow row in TablePeronas.Rows)
                        {
                            Persona persona = new Persona();

                            persona.IdPersona = int.Parse(row[0].ToString());
                            persona.Nombre = row[1].ToString();
                            persona.ApellidoPaterno = row[2].ToString();
                            persona.ApelldioMaterno = row[3].ToString();
                            persona.Correo = row[4].ToString();
                            persona.Imagen = row[5].ToString();

                            result.Objects.Add(persona);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch(Exception ex) 
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
