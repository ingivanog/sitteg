using GuanajuatoAdminUsuarios.Interfaces;
using GuanajuatoAdminUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GuanajuatoAdminUsuarios.Services
{
    public class GruasService : IGruasService
    {
        private readonly ISqlClientConnectionBD _sqlClientConnectionBD;
        public GruasService(ISqlClientConnectionBD sqlClientConnectionBD)
        {
            _sqlClientConnectionBD = sqlClientConnectionBD;
        }

        public List<GruasConcesionariosModel> GetGruasConcesionariosByIdCocesionario(int Id)
        {
            List<GruasConcesionariosModel> GruasConcesionariosList = new List<GruasConcesionariosModel>();
            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
                try
                {
                    connection.Open();
                    const string SqlTransact =
                        @"select g.IdGrua,  c.IdConcesionario,g.noEconomico,g.placas,g.modelo,g.capacidad,
                            c.Concesionario,catg.IdTipoGrua, catg.TipoGrua
                            from Concesionarios c
                            inner join Gruas g on g.idConcesionario= c.idConcesionario
                            inner join catTipoGrua catg ON catg.IdTipoGrua=g.IdTipoGrua
                            where c.IdConcesionario=@IdConcesionario";

                    SqlCommand command = new SqlCommand(SqlTransact, connection);
                    command.Parameters.Add(new SqlParameter("@IdConcesionario", SqlDbType.Int)).Value = (object)Id ?? DBNull.Value;
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            GruasConcesionariosModel gruasConcesionario = new GruasConcesionariosModel();
                            gruasConcesionario.IdGrua = Convert.ToInt32(reader["IdGrua"].ToString());
                            gruasConcesionario.IdConcesionario = Convert.ToInt32(reader["IdConcesionario"].ToString());
                            gruasConcesionario.IdTipoGrua = Convert.ToInt32(reader["IdTipoGrua"].ToString());
                            gruasConcesionario.noEconomico = reader["noEconomico"].ToString();
                            gruasConcesionario.placas = reader["placas"].ToString();
                            gruasConcesionario.modelo = reader["modelo"].ToString();
                            gruasConcesionario.capacidad = reader["capacidad"].ToString();
                            //gruasConcesionario.clasificacion = reader["clasificacion"].ToString();
                            gruasConcesionario.Concesionario = reader["Concesionario"].ToString();
                            gruasConcesionario.TipoGrua = reader["TipoGrua"].ToString();
                            GruasConcesionariosList.Add(gruasConcesionario);

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
            return GruasConcesionariosList;

        }

        public List<TipoGruaModel> GetTipoGruas()
        {
            List<TipoGruaModel> ListTipoGruas = new List<TipoGruaModel>();

            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("Select * from catTipoGrua", connection);
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            TipoGruaModel tipoGruaModel = new TipoGruaModel();
                            tipoGruaModel.IdTipoGrua = Convert.ToInt32(reader["IdTipoGrua"].ToString());
                            tipoGruaModel.TipoGrua = reader["TipoGrua"].ToString();
                            ListTipoGruas.Add(tipoGruaModel);

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
            return ListTipoGruas;

        }

        public List<GruasModel> GetGruas()
        {
            List<GruasModel> ListGruas = new List<GruasModel>();

            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("Select * from Gruas where estatus=1", connection);
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            GruasModel gruaModel = new GruasModel();
                            gruaModel.IdGrua = Convert.ToInt32(reader["IdGrua"].ToString());
                            gruaModel.Placas = reader["Placas"].ToString();
                            ListGruas.Add(gruaModel);

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
            return ListGruas;

        }

    }
}
