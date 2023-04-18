using System;
using System.Collections.Generic;

namespace GuanajuatoAdminUsuarios.Models
{
    public class TransitoTransporteBusquedaModel
    {
        /// <summary>
        /// TblDepositos
        /// </summary>
        public string Placas { get; set; }

        /// <summary>
        /// tblsolicitudes
        /// </summary>
        public string FolioSolicitud { get; set; }

        /// <summary>
        /// tblInfracciones Aun no Creada
        /// </summary>
        public string FolioInfraccion { get; set; }

        /// <summary>
        /// tblVehiculo
        /// </summary>
        public string Propietario { get; set; }

        /// <summary>
        /// tblVehiculo
        /// </summary>
        public string NumeroEconomico { get; set; }

        /// <summary>
        /// tblDelegacion ddl
        /// </summary>
        public int IdDelegacion { get; set; }

        /// <summary>
        /// tblPension ddl
        /// </summary>
        public int IdPension { get; set; }

        /// <summary>
        /// Aun no se  ddl
        /// </summary>
        public int IdEstatus { get; set; }

        /// <summary>
        /// Aun no se  ddl
        /// </summary>
        public int IdDependenciaGenera { get; set; }

        /// <summary>
        /// Aun no se  ddl
        /// </summary>
        public int IdDependenciaTransito { get; set; }

        /// <summary>
        /// Aun no se  ddl
        /// </summary>
        public int IdDependenciaNoTransito { get; set; }

        /// <summary>
        /// tblDepositos
        /// </summary>
        public DateTime FechaIngreso { get; set; }

        /// <summary>
        /// tblDepositos
        /// </summary>
        public DateTime FechaIngresoFin { get; set; }

        public List<TransitoTransporteModel> ListTransitoTransporte { get; set; }


    }
}
