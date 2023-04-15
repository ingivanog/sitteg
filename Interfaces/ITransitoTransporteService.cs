using GuanajuatoAdminUsuarios.Entity;
using GuanajuatoAdminUsuarios.Models;
using System.Collections.Generic;

namespace GuanajuatoAdminUsuarios.Interfaces
{
    public interface ITransitoTransporteService
    {
        List<TransitoTransporteModel> GetAllTransitoTransporte();
        List<TransitoTransporteModel> GetTransitoTransportes(TransitoTransporteBusquedaModel model);
        List<Delegaciones> GetDelegaciones();
        List<Pensiones> GetPensiones();

    }
}
