using GuanajuatoAdminUsuarios.Models;
using System.Collections.Generic;

namespace GuanajuatoAdminUsuarios.Interfaces
{
    public interface IGruasService
    {
        List<GruasConcesionariosModel> GetGruasConcesionariosByIdCocesionario(int Id);
        List<TipoGruaModel> GetTipoGruas();

    }
}
