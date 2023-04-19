using GuanajuatoAdminUsuarios.Interfaces;
using GuanajuatoAdminUsuarios.Models;
using System.Collections.Generic;

namespace GuanajuatoAdminUsuarios.Services
{
    public class PadronDepositosGruasService : IPadronDepositosGruasService
    {

        private readonly ISqlClientConnectionBD _sqlClientConnectionBD;
        public PadronDepositosGruasService(ISqlClientConnectionBD sqlClientConnectionBD)
        {
            _sqlClientConnectionBD = sqlClientConnectionBD;
        }
        public List<PadronDepositosGruasModel> GetAllPadronDepositosGruas()
        {
            return null;
        }
        public List<PadronDepositosGruasModel> GetPadronDepositosGruas(PadronDepositosGruasBusquedaModel model)
        {
            return null;
        }
    }
}
