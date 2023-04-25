using GuanajuatoAdminUsuarios.Models;
using System.Collections;
using System.Collections.Generic;

namespace GuanajuatoAdminUsuarios.Interfaces
{
    public interface IGruas2Service
    {
        public int CrearGrua(Gruas2Model model);
        public int EditarGrua(Gruas2Model model);
        public int EliminarGrua(Gruas2Model model);
        public Gruas2Model GetGruaById(int idGrua);
        public IEnumerable<Gruas2Model> GetAllGruas();
        public IEnumerable<Gruas2Model> GetGruasByIdConcesionario(int idConcesionario);
        public IEnumerable<Gruas2Model> GetGruasToGrid(string placas, string noEconomico, int? idTipoGrua);
    }
}
