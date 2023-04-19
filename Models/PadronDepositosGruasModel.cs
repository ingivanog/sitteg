namespace GuanajuatoAdminUsuarios.Models
{
    public class PadronDepositosGruasModel
    {

        #region TipoGrua
        public int IdTipoGrua { get; set; }
        public string TipoGrua { get; set; }
        #endregion

        #region Grua
        public int IdGrua { get; set; }
        public string Grua { get; set; }
        public string noEconomico { get; set; }
        public string Placas { get; set; }
        public string Modelo { get; set; }
        #endregion

        #region Concesionario
       
        public string Concesionario { get; set; }
        #endregion

        #region Deposito
        public int IdDeposito { get; set; }
        public int IdPension { get; set; }
        public int IdConcesionario { get; set; }
        #endregion

        #region Pension
        public string Pension { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int IdMunicipio { get; set; }
        #endregion

        #region Municipio
        public string Municipio { get; set; }

        #endregion




    }
}
