using System;
using System.Collections.Generic;

namespace GuanajuatoAdminUsuarios.Models
{
    public class ReporteAsignacionBusquedaModel
    {
        public int IdGrua { get; set; }
        public int IdPension { get; set; }
        public int IdEvento { get; set; }
        public string Evento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<ReporteAsignacionModel> ListReporteAsignacion { get; set; }
    }
}
