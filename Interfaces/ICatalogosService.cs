using System.Collections.Generic;

namespace GuanajuatoAdminUsuarios.Interfaces
{
    public interface ICatalogosService
    {
        public List<Dictionary<string, string>> GetGenericCatalogos(string tabla, string[] campos);
    }
}
