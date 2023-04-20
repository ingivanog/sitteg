﻿using GuanajuatoAdminUsuarios.Interfaces;
using GuanajuatoAdminUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static GuanajuatoAdminUsuarios.Models.PadronDepositosGruasModel;

namespace GuanajuatoAdminUsuarios.Services
{
    public class PadronDepositosGruasService : IPadronDepositosGruasService
    {

        private readonly ISqlClientConnectionBD _sqlClientConnectionBD;
        public PadronDepositosGruasService(ISqlClientConnectionBD sqlClientConnectionBD)
        {
            _sqlClientConnectionBD = sqlClientConnectionBD;
        }

        private List<PadronDepositosGruasModel> GetJoinsPadronDepositosGruas()
        {
            List<PadronDepositosGruasModel> PadronDepositosGruasList = new List<PadronDepositosGruasModel>();
            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
                try
                {
                    connection.Open();

                    #region first query
                    //const string SqlTransact1 =
                    //  @"select tg.TipoGrua, tg.IdTipoGrua, g.IdGrua,g.noEconomico,g.Placas,g.Modelo,
                    //        c.IdConcesionario,c.Concesionario,g.noEconomico,g.placas,g.modelo,g.capacidad,
                    //        g.clasificacion,c.Concesionario,dep.IdDeposito,dep.IdPension,dep.IdConcesionario,p.Pension,
                    //        p.Direccion, p.Telefono,  p.IdMunicipio, m.Municipio
                    //        from municipios m 
                    //        inner join pensiones p on m.idMunicipio = p.idMunicipio
                    //        inner join depositos dep on p.idPension = dep.idPension
                    //        inner join Concesionarios c	on dep.IdConcesionario= c.IdConcesionario
                    //        left join  GruasConcesionario gc on c.IdConcesionario= gc.IdConcesionario
                    //        left join Gruas g on gc.IdGrua= g.IdGrua
                    //        left join catTipoGrua tg ON g.IdTipoGrua = tg.IdTipoGrua";
                    #endregion

                    const string SqlTransact =
                        @"select tg.TipoGrua, tg.IdTipoGrua, g.IdGrua,g.noEconomico,g.Placas,g.Modelo,
                            c.IdConcesionario,c.Concesionario,g.noEconomico,g.placas,g.modelo,g.capacidad,
                            g.clasificacion,c.Concesionario,dep.IdDeposito,dep.IdPension,dep.IdConcesionario,p.Pension,
                            p.Direccion, p.Telefono,  p.IdMunicipio, m.Municipio
                           from gruas g 
                            inner join GruasConcesionario gc on g.IdGrua = gc.IdGrua
                            inner join Concesionarios c on gc.IdConcesionario= c.IdConcesionario
                            inner join catTipoGrua tg on g.IdTipoGrua= tg.IdTipoGrua
                            inner join depositos dep on c.IdConcesionario =dep.IdConcesionario
                            inner join pensiones p on dep.idPension= p.idPension
                            inner join municipios m on p.idMunicipio = m.idMunicipio
                            order by  g.IdGrua,c.IdConcesionario";

                    SqlCommand command = new SqlCommand(SqlTransact, connection);

                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            PadronDepositosGruasModel padronDepositosGruas = new PadronDepositosGruasModel();

                            padronDepositosGruas.IdTipoGrua = Convert.ToInt32(reader["IdTipoGrua"].ToString());
                            padronDepositosGruas.TipoGrua = reader["TipoGrua"].ToString();
                            padronDepositosGruas.IdGrua = Convert.ToInt32(reader["IdGrua"].ToString());
                            padronDepositosGruas.noEconomico = reader["noEconomico"].ToString();
                            padronDepositosGruas.Placas = reader["placas"].ToString();
                            padronDepositosGruas.Modelo = reader["modelo"].ToString();
                            padronDepositosGruas.Concesionario = reader["Concesionario"].ToString();
                            padronDepositosGruas.IdDeposito = Convert.ToInt32(reader["IdDeposito"].ToString());
                            padronDepositosGruas.IdConcesionario = Convert.ToInt32(reader["IdConcesionario"].ToString());
                            padronDepositosGruas.IdPension = Convert.ToInt32(reader["IdPension"].ToString());
                            padronDepositosGruas.Pension = reader["Pension"].ToString();
                            padronDepositosGruas.Direccion = reader["Direccion"].ToString();
                            padronDepositosGruas.Telefono = reader["Telefono"].ToString();
                            padronDepositosGruas.IdMunicipio = Convert.ToInt32(reader["IdMunicipio"].ToString());
                            padronDepositosGruas.Municipio = reader["Municipio"].ToString();
                            PadronDepositosGruasList.Add(padronDepositosGruas);
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
            return PadronDepositosGruasList;
        }

        public List<PadronDepositosGruasModel> GetAllPadronDepositosGruas()
        {
            var modelList = GetJoinsPadronDepositosGruas();
            var indices = modelList
                         .Select((s, i) => new { index = i, item = s })
                         .GroupBy(grp => grp.item.IdPension)
                         //.Where(w => !string.IsNullOrEmpty(w.))
                         .SelectMany(sm => sm.Select(s => s.index))
                         .ToList();

            var ListaAgrupada = modelList.Select((s, i) => new { index = i, items = s })
                             .GroupBy(x => indices.FirstOrDefault(r => r > x.index))
                             .Select(s => s.Select(ss => ss.items).ToList())
                             .ToList();

            List<PadronDepositosGruasModel> ListItems = new List<PadronDepositosGruasModel>();
            List<PensionPadronModel> padronPension = new List<PensionPadronModel>();
            foreach (var item in ListaAgrupada)
            {

                if (item.Count() > 1)
                {

                    foreach (var itemInside in item)
                    {
                        PensionPadronModel pension = new PensionPadronModel();
                        pension.IdPension = itemInside.IdPension;
                        pension.Pension = itemInside.Pension;
                        pension.Telefono = itemInside.Telefono;
                        pension.Direccion = itemInside.Direccion;
                        pension.IdMunicipio = itemInside.IdMunicipio;
                        padronPension.Add(pension);
                    }

                    foreach (var itemInside in item)
                    {
                        itemInside.Pensiones = padronPension;
                        ListItems.Add(itemInside);
                    }

                    //var one = item.FirstOrDefault();
                    //one.Pensiones = padronPension;
                    //ListItems.Add(one);
                }
                else
                {
                    ListItems.Add(item.First());
                }
            }
            return ListItems;
        }

        public List<PensionesModel> GetPensiones()
        {
            List<PensionesModel> ListPensiones = new List<PensionesModel>();

            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("Select * from pensiones where estatus=1 ", connection);
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            PensionesModel pension = new PensionesModel();
                            pension.IdPension = Convert.ToInt32(reader["IdPension"].ToString());
                            pension.Pension = reader["Pension"].ToString();
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

        private List<PadronDepositosGruasModel> GetJoinsPadronDepositosGruas(PadronDepositosGruasBusquedaModel model)
        {
            List<PadronDepositosGruasModel> PadronDepositosGruasList = new List<PadronDepositosGruasModel>();
            using (SqlConnection connection = new SqlConnection(_sqlClientConnectionBD.GetConnection()))
                try
                {
                    connection.Open();
                    const string SqlTransact =
                        @"select tg.TipoGrua, tg.IdTipoGrua, g.IdGrua,g.noEconomico,g.Placas,g.Modelo,
                        c.IdConcesionario,c.Concesionario,g.noEconomico,g.placas,g.modelo,g.capacidad,
                        g.clasificacion,c.Concesionario,dep.IdDeposito,dep.IdPension,dep.IdConcesionario,p.Pension,
                        p.Direccion, p.Telefono,  p.IdMunicipio, m.Municipio
                        from gruas g 
                        inner join GruasConcesionario gc on g.IdGrua = gc.IdGrua
                        inner join Concesionarios c on gc.IdConcesionario= c.IdConcesionario
                        inner join catTipoGrua tg on g.IdTipoGrua= tg.IdTipoGrua
                        inner join depositos dep on c.IdConcesionario =dep.IdConcesionario
                        inner join pensiones p on dep.idPension= p.idPension
                        inner join municipios m on p.idMunicipio = m.idMunicipio
                        where  dep.estatus=1 AND p.estatus=1 AND( m.idMunicipio=@IdMunicipio OR c.IdConcesionario=@IdConcesionario 
                        OR p.idPension=@IdPension OR  tg.IdTipoGrua=@IdTipoGrua)
                        order by  g.IdGrua,c.IdConcesionario";

                    SqlCommand command = new SqlCommand(SqlTransact, connection);
                    command.Parameters.Add(new SqlParameter("@IdMunicipio", SqlDbType.Int)).Value = (object)model.IdMunicipio ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@IdConcesionario", SqlDbType.Int)).Value = (object)model.IdConcesionario ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@IdPension", SqlDbType.Int)).Value = (object)model.IdPension ?? DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@IdTipoGrua", SqlDbType.Int)).Value = (object)model.IdTipoGrua ?? DBNull.Value;
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            PadronDepositosGruasModel padronDepositosGruas = new PadronDepositosGruasModel();

                            padronDepositosGruas.IdTipoGrua = Convert.ToInt32(reader["IdTipoGrua"].ToString());
                            padronDepositosGruas.TipoGrua = reader["TipoGrua"].ToString();
                            padronDepositosGruas.IdGrua = Convert.ToInt32(reader["IdGrua"].ToString());
                            padronDepositosGruas.noEconomico = reader["noEconomico"].ToString();
                            padronDepositosGruas.Placas = reader["placas"].ToString();
                            padronDepositosGruas.Modelo = reader["modelo"].ToString();
                            padronDepositosGruas.Concesionario = reader["Concesionario"].ToString();
                            padronDepositosGruas.IdDeposito = Convert.ToInt32(reader["IdDeposito"].ToString());
                            padronDepositosGruas.IdConcesionario = Convert.ToInt32(reader["IdConcesionario"].ToString());
                            padronDepositosGruas.IdPension = Convert.ToInt32(reader["IdPension"].ToString());
                            padronDepositosGruas.Pension = reader["Pension"].ToString();
                            padronDepositosGruas.Direccion = reader["Direccion"].ToString();
                            padronDepositosGruas.Telefono = reader["Telefono"].ToString();
                            padronDepositosGruas.IdMunicipio = Convert.ToInt32(reader["IdMunicipio"].ToString());
                            padronDepositosGruas.Municipio = reader["Municipio"].ToString();
                            PadronDepositosGruasList.Add(padronDepositosGruas);
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
            return PadronDepositosGruasList;
        }


        public List<PadronDepositosGruasModel> GetPadronDepositosGruas(PadronDepositosGruasBusquedaModel model)
        {
            var modelList = GetJoinsPadronDepositosGruas(model);
            var indices = modelList
                         .Select((s, i) => new { index = i, item = s })
                         .GroupBy(grp => grp.item.IdPension)
                         //.Where(w => !string.IsNullOrEmpty(w.))
                         .SelectMany(sm => sm.Select(s => s.index))
                         .ToList();

            var ListaAgrupada = modelList.Select((s, i) => new { index = i, items = s })
                             .GroupBy(x => indices.FirstOrDefault(r => r > x.index))
                             .Select(s => s.Select(ss => ss.items).ToList())
                             .ToList();

            List<PadronDepositosGruasModel> ListItems = new List<PadronDepositosGruasModel>();
            List<PensionPadronModel> padronPension = new List<PensionPadronModel>();
            foreach (var item in ListaAgrupada)
            {

                if (item.Count() > 1)
                {

                    foreach (var itemInside in item)
                    {
                        PensionPadronModel pension = new PensionPadronModel();
                        pension.IdPension = itemInside.IdPension;
                        pension.Pension = itemInside.Pension;
                        pension.Telefono = itemInside.Telefono;
                        pension.Direccion = itemInside.Direccion;
                        pension.IdMunicipio = itemInside.IdMunicipio;
                        padronPension.Add(pension);
                    }

                    foreach (var itemInside in item)
                    {
                        itemInside.Pensiones = padronPension;
                        ListItems.Add(itemInside);
                    }

                    //var one = item.FirstOrDefault();
                    //one.Pensiones = padronPension;
                    //ListItems.Add(one);
                }
                else
                {
                    ListItems.Add(item.First());
                }
            }
            return ListItems;
        }
    }
}
