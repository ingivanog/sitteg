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
        public string NumeroEconimico { get; set; }

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
        public int Estatus { get; set; }

        /// <summary>
        /// Aun no se  ddl
        /// </summary>
        public int TTO_TTE { get; set; }

        /// <summary>
        /// Aun no se  ddl
        /// </summary>
        public int DependenciaTTO_TTE { get; set; }

        /// <summary>
        /// Aun no se  ddl
        /// </summary>
        public int DependenciaEncia { get; set; }

        /// <summary>
        /// tblDepositos
        /// </summary>
        public DateTime FechaIngreso { get; set; }

        /// <summary>
        /// tblDepositos
        /// </summary>
        public DateTime FechaLiberacion { get; set; }

        public List<TransitoTransporteModel> TransitoTransporte { get; set; }


    }
}
