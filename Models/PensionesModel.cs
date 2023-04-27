using System;

namespace GuanajuatoAdminUsuarios.Models
{
    public class PensionesModel
    {
        public int IdPension { get; set; }
        public int Indicador { get; set; }
        public string Pension { get; set; }
        public string Permiso { get; set; }
        public int IdDelegacion { get; set; }
        public int IdResponsable { get; set; }
        public int IdMunicipio { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Responsable { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public int ActualizadoPor { get; set; }
        public int estatus { get; set; }

        public string municipio { get; set; }

        public string responsable { get; set; }
        public string delegacion { get; set; }
        
    }
}
