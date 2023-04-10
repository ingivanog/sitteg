﻿using System;

namespace GuanajuatoAdminUsuarios.Models
{
    public class SalariosMinimosModel
    {
        public int IdSalario { get; set; }

        public string Area { get; set; }

        public float Salario { get; set; }
        public DateTime? Fecha { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public int? ActualizadoPor { get; set; }

        public int? Estatus { get; set; }

        public string estatusDesc { get; set; }

    }
}
