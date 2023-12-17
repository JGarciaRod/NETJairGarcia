using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EvalXPersona
    {
        public int IdEvalxPersona { get; set; }
        public Examen Examen { get; set; }
        public Persona Persona { get; set; }
        public DateTime FechaAsignacion { get; set; }

        public List<object> EvalxPersonas { get; set; }

        public static Result GetExamenAsignado(int IdPersona)
        {
            Result result = new Result();
            try
            {
                using (SqlConnection connection = new SqlConnection(DL.Conexion.GetConecctionString()))
                {
                    string query = "GetExamenAsignado";

                    SqlCommand cmd = new SqlCommand(query, connection);

                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] param = new SqlParameter[1];

                    param[0] = new SqlParameter("@IdPersona", SqlDbType.Int);
                    param[0].Value = IdPersona;

                    cmd.Parameters.AddRange(param);
                    cmd.Connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataTable TableExAsiganados = new DataTable();

                    adapter.Fill(TableExAsiganados);

                    if (TableExAsiganados.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (DataRow row in TableExAsiganados.Rows)
                        {
                            EvalXPersona evalXPersona = new EvalXPersona();
                            evalXPersona.Persona = new Persona();
                            evalXPersona.Examen = new Examen();

                            evalXPersona.IdEvalxPersona = int.Parse(row[0].ToString());
                            evalXPersona.Examen.IdExamen = int.Parse(row[1].ToString());
                            evalXPersona.Examen.Descripcion = row[2].ToString();
                            evalXPersona.Persona.IdPersona = int.Parse(row[3].ToString());
                            evalXPersona.Persona.Nombre = row[4].ToString();
                            evalXPersona.Persona.ApellidoPaterno = row[5].ToString();
                            evalXPersona.Persona.ApelldioMaterno = row[6].ToString();
                            evalXPersona.FechaAsignacion = DateTime.Parse(row[7].ToString());

                            result.Objects.Add(evalXPersona);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static Result GetExamenNoAsignado(int IdPersona)
        {
            Result result = new Result();
            try
            {
                using (SqlConnection connection = new SqlConnection(DL.Conexion.GetConecctionString()))
                {
                    var query = "GetExamenesNoAsignados";

                    SqlCommand cmd = new SqlCommand(query, connection);

                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] param = new SqlParameter[1];

                    param[0] = new SqlParameter("@IdPersona", SqlDbType.Int);
                    param[0].Value = IdPersona;

                    cmd.Parameters.AddRange(param);
                    cmd.Connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataTable TableExNoAsiganados = new DataTable();

                    adapter.Fill(TableExNoAsiganados);

                    if(TableExNoAsiganados.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();
                        
                        foreach (DataRow row in TableExNoAsiganados.Rows)
                        {
                            BL.Examen examen = new BL.Examen();

                            examen.IdExamen = int.Parse(row[0].ToString());
                            examen.Descripcion = row[1].ToString();

                            result.Objects.Add(examen);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct= false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static Result Add(int IdPersona, int IdExamen)
        {
            Result result = new Result();
            try
            {
                using (SqlConnection connection = new SqlConnection(DL.Conexion.GetConecctionString()))
                {
                    string query = "AddExamenPersona";

                    SqlCommand cmd = new SqlCommand(query, connection);

                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] param = new SqlParameter[2];

                    param[0] = new SqlParameter("@IdPersona", SqlDbType.Int);
                    param[0].Value = IdPersona;

                    param[1] = new SqlParameter("@IdExamen",SqlDbType.Int);
                    param[1].Value = IdExamen;

                    cmd.Parameters.AddRange(param);
                    cmd.Connection.Open();

                    int rowAffected = cmd.ExecuteNonQuery();

                    if (rowAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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
        public static Result Delete(int IdEvalxPersona)
        {
            Result result = new Result();
            try
            {
                using (SqlConnection connection =  new SqlConnection(DL.Conexion.GetConecctionString()))
                {
                    string query = "DellExamenAsignado";

                    SqlCommand cmd = new SqlCommand(query, connection);

                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] parms = new SqlParameter[1];

                    parms[0] = new SqlParameter("@IdEvalxPersona", SqlDbType.Int);
                    parms[0].Value = IdEvalxPersona;

                    cmd.Parameters.AddRange(parms);
                    cmd.Connection.Open();

                    int rowAffected = cmd.ExecuteNonQuery();
                    if (rowAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        

        public static Result GetByCorreo(string Correo)
        {
            Result result = new Result();
            try
            {
                using (SqlConnection connection = new SqlConnection(DL.Conexion.GetConecctionString()))
                {
                    string quey = "GetByCorreo";

                    SqlCommand cmd = new SqlCommand(quey,connection);

                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] param = new SqlParameter[1];

                    param[0] = new SqlParameter("@Correo", SqlDbType.VarChar);
                    param[0].Value = Correo;

                    cmd.Parameters.AddRange(param);
                    cmd.Connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataTable TablaCorreo = new DataTable();

                    adapter.Fill(TablaCorreo);

                    if(TablaCorreo.Rows.Count > 0)
                    {
                        result.Object = new List<object>();
                        DataRow row = TablaCorreo.Rows[0];

                        EvalXPersona evalXPersona = new EvalXPersona();
                        evalXPersona.Persona = new Persona();
                        evalXPersona.Examen = new Examen();

                        evalXPersona.Persona.IdPersona = int.Parse(row[0].ToString());
                        evalXPersona.Persona.Nombre = row[1].ToString();
                        evalXPersona.Persona.ApellidoPaterno = row[2].ToString();
                        evalXPersona.Persona.ApelldioMaterno = row[3].ToString();
                        evalXPersona.Persona.Correo = row[4].ToString();
                        evalXPersona.Examen.IdExamen = int.Parse(row[5].ToString());
                        evalXPersona.Examen.Descripcion = row[6].ToString();
                        evalXPersona.FechaAsignacion = DateTime.Parse(row[7].ToString());
                        evalXPersona.Persona.Imagen = row[8].ToString();

                        result.Object = evalXPersona;

                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
