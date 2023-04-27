using System.Collections.Generic;

namespace GuanajuatoAdminUsuarios.Models
{
    public class PensionesBusquedaModel
    {
   
        public string pension { get; set; }
        public int IdDelegacion { get; set; }
        public List<PensionesModel> ListPensiones { get; set; }
    }
}
