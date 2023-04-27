﻿using GuanajuatoAdminUsuarios.Interfaces;
using GuanajuatoAdminUsuarios.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace GuanajuatoAdminUsuarios.Services
{
    public class CatalogosService : ICatalogosService
    {
        private readonly ISqlClientConnectionBD _sqlClientConnectionBD;
        public CatalogosService(ISqlClientConnectionBD sqlClientConnectionBD)
        {
            _sqlClientConnectionBD = sqlClientConnectionBD;
        }
        public List<Dictionary<string, string>> GetGenericCatalogos(string tabla, string[] campos)
        {
            List<Dictionary<string, string>> modelList = new List<Dictionary<string, string>>();
            string strParams = string.Join(",", campos);
            string strQuery = @"SELECT
                                {0}
                                FROM {1}
                                WHERE estatus = 1";
            strQuery = string.Format(strQuery, strParams, tabla);
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
                            Dictionary<string, string> dictionary = new Dictionary<string, string>();
                            foreach (string campo in campos)
                            {
                                dictionary.Add(campo, Convert.ToString(reader[campo]));
                            }
                            modelList.Add(dictionary);
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
            return modelList;
        }
    }
}
