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


        public List<Concesionarios2Model> GetConcesionarios2ByIdDelegacion(int idDelegacion)
        {
            List<Concesionarios2Model> ListConcesionarios = new List<Concesionarios2Model>();
            string strQuery = @"SELECT 
                                idConcesionario
                                ,concesionario
                                ,idDelegacion
                                ,idMunicipio
                                ,alias
                                ,razonSocial
                                ,fechaActualizacion
                                ,actualizadoPor
                                ,estatus
                                FROM concesionarios
                                WHERE idDelegacion = @idDelegacion AND estatus = 1";
            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(strQuery, connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@idDelegacion", SqlDbType.Int)).Value = idDelegacion;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            Concesionarios2Model concesionarios = new Concesionarios2Model();
                            concesionarios.idConcesionario = Convert.ToInt32(reader["idConcesionario"].ToString());
                            concesionarios.nombre = reader["concesionario"].ToString();
                            concesionarios.idDelegacion = Convert.ToInt32(reader["idDelegacion"].ToString());
                            concesionarios.idMunicipio = Convert.ToInt32(reader["idMunicipio"].ToString());
                            concesionarios.alias = reader["alias"].ToString();
                            concesionarios.razonSocial = reader["razonSocial"].ToString();
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
