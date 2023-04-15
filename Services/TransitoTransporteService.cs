using GuanajuatoAdminUsuarios.Entity;
using GuanajuatoAdminUsuarios.Interfaces;
using GuanajuatoAdminUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GuanajuatoAdminUsuarios.Services
{
    public class TransitoTransporteService : ITransitoTransporteService
    {
        private readonly ISqlClientConnectionBD _sqlClientConnectionBD;
        public TransitoTransporteService(ISqlClientConnectionBD sqlClientConnectionBD)
        {
            _sqlClientConnectionBD = sqlClientConnectionBD;
        }

        public List<TransitoTransporteModel> GetAllTransitoTransporte()
        {
            #region Consulta base
            //var query = @"select top(100) d.IdDeposito,d.IdSolicitud,d.IdDelegacion,d.IdMarca,d.IdSubmarca,d.IdPension,d.IdTramo,
            //            d.IdColor,d.Serie,d.Placa,d.FechaIngreso,d.Folio,d.Km,d.Liberado,d.Autoriza,d.FechaActualizacion,
            //            del.delegacion, d.ActualizadoPor, d.estatus, m.marcaVehiculo,subm.nombreSubmarca,sol.solicitanteNombre,
            //            sol.solicitanteAp,sol.solicitanteAm, col.color,pen.pension,cTra.tramo,
            //            --esto es nuevo
            //            sol.fechaSolicitud,sol.folio, dep.IdDependencia,dep.nombreDependencia,inf.IdInfraccion,inf.FolioInfraccion,
            //            veh.idVehiculo,veh.propietario,veh.numeroEconomico,
            //            g.IdGrua,g.Grua

            //            from depositos d inner join delegaciones del on d.idDelegacion= del.idDelegacion
            //            inner join marcasVehiculos m on d.idMarca=m.idMarcaVehiculo
            //            inner join colores col on d.idColor = col.idColor
            //            inner join pensiones pen on d.idPension	= pen.idPension
            //            inner join catTramos cTra  on d.Idtramo=cTra.idTramo
            //            inner join submarcasVehiculos  subm on m.idMarcaVehiculo=subm.idMarcaVehiculo
            //            inner join solicitudes sol on d.idSolicitud = sol.idSolicitud
            //            inner join dependencias dep on sol.IdDependencia = dep.IdDependencia
            //            inner join Infracciones inf on sol.idInfraccion = inf.IdInfraccion
            //            inner join	vehiculos  veh on sol.idVehiculo =veh.idVehiculo 
            //            left join 	GruasDepositos gd on  d.IdDeposito=	 gd.IdDeposito
            //            left join Gruas g on g.IdGrua= gd.IdGrua
            //            where 
            //            --d.liberado=0 and d.estatus=1	and
            //            (d.IdDeposito=@IdDeposito  OR d.IdMarca=@IdMarca 
            //            OR d.Serie LIKE '%' + @Serie + '%' OR d.FechaIngreso =@FechaIngreso 
            //            OR d.Folio LIKE '%' + @Folio + '%')";
            #endregion

            List<TransitoTransporteModel> transitoList = new List<TransitoTransporteModel>();
            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
                try
                {
                    connection.Open();
                    const string SqlTransact =
                        @"select top(100) d.iddeposito,d.idsolicitud,d.iddelegacion,d.idmarca,d.idsubmarca,d.idpension,d.idtramo,
                        d.idcolor,d.serie,d.placa,d.fechaingreso,d.folio,d.km,d.liberado,d.autoriza,d.fechaactualizacion,
                        del.delegacion, d.actualizadopor, d.estatus, m.marcavehiculo,subm.nombresubmarca,sol.solicitantenombre,
                        sol.solicitanteap,sol.solicitanteam, col.color,pen.pension,ctra.tramo,
                       
                        sol.fechasolicitud, sol.folio as FolioSolicitud, dep.iddependencia,dep.nombredependencia,inf.idinfraccion,inf.folioinfraccion,
                        veh.idvehiculo,veh.propietario,veh.numeroeconomico,
                        g.idgrua,g.grua
                        from depositos d inner join delegaciones del on d.iddelegacion= del.iddelegacion
                        inner join marcasvehiculos m on d.idmarca=m.idmarcavehiculo
                        inner join colores col on d.idcolor = col.idcolor
                        inner join pensiones pen on d.idpension	= pen.idpension
                        inner join cattramos ctra  on d.idtramo=ctra.idtramo
                        inner join submarcasvehiculos  subm on m.idmarcavehiculo=subm.idmarcavehiculo
                        inner join solicitudes sol on d.idsolicitud = sol.idsolicitud
                        inner join dependencias dep on sol.iddependencia = dep.iddependencia
                        inner join infracciones inf on sol.idinfraccion = inf.idinfraccion
                        inner join	vehiculos  veh on sol.idvehiculo =veh.idvehiculo 
                        left join 	gruasdepositos gd on  d.iddeposito=	 gd.iddeposito
                        left join gruas g on g.idgrua= gd.idgrua";

                    SqlCommand command = new SqlCommand(SqlTransact, connection);
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            TransitoTransporteModel transito = new TransitoTransporteModel();
                            transito.IdDeposito = Convert.ToInt32(reader["IdDeposito"].ToString());
                            transito.IdSolicitud = Convert.ToInt32(reader["IdSolicitud"].ToString());
                            transito.IdDelegacion = Convert.ToInt32(reader["IdDelegacion"].ToString());
                            transito.IdMarca = Convert.ToInt32(reader["IdMarca"].ToString());
                            transito.IdSubmarca = Convert.ToInt32(reader["IdSubmarca"].ToString());
                            transito.IdPension = Convert.ToInt32(reader["IdPension"].ToString());
                            transito.IdTramo = Convert.ToInt32(reader["IdTramo"].ToString());
                            transito.IdColor = Convert.ToInt32(reader["IdColor"].ToString());
                            transito.Serie = reader["Serie"].ToString();
                            transito.Placa = reader["Placa"].ToString();
                            transito.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"].ToString());
                            transito.Folio = reader["Folio"].ToString();
                            transito.Km = reader["Km"].ToString();
                            transito.Liberado = Convert.ToInt32(reader["Liberado"].ToString());
                            transito.Autoriza = reader["Autoriza"].ToString();
                            transito.FechaActualizacion = Convert.ToDateTime(reader["FechaActualizacion"].ToString());
                            transito.ActualizadoPor = Convert.ToInt32(reader["ActualizadoPor"].ToString());
                            transito.DepositoEstatus = Convert.ToInt32(reader["Estatus"].ToString());
                            transito.marcaVehiculo = reader["marcaVehiculo"].ToString();
                            transito.nombreSubmarca = reader["nombreSubmarca"].ToString();
                            transito.delegacion = reader["delegacion"].ToString();

                            transito.solicitanteNombre = reader["solicitanteNombre"].ToString();
                            transito.solicitanteAp = reader["solicitanteAp"].ToString();
                            transito.solicitanteAm = reader["solicitanteAm"].ToString();
                            transito.Color = reader["Color"].ToString();
                            transito.pension = reader["pension"].ToString();
                            transito.tramo = reader["tramo"].ToString();

                            //nuevos
                            transito.FechaSolicitud = Convert.ToDateTime(reader["FechaSolicitud"].ToString());
                            transito.IdDependencia = Convert.ToInt32(reader["IdDependencia"].ToString());
                            transito.NombreDependencia = reader["NombreDependencia"].ToString();
                            transito.IdInfraccion = Convert.ToInt32(reader["IdInfraccion"].ToString());
                            transito.FolioInfraccion = reader["FolioInfraccion"].ToString();
                            transito.IdVehiculo = Convert.ToInt32(reader["IdVehiculo"].ToString());
                            transito.propietario = reader["propietario"].ToString();
                            transito.numeroEconomico = reader["propietario"].ToString();
                            transito.IdGrua = Convert.ToInt32(reader["IdGrua"].ToString());
                            transito.Grua = reader["Grua"].ToString();
                            transito.FolioSolicitud = reader["FolioSolicitud"].ToString();
                            //transito.Folio = reader["Folio"].ToString();
                            transitoList.Add(transito);

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
            return transitoList;
        }

        public List<TransitoTransporteModel> GetTransitoTransportes(TransitoTransporteBusquedaModel model)
        {
            return null;
        }

        /// <summary>
        /// Servicio Provicional ya que no tenemos acceso a repositorio
        /// </summary>
        /// <returns></returns>
        public List<Delegaciones> GetDelegaciones()
        {
            List<Delegaciones> delegaciones = new List<Delegaciones>();

            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
                try

                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("Select * from delegaciones", connection);
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            Delegaciones delegacion = new Delegaciones();
                            delegacion.IdDelegacion = Convert.ToInt32(reader["idDelegacion"].ToString());
                            delegacion.Delegacion = reader["delegacion"].ToString();
                            delegaciones.Add(delegacion);
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
            return delegaciones;
        }

        /// <summary>
        /// Servicio Provicional ya que no tenemos acceso a repositorio
        /// </summary>
        /// <returns></returns>
        public List<Pensiones> GetPensiones()
        {
            List<Pensiones> pensiones = new List<Pensiones>();

            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
                try

                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("Select * from pensiones", connection);
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            Pensiones pension = new Pensiones();
                            pension.IdPension = Convert.ToInt32(reader["idPension"].ToString());
                            pension.Pension = reader["pension"].ToString();
                            pensiones.Add(pension);
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
            return pensiones;
        }


    }
}
