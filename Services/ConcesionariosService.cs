using GuanajuatoAdminUsuarios.Interfaces;
using GuanajuatoAdminUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GuanajuatoAdminUsuarios.Services
{
    public class ConcesionariosService : IConcesionariosService
    {
        private readonly ISqlClientConnectionBD _sqlClientConnectionBD;
        public ConcesionariosService(ISqlClientConnectionBD sqlClientConnectionBD)
        {
            _sqlClientConnectionBD = sqlClientConnectionBD;
        }


        public List<ConcesionariosModel> GetConcesionarios()
        {
            List<ConcesionariosModel> ListConcesionarios = new List<ConcesionariosModel>();

            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("Select * from Concesionarios", connection);
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            ConcesionariosModel concesionarios = new ConcesionariosModel();
                            concesionarios.IdConcesionario = Convert.ToInt32(reader["IdConcesionario"].ToString());
                            concesionarios.Concesionario = reader["Concesionario"].ToString();
                            ListConcesionarios.Add(concesionarios);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    //Guardar la excepcion en algun log de errores
                    //ex
                }
                finally
                {
                    connection.Close();
                }
            return ListConcesionarios;

        }
    }
}
