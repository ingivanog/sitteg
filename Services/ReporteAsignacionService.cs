using GuanajuatoAdminUsuarios.Interfaces;
using GuanajuatoAdminUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GuanajuatoAdminUsuarios.Services
{
    public class ReporteAsignacionService : IReporteAsignacionService
    {

        private readonly ISqlClientConnectionBD _sqlClientConnectionBD;
        public ReporteAsignacionService(ISqlClientConnectionBD sqlClientConnectionBD)
        {
            _sqlClientConnectionBD = sqlClientConnectionBD;
        }

        public List<ReporteAsignacionModel> GetAllReporteAsignaciones()
        {
            List<ReporteAsignacionModel> ReporteAsignacionesList = new List<ReporteAsignacionModel>();
            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
                try
                {
                    connection.Open();
                    const string SqlTransact =
                        @"select * from solicitudes sol
                            where estatus= 1";

                    SqlCommand command = new SqlCommand(SqlTransact, connection);
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            ReporteAsignacionModel ReporteAsignacion = new ReporteAsignacionModel();
                            ReporteAsignacion.idSolicitud = Convert.ToInt32(reader["idSolicitud"].ToString());
                            ReporteAsignacion.vehiculoCarretera = reader["vehiculoCarretera"].ToString();
                            ReporteAsignacion.vehiculoTramo = reader["vehiculoTramo"].ToString();
                            ReporteAsignacion.vehiculoKm = reader["vehiculoKm"].ToString();
                            ReporteAsignacion.fechaSolicitud = Convert.ToDateTime(reader["fechaSolicitud"].ToString());
                            ReporteAsignacion.evento = reader["evento"].ToString();
                            ReporteAsignacion.solicitanteNombre = reader["solicitanteNombre"].ToString();
                            ReporteAsignacion.solicitanteAp = reader["solicitanteAp"].ToString();
                            ReporteAsignacion.solicitanteAm = reader["solicitanteAm"].ToString();
                            ReporteAsignacion.solicitanteColonia = reader["solicitanteColonia"].ToString();
                            ReporteAsignacion.solicitanteCalle = reader["solicitanteCalle"].ToString();
                            ReporteAsignacion.solicitanteNumero = reader["solicitanteNumero"].ToString();
                            ReporteAsignacion.tipoVehiculo = reader["tipoVehiculo"].ToString();
                            ReporteAsignacion.propietarioGrua = reader["propietarioGrua"].ToString();
                            ReporteAsignacion.oficial = reader["oficial"].ToString();
                            ReporteAsignacion.folio = reader["folio"].ToString();
                            ReporteAsignacion.vehiculoPension = reader["vehiculoPension"].ToString();
                            ReporteAsignacion.IdDependencia = Convert.ToInt32(reader["IdDependencia"].ToString());
                            ReporteAsignacionesList.Add(ReporteAsignacion);

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
            return ReporteAsignacionesList;
        }

        public List<ReporteAsignacionModel> GetAllReporteAsignaciones(ReporteAsignacionBusquedaModel model)
        {
            List<ReporteAsignacionModel> ReporteAsignacionesList = new List<ReporteAsignacionModel>();
            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
                try
                {
                    connection.Open();
                    const string SqlTransact =
                        @"select * from solicitudes sol
                            where estatus= 1";

                    SqlCommand command = new SqlCommand(SqlTransact, connection);
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            ReporteAsignacionModel ReporteAsignacion = new ReporteAsignacionModel();
                            ReporteAsignacion.idSolicitud = Convert.ToInt32(reader["idSolicitud"].ToString());
                            ReporteAsignacion.vehiculoCarretera = reader["solicitanteNombre"].ToString();
                            ReporteAsignacion.vehiculoTramo = reader["solicitanteNombre"].ToString();
                            ReporteAsignacion.vehiculoKm = reader["solicitanteNombre"].ToString();
                            ReporteAsignacion.fechaSolicitud = Convert.ToDateTime(reader["fechaSolicitud"].ToString());
                            ReporteAsignacion.evento = reader["evento"].ToString();
                            ReporteAsignacion.solicitanteNombre = reader["solicitanteNombre"].ToString();
                            ReporteAsignacion.solicitanteAp = reader["solicitanteAp"].ToString();
                            ReporteAsignacion.solicitanteAm = reader["solicitanteAm"].ToString();
                            ReporteAsignacion.solicitanteColonia = reader["solicitanteColonia"].ToString();
                            ReporteAsignacion.solicitanteCalle = reader["solicitanteCalle"].ToString();
                            ReporteAsignacion.solicitanteNumero = reader["solicitanteNumero"].ToString();
                            ReporteAsignacion.tipoVehiculo = reader["tipoVehiculo"].ToString();
                            ReporteAsignacion.propietarioGrua = reader["propietarioGrua"].ToString();
                            ReporteAsignacion.oficial = reader["oficial"].ToString();
                            ReporteAsignacion.folio = reader["folio"].ToString();
                            ReporteAsignacion.vehiculoPension = reader["vehiculoPension"].ToString();
                            ReporteAsignacion.IdDependencia = Convert.ToInt32(reader["IdDependencia"].ToString());
                            ReporteAsignacionesList.Add(ReporteAsignacion);
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
            return ReporteAsignacionesList;

        }


    }
}
