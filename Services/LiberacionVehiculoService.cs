using GuanajuatoAdminUsuarios.Entity;
using GuanajuatoAdminUsuarios.Interfaces;
using GuanajuatoAdminUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GuanajuatoAdminUsuarios.Services
{
    public class LiberacionVehiculoService : ILiberacionVehiculoService
    {
        private readonly ISqlClientConnectionBD _sqlClientConnectionBD;
        public LiberacionVehiculoService(ISqlClientConnectionBD sqlClientConnectionBD)
        {
            _sqlClientConnectionBD = sqlClientConnectionBD;
        }

        public List<LiberacionVehiculoModel> GetAllTopDepositos()
        {
            List<LiberacionVehiculoModel> depositosList = new List<LiberacionVehiculoModel>();
            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
                try
                {
                    connection.Open();
                    const string SqlTransact =
                        @"select top(100) d.IdDeposito,d.IdSolicitud,d.IdDelegacion,d.IdMarca,d.IdSubmarca,d.IdPension,d.IdTramo,
		                d.IdColor,d.Serie,d.Placa,d.FechaIngreso,d.Folio,d.Km,d.Liberado,d.Autoriza,d.FechaActualizacion,
		                del.delegacion, d.ActualizadoPor, d.estatus, m.marcaVehiculo,subm.nombreSubmarca
		                from depositos d inner join delegaciones del on d.idDelegacion= del.idDelegacion
		                inner join marcasVehiculos m on d.idMarca=m.idMarcaVehiculo
		                inner join submarcasVehiculos  subm on m.idMarcaVehiculo=subm.idMarcaVehiculo
		                where d.liberado=0 and d.estatus=1";
                    SqlCommand command = new SqlCommand(SqlTransact, connection);
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            LiberacionVehiculoModel deposito = new LiberacionVehiculoModel();
                            deposito.IdDeposito = Convert.ToInt32(reader["IdDeposito"].ToString());
                            deposito.IdSolicitud = Convert.ToInt32(reader["IdSolicitud"].ToString());
                            deposito.IdDelegacion = Convert.ToInt32(reader["IdDelegacion"].ToString());
                            deposito.IdMarca = Convert.ToInt32(reader["IdMarca"].ToString());
                            deposito.IdSubmarca = Convert.ToInt32(reader["IdSubmarca"].ToString());
                            deposito.IdPension = Convert.ToInt32(reader["IdPension"].ToString());
                            deposito.IdTramo = Convert.ToInt32(reader["IdTramo"].ToString());
                            deposito.IdColor = Convert.ToInt32(reader["IdColor"].ToString());
                            deposito.Serie = reader["Serie"].ToString();
                            deposito.Placa = reader["Placa"].ToString();
                            deposito.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"].ToString());
                            deposito.Folio = reader["Folio"].ToString();
                            deposito.Km = reader["Km"].ToString();
                            deposito.Liberado = Convert.ToInt32(reader["Liberado"].ToString());
                            //deposito.AcreditacionPropiedad = reader["AcreditacionPropiedad"].ToString();
                            //deposito.AcreditacionPersonalidad = reader["AcreditacionPersonalidad"].ToString();
                            //deposito.ReciboPago = reader["ReciboPago"].ToString();
                            //deposito.Observaciones = reader["Observaciones"].ToString();
                            deposito.Autoriza = reader["Autoriza"].ToString();
                            deposito.FechaActualizacion = Convert.ToDateTime(reader["FechaActualizacion"].ToString());
                            deposito.ActualizadoPor = Convert.ToInt32(reader["ActualizadoPor"].ToString());
                            deposito.Estatus = Convert.ToInt32(reader["Estatus"].ToString());
                            deposito.marcaVehiculo = reader["marcaVehiculo"].ToString();
                            deposito.nombreSubmarca = reader["nombreSubmarca"].ToString();
                            deposito.delegacion = reader["delegacion"].ToString();
                            depositosList.Add(deposito);
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
            return depositosList;
        }

        public List<LiberacionVehiculoModel> GetDepositos(LiberacionVehiculoBusquedaModel model)
        {
            List<LiberacionVehiculoModel> depositosList = new List<LiberacionVehiculoModel>();

            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
                try
                {
                    connection.Open();
                    const string SqlTransact =
                                @"select top(100) d.IdDeposito,d.IdSolicitud,d.IdDelegacion,d.IdMarca,d.IdSubmarca,d.IdPension,d.IdTramo,
		                        d.IdColor,d.Serie,d.Placa,d.FechaIngreso,d.Folio,d.Km,d.Liberado,d.Autoriza,d.FechaActualizacion,
		                        del.delegacion, d.ActualizadoPor, d.estatus, m.marcaVehiculo,subm.nombreSubmarca
		                        from depositos d inner join delegaciones del on d.idDelegacion= del.idDelegacion
		                        inner join marcasVehiculos m on d.idMarca=m.idMarcaVehiculo
		                        inner join submarcasVehiculos  subm on m.idMarcaVehiculo=subm.idMarcaVehiculo
		                        where d.liberado=0 and d.estatus=1	and
		                        (d.IdDeposito=@IdDeposito  OR d.IdMarca=@IdMarca 
		                        OR d.Serie LIKE '%' + @Serie + '%' OR d.FechaIngreso =@FechaIngreso 
		                        OR d.Folio LIKE '%' + @Folio + '%')";

                    SqlCommand command = new SqlCommand(SqlTransact, connection);
                    command.Parameters.Add(new SqlParameter("@IdDeposito", SqlDbType.Int)).Value = (object)model.IdDeposito ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@IdMarca", SqlDbType.Int)).Value = (object)model.IdMarcaVehiculo ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@Serie", SqlDbType.NVarChar)).Value = (object)model.Serie ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@FechaIngreso", SqlDbType.DateTime)).Value = (object)model.FechaIngreso ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@Folio", SqlDbType.NVarChar)).Value = (object)model.Folio ?? DBNull.Value;

                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            LiberacionVehiculoModel deposito = new LiberacionVehiculoModel();
                            deposito.IdDeposito = Convert.ToInt32(reader["IdDeposito"].ToString());
                            deposito.IdSolicitud = Convert.ToInt32(reader["IdSolicitud"].ToString());
                            deposito.IdDelegacion = Convert.ToInt32(reader["IdDelegacion"].ToString());
                            deposito.IdMarca = Convert.ToInt32(reader["IdMarca"].ToString());
                            deposito.IdSubmarca = Convert.ToInt32(reader["IdSubmarca"].ToString());
                            deposito.IdPension = Convert.ToInt32(reader["IdPension"].ToString());
                            deposito.IdTramo = Convert.ToInt32(reader["IdTramo"].ToString());
                            deposito.IdColor = Convert.ToInt32(reader["IdColor"].ToString());
                            deposito.Serie = reader["Serie"].ToString();
                            deposito.Placa = reader["Placa"].ToString();
                            deposito.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"].ToString());
                            deposito.Folio = reader["Folio"].ToString();
                            deposito.Km = reader["Km"].ToString();
                            deposito.Liberado = Convert.ToInt32(reader["Liberado"].ToString());
                            //deposito.AcreditacionPropiedad = reader["AcreditacionPropiedad"].ToString();
                            //deposito.AcreditacionPersonalidad = reader["AcreditacionPersonalidad"].ToString();
                            //deposito.ReciboPago = reader["ReciboPago"].ToString();
                            //deposito.Observaciones = reader["Observaciones"].ToString();
                            deposito.Autoriza = reader["Autoriza"].ToString();
                            deposito.FechaActualizacion = Convert.ToDateTime(reader["FechaActualizacion"].ToString());
                            deposito.ActualizadoPor = Convert.ToInt32(reader["ActualizadoPor"].ToString());
                            deposito.Estatus = Convert.ToInt32(reader["Estatus"].ToString());

                            deposito.marcaVehiculo = reader["marcaVehiculo"].ToString();
                            deposito.nombreSubmarca = reader["nombreSubmarca"].ToString();
                            deposito.delegacion = reader["delegacion"].ToString();

                            depositosList.Add(deposito);
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
            return depositosList;
        }

        public LiberacionVehiculoModel GetDepositoByID(int Id)
        {
            LiberacionVehiculoModel deposito = new LiberacionVehiculoModel();
            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
                try
                {
                    connection.Open();
                    const string SqlTransact =
                        @"select d.IdDeposito,d.IdSolicitud,d.IdDelegacion,d.IdMarca,d.IdSubmarca,d.IdPension,d.IdTramo,
		                d.IdColor,d.Serie,d.Placa,d.FechaIngreso,d.Folio,d.Km,d.Liberado,d.Autoriza,d.FechaActualizacion,
		                del.delegacion, d.ActualizadoPor, d.estatus, m.marcaVehiculo,subm.nombreSubmarca
		                from depositos d inner join delegaciones del on d.idDelegacion= del.idDelegacion
		                inner join marcasVehiculos m on d.idMarca=m.idMarcaVehiculo
		                inner join submarcasVehiculos  subm on m.idMarcaVehiculo=subm.idMarcaVehiculo
		                where d.liberado=0 and d.estatus=1 and d.IdDeposito=@IdDeposito";
                    SqlCommand command = new SqlCommand(SqlTransact, connection);
                    command.Parameters.Add(new SqlParameter("@IdDeposito", SqlDbType.Int)).Value = (object)Id ?? DBNull.Value;
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {

                            deposito.IdDeposito = Convert.ToInt32(reader["IdDeposito"].ToString());
                            deposito.IdSolicitud = Convert.ToInt32(reader["IdSolicitud"].ToString());
                            deposito.IdDelegacion = Convert.ToInt32(reader["IdDelegacion"].ToString());
                            deposito.IdMarca = Convert.ToInt32(reader["IdMarca"].ToString());
                            deposito.IdSubmarca = Convert.ToInt32(reader["IdSubmarca"].ToString());
                            deposito.IdPension = Convert.ToInt32(reader["IdPension"].ToString());
                            deposito.IdTramo = Convert.ToInt32(reader["IdTramo"].ToString());
                            deposito.IdColor = Convert.ToInt32(reader["IdColor"].ToString());
                            deposito.Serie = reader["Serie"].ToString();
                            deposito.Placa = reader["Placa"].ToString();
                            deposito.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"].ToString());
                            deposito.Folio = reader["Folio"].ToString();
                            deposito.Km = reader["Km"].ToString();
                            deposito.Liberado = Convert.ToInt32(reader["Liberado"].ToString());
                            //deposito.AcreditacionPropiedad = reader["AcreditacionPropiedad"].ToString();
                            //deposito.AcreditacionPersonalidad = reader["AcreditacionPersonalidad"].ToString();
                            //deposito.ReciboPago = reader["ReciboPago"].ToString();
                            //deposito.Observaciones = reader["Observaciones"].ToString();
                            deposito.Autoriza = reader["Autoriza"].ToString();
                            deposito.FechaActualizacion = Convert.ToDateTime(reader["FechaActualizacion"].ToString());
                            deposito.ActualizadoPor = Convert.ToInt32(reader["ActualizadoPor"].ToString());
                            deposito.Estatus = Convert.ToInt32(reader["Estatus"].ToString());
                            deposito.marcaVehiculo = reader["marcaVehiculo"].ToString();
                            deposito.nombreSubmarca = reader["nombreSubmarca"].ToString();
                            deposito.delegacion = reader["delegacion"].ToString();
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
            return deposito;
        }


    }
}
