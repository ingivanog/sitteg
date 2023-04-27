using GuanajuatoAdminUsuarios.Models;
using System.Collections.Generic;

namespace GuanajuatoAdminUsuarios.Interfaces
{
    public interface IPensionesService
    {
        List<PensionesModel> GetAllPensiones();
        List<PensionesModel> GetPensiones(PensionesBusquedaModel model);
    }
}
