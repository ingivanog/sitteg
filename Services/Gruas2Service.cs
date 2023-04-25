using GuanajuatoAdminUsuarios.Interfaces;
using GuanajuatoAdminUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;

namespace GuanajuatoAdminUsuarios.Services
{
    public class Gruas2Service : IGruas2Service
    {
        private readonly ISqlClientConnectionBD _sqlClientConnectionBD;
        public Gruas2Service(ISqlClientConnectionBD sqlClientConnectionBD)
        {
            _sqlClientConnectionBD = sqlClientConnectionBD;
        }
        public int CrearGrua(Gruas2Model model)
        {
            int result = 0;
            string strQuery = @"INSERT INTO Gruas2 (@idGrua
                                                   ,@idConcesionario
                                                   ,@idClasificacion
                                                   ,@idTipoGrua
                                                   ,@idSituacion
                                                   ,@noEconomico
                                                   ,@placas
                                                   ,@modelo
                                                   ,@capacidad
                                                   ,@fechaActualizacion
                                                   ,@actualizadoPor
                                                   ,@estatus)";
            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(strQuery, connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@idGrua", SqlDbType.Int)).Value = (object)model.idGrua ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@idConcesionario", SqlDbType.Int)).Value = (object)model.idConcesionario ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@idClasificacion", SqlDbType.Int)).Value = (object)model.idClasificacion ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@idTipoGrua", SqlDbType.Int)).Value = (object)model.idTipoGrua ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@idSituacion", SqlDbType.Int)).Value = (object)model.idSituacion ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@noEconomico", SqlDbType.NVarChar)).Value = (object)model.noEconomico ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@placas", SqlDbType.NVarChar)).Value = (object)model.placas ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@modelo", SqlDbType.NVarChar)).Value = (object)model.modelo ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@capacidad", SqlDbType.NVarChar)).Value = (object)model.capacidad ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@fechaActualizacion", SqlDbType.DateTime)).Value = DateTime.Now.ToString("yyyy-MM-dd");
                    command.Parameters.Add(new SqlParameter("@actualizadoPor", SqlDbType.Int)).Value = 1;
                    command.Parameters.Add(new SqlParameter("@estatus", SqlDbType.Int)).Value = 1;
                    result = command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    return result;
                }
                finally
                {
                    connection.Close();
                }
            }
            return result;
        }

        public int EditarGrua(Gruas2Model model)
        {
            int result = 0;
            string strQuery = @"UPDATE Gruas2 SET idConcesionario       = @idConcesionario    
                                                 ,idClasificacion	   = @idClasificacion	
                                                 ,idTipoGrua			   = @idTipoGrua			
                                                 ,idSituacion		   = @idSituacion		
                                                 ,noEconomico		   = @noEconomico		
                                                 ,placas				   = @placas				
                                                 ,modelo				   = @modelo				
                                                 ,capacidad			   = @capacidad			
                                                 ,fechaActualizacion	   = @fechaActualizacion	
                                                 ,actualizadoPor		   = @actualizadoPor		
                                                 ,estatus			   = @estatus		
                                WHERE idGrua = @idGrua;";

            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(strQuery, connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@idGrua", SqlDbType.Int)).Value = (object)model.idGrua ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@idConcesionario", SqlDbType.Int)).Value = (object)model.idConcesionario ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@idClasificacion", SqlDbType.Int)).Value = (object)model.idClasificacion ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@idTipoGrua", SqlDbType.Int)).Value = (object)model.idTipoGrua ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@idSituacion", SqlDbType.Int)).Value = (object)model.idSituacion ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@noEconomico", SqlDbType.NVarChar)).Value = (object)model.noEconomico ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@placas", SqlDbType.NVarChar)).Value = (object)model.placas ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@modelo", SqlDbType.NVarChar)).Value = (object)model.modelo ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@capacidad", SqlDbType.NVarChar)).Value = (object)model.capacidad ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@fechaActualizacion", SqlDbType.DateTime)).Value = DateTime.Now.ToString("yyyy-MM-dd");
                    command.Parameters.Add(new SqlParameter("@actualizadoPor", SqlDbType.Int)).Value = 1;
                    command.Parameters.Add(new SqlParameter("@estatus", SqlDbType.Int)).Value = 1;
                    result = command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    return result;
                }
                finally
                {
                    connection.Close();
                }
            }
            return result;
        }

        public int EliminarGrua(Gruas2Model model)
        {
            int result = 0;
            string strQuery = @"UPDATE Gruas2 SET fechaActualizacion	   = @fechaActualizacion	
                                                 ,actualizadoPor		   = @actualizadoPor		
                                                 ,estatus			   = @estatus		
                                WHERE idGrua = @idGrua";

            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(strQuery, connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@idGrua", SqlDbType.Int)).Value = (object)model.idGrua ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@fechaActualizacion", SqlDbType.DateTime)).Value = DateTime.Now.ToString("yyyy-MM-dd");
                    command.Parameters.Add(new SqlParameter("@actualizadoPor", SqlDbType.Int)).Value = 1;
                    command.Parameters.Add(new SqlParameter("@estatus", SqlDbType.Int)).Value = 0;
                    result = command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    return result;
                }
                finally
                {
                    connection.Close();
                }
            }
            return result;
        }

        public IEnumerable<Gruas2Model> GetAllGruas()
        {
            List<Gruas2Model> ListGruas = new List<Gruas2Model>();
            string strQuery = @"SELECT
                                idGrua
                                ,idConcesionario
                                ,idClasificacion
                                ,idTipoGrua
                                ,idSituacion
                                ,noEconomico
                                ,placas
                                ,modelo
                                ,capacidad
                                ,fechaActualizacion
                                ,actualizadoPor
                                ,estatus
                                FROM Gruas2
                                WHERE estatus = 1";
            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(strQuery, connection);
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            Gruas2Model gruasModel = new Gruas2Model();
                            gruasModel.idGrua = Convert.ToInt32(reader["idGrua"].ToString());
                            gruasModel.idConcesionario = Convert.ToInt32(reader["idConcesionario"].ToString());
                            gruasModel.idClasificacion = Convert.ToInt32(reader["idClasificacion"].ToString());
                            gruasModel.idTipoGrua = Convert.ToInt32(reader["idTipoGrua"].ToString());
                            gruasModel.idSituacion = Convert.ToInt32(reader["idSituacion"].ToString());
                            gruasModel.noEconomico = reader["noEconomico"].ToString();
                            gruasModel.placas = reader["placas"].ToString();
                            gruasModel.modelo = reader["modelo"].ToString();
                            gruasModel.capacidad = reader["capacidad"].ToString();
                            ListGruas.Add(gruasModel);
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

        public Gruas2Model GetGruaById(int idGrua)
        {
            List<Gruas2Model> ListGruas = new List<Gruas2Model>();
            string strQuery = @"SELECT
                                idGrua
                                ,idConcesionario
                                ,idClasificacion
                                ,idTipoGrua
                                ,idSituacion
                                ,noEconomico
                                ,placas
                                ,modelo
                                ,capacidad
                                ,fechaActualizacion
                                ,actualizadoPor
                                ,estatus
                                FROM Gruas2
                                WHERE idGrua = @idGrua AND estatus = 1";
            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(strQuery, connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@idGrua", SqlDbType.Int)).Value = (object)idGrua ?? DBNull.Value;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            Gruas2Model gruasModel = new Gruas2Model();
                            gruasModel.idGrua = Convert.ToInt32(reader["idGrua"].ToString());
                            gruasModel.idConcesionario = Convert.ToInt32(reader["idConcesionario"].ToString());
                            gruasModel.idClasificacion = Convert.ToInt32(reader["idClasificacion"].ToString());
                            gruasModel.idTipoGrua = Convert.ToInt32(reader["idTipoGrua"].ToString());
                            gruasModel.idSituacion = Convert.ToInt32(reader["idSituacion"].ToString());
                            gruasModel.noEconomico = reader["noEconomico"].ToString();
                            gruasModel.placas = reader["placas"].ToString();
                            gruasModel.modelo = reader["modelo"].ToString();
                            gruasModel.capacidad = reader["capacidad"].ToString();
                            ListGruas.Add(gruasModel);
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
            return ListGruas.FirstOrDefault();
        }

        public IEnumerable<Gruas2Model> GetGruasByIdConcesionario(int idConcesionario)
        {
            List<Gruas2Model> ListGruas = new List<Gruas2Model>();
            string strQuery = @"SELECT
                                idGrua
                                ,idConcesionario
                                ,idClasificacion
                                ,idTipoGrua
                                ,idSituacion
                                ,noEconomico
                                ,placas
                                ,modelo
                                ,capacidad
                                ,fechaActualizacion
                                ,actualizadoPor
                                ,estatus
                                FROM Gruas2
                                WHERE idConcesionario = @idConcesionario AND estatus = 1";
            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(strQuery, connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@idGrua", SqlDbType.Int)).Value = (object)idConcesionario ?? DBNull.Value;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            Gruas2Model gruasModel = new Gruas2Model();
                            gruasModel.idGrua = Convert.ToInt32(reader["idGrua"].ToString());
                            gruasModel.idConcesionario = Convert.ToInt32(reader["idConcesionario"].ToString());
                            gruasModel.idClasificacion = Convert.ToInt32(reader["idClasificacion"].ToString());
                            gruasModel.idTipoGrua = Convert.ToInt32(reader["idTipoGrua"].ToString());
                            gruasModel.idSituacion = Convert.ToInt32(reader["idSituacion"].ToString());
                            gruasModel.noEconomico = reader["noEconomico"].ToString();
                            gruasModel.placas = reader["placas"].ToString();
                            gruasModel.modelo = reader["modelo"].ToString();
                            gruasModel.capacidad = reader["capacidad"].ToString();
                            ListGruas.Add(gruasModel);
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

        public IEnumerable<Gruas2Model> GetGruasToGrid(string placas, string noEconomico, int? idTipoGrua)
        {
            List<Gruas2Model> ListGruas = new List<Gruas2Model>();
            string strQuery = @"SELECT
                                idGrua
                                ,idConcesionario
                                ,idClasificacion
                                ,idTipoGrua
                                ,idSituacion
                                ,noEconomico
                                ,placas
                                ,modelo
                                ,capacidad
                                ,fechaActualizacion
                                ,actualizadoPor
                                ,estatus
                                FROM Gruas2
                                WHERE estatus = 1
                                AND placas = {0}
                                AND noEconomico = {1}
                                AND idTipoGrua = {2}";
            string strWherePlacas = !string.IsNullOrEmpty(placas) ? placas : "placas";
            string strWhereNoEconomico = !string.IsNullOrEmpty(noEconomico) ? placas : "noEconomico";
            string strWhereidTipoGrua = idTipoGrua != null ? idTipoGrua.ToString() : "idTipoGrua";
            strQuery = string.Format(strQuery, strWherePlacas, strWhereNoEconomico, strWhereidTipoGrua);

            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(strQuery, connection);
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            Gruas2Model gruasModel = new Gruas2Model();
                            gruasModel.idGrua = Convert.ToInt32(reader["idGrua"].ToString());
                            gruasModel.idConcesionario = Convert.ToInt32(reader["idConcesionario"].ToString());
                            gruasModel.idClasificacion = Convert.ToInt32(reader["idClasificacion"].ToString());
                            gruasModel.idTipoGrua = Convert.ToInt32(reader["idTipoGrua"].ToString());
                            gruasModel.idSituacion = Convert.ToInt32(reader["idSituacion"].ToString());
                            gruasModel.noEconomico = reader["noEconomico"].ToString();
                            gruasModel.placas = reader["placas"].ToString();
                            gruasModel.modelo = reader["modelo"].ToString();
                            gruasModel.capacidad = reader["capacidad"].ToString();
                            ListGruas.Add(gruasModel);
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

        public List<TipoGruaModel> GetCatTiposGrua()
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
    }
}
