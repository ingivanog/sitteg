using GuanajuatoAdminUsuarios.Models;
using System.Collections.Generic;

namespace GuanajuatoAdminUsuarios.Interfaces
{
    public interface IConcesionariosService
    {
        List<ConcesionariosModel> GetConcesionarios();
    }
}
