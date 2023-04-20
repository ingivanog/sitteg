using GuanajuatoAdminUsuarios.Entity;
using System;

namespace GuanajuatoAdminUsuarios.Models
{
    public class ReporteAsignacionModel
    {
        public int idSolicitud { get; set; }
        public string vehiculoCarretera { get; set; }
        public string vehiculoTramo { get; set; }
        public string vehiculoKm { get; set; }

        /// <summary>
        /// Filtro
        /// </summary>
        public DateTime fechaSolicitud { get; set; }

        //ToDo:no  esta Fecha Salida de donde lo tomo
        
        /// <summary>
        /// Filtro
        /// </summary>
        public string evento { get; set; }
        public string solicitanteNombre { get; set; }
        public string solicitanteAp { get; set; }
        public string solicitanteAm { get; set; }

        //Direccion
        public string solicitanteColonia { get; set; }
        public string solicitanteCalle { get; set; }
        public string solicitanteNumero { get; set; }

        public string tipoVehiculo { get; set; }

        //ToDo:sobre que filtro grua? ya que el drop esta sobre id y placa
        public string propietarioGrua { get; set; }

        //ToDo no esta alias
        //ToDo no esta motivo

        public string oficial { get; set; }
        public string folio { get; set; }

        //ToDo no esta inventario

        /// <summary>
        /// Filtro
        /// </summary>
        public string vehiculoPension { get; set; }

        //ToDo no esta delegacion


        public string servicio { get; set; }
        public string tipoUsuario { get; set; }               
        public string solicitanteTel { get; set; }
        public string solicitanteEntidad { get; set; }
        public string solicitanteMunicipio { get; set; }
        public string vehiculoCalle { get; set; }
        public string vehiculoNumero { get; set; }
        public string vehiculoColonia { get; set; }
        
        public string vehiculoEntidad { get; set; }
        public string vehiculoMunicipio { get; set; }
  
        public string vehiculoInterseccion { get; set; }
  
        public int idVehiculo { get; set; }
        public int idInfraccion { get; set; }
        public DateTime fechaActualizacion { get; set; }
        public string actualizadoPor { get; set; }
        public string estatus { get; set; }
        public int IdDependencia { get; set; }

    }
}
