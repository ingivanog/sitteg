using System;

namespace GuanajuatoAdminUsuarios.Models
{
    public class catTipoGruaModel
    {
        public int IdTipoGrua { get; set; }
        public string TipoGrua { get; set; }
        public DateTime fechaActualizacion { get; set; }
        public int actualizadoPor { get; set; }
        public int estatus { get; set; }
    }
}
