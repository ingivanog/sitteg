﻿using GuanajuatoAdminUsuarios.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GuanajuatoAdminUsuarios.Services
{
    
        public class SqlClientConnectionBD : ISqlClientConnectionBD
        {
            private readonly IConfiguration _configuration;
            public string CadenaConexion { get; set; } = null;

            public SqlClientConnectionBD(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public string GetConnection()
            {
                CadenaConexion = _configuration.GetConnectionString("SqlClientConnectionBD");
                return CadenaConexion;
            }


        }

    }
