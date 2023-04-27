﻿using GuanajuatoAdminUsuarios.Interfaces;
using GuanajuatoAdminUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GuanajuatoAdminUsuarios.Services
{
    public class PensionesService : IPensionesService
    {

        private readonly ISqlClientConnectionBD _sqlClientConnectionBD;
        public PensionesService(ISqlClientConnectionBD sqlClientConnectionBD)
        {
            _sqlClientConnectionBD = sqlClientConnectionBD;
        }

        public List<PensionesModel> GetAllPensiones()
        {

            List<PensionesModel> ListPensiones = new List<PensionesModel>();
            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
                try
                {
                    connection.Open();
                    const string SqlTransact = @"select pen.idPension,	pen.indicador, pen.pension,
                                           pen.permiso,pen.idDelegacion, pen.idMunicipio,pen.direccion,
                                           pen.telefono, pen.correo,pen.idConcesionario,pen.fechaActualizacion,
                                           pen.actualizadoPor,pen.estatus, pen.idResponsable, mun.municipio,
                                           re.responsable,del.delegacion
                                          from pensiones pen 
                                          join catMunicipios mun on  pen.idMunicipio= mun.idMunicipio
                                          join catDelegaciones del on  pen.idDelegacion= del.idDelegacion
                                          join catResponsablePensiones re on pen.idResponsable = re.idResponsable
                                          where pen.estatus=1";

                    SqlCommand command = new SqlCommand(SqlTransact, connection);
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            PensionesModel pension = new PensionesModel();
                            pension.IdPension = Convert.ToInt32(reader["idPension"].ToString());
                            pension.Indicador = Convert.ToInt32(reader["indicador"].ToString());
                            pension.Pension = reader["pension"].ToString();
                            pension.Permiso = reader["permiso"].ToString();
                            pension.IdDelegacion = Convert.ToInt32(reader["idDelegacion"].ToString());
                            pension.IdMunicipio = Convert.ToInt32(reader["idMunicipio"].ToString());
                            pension.Direccion = reader["direccion"].ToString();
                            pension.Telefono = reader["telefono"].ToString();
                            pension.Correo = reader["correo"].ToString();
                            pension.FechaActualizacion = Convert.ToDateTime(reader["fechaActualizacion"].ToString());
                            pension.ActualizadoPor = Convert.ToInt32(reader["actualizadoPor"].ToString());
                            pension.estatus = Convert.ToInt32(reader["estatus"].ToString());
                            pension.municipio = reader["municipio"].ToString();
                            pension.responsable = reader["responsable"].ToString();
                            pension.delegacion = reader["delegacion"].ToString();
                            ListPensiones.Add(pension);

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
            return ListPensiones;
        }

        public List<PensionesModel> GetPensiones(PensionesBusquedaModel model)
        {

            List<PensionesModel> ListPensiones = new List<PensionesModel>();
            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
                try
                {
                    connection.Open();
                    const string SqlTransact = @"select pen.idPension,	pen.indicador, pen.pension,
                                           pen.permiso,pen.idDelegacion, pen.idMunicipio,pen.direccion,
                                           pen.telefono, pen.correo,pen.idConcesionario,pen.fechaActualizacion,
                                           pen.actualizadoPor,pen.estatus, pen.idResponsable, mun.municipio,
                                           re.responsable,del.delegacion
                                          from pensiones pen 
                                          join catMunicipios mun on  pen.idMunicipio= mun.idMunicipio
                                          join catDelegaciones del on  pen.idDelegacion= del.idDelegacion
                                          join catResponsablePensiones re on pen.idResponsable = re.idResponsable
                                          where pen.estatus=1 and (idDelegacion = @IdDelegacion OR pension LIKE '%' + @pension + '%')";

                    SqlCommand command = new SqlCommand(SqlTransact, connection);

                    command.Parameters.Add(new SqlParameter("@IdDelegacion", SqlDbType.Int)).Value = (object)model.IdDelegacion ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@pension", SqlDbType.NVarChar)).Value = (object)model.pension ?? DBNull.Value;

                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            PensionesModel pension = new PensionesModel();
                            pension.IdPension = Convert.ToInt32(reader["idPension"].ToString());
                            pension.Indicador = Convert.ToInt32(reader["indicador"].ToString());
                            pension.Pension = reader["pension"].ToString();
                            pension.Permiso = reader["permiso"].ToString();
                            pension.IdDelegacion = Convert.ToInt32(reader["idDelegacion"].ToString());
                            pension.IdMunicipio = Convert.ToInt32(reader["idMunicipio"].ToString());
                            pension.Direccion = reader["direccion"].ToString();
                            pension.Telefono = reader["telefono"].ToString();
                            pension.Correo = reader["correo"].ToString();
                            pension.FechaActualizacion = Convert.ToDateTime(reader["fechaActualizacion"].ToString());
                            pension.ActualizadoPor = Convert.ToInt32(reader["actualizadoPor"].ToString());
                            pension.estatus = Convert.ToInt32(reader["estatus"].ToString());
                            pension.municipio = reader["municipio"].ToString();
                            pension.responsable = reader["responsable"].ToString();
                            pension.delegacion = reader["delegacion"].ToString();
                            ListPensiones.Add(pension);

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
            return ListPensiones;
        }
    }
}
