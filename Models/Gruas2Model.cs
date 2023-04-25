namespace GuanajuatoAdminUsuarios.Models
{
    public class Gruas2Model : EntityModel
    {
        public int idGrua { get; set; }
        public int idConcesionario { get; set; }
        public int idClasificacion { get; set; }
        public int idTipoGrua { get; set; }
        public int idSituacion { get; set; }
        public string noEconomico { get; set; }
        public string placas { get; set; }
        public string modelo { get; set; }
        public string capacidad { get; set; }
       

    }
}
