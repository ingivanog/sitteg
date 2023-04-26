using GuanajuatoAdminUsuarios.Interfaces;
using GuanajuatoAdminUsuarios.Models;
using GuanajuatoAdminUsuarios.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GuanajuatoAdminUsuarios.Controllers
{
    public class PadronGruasController : Controller
    {
        private readonly ICatDictionary _catDictionary;
        private readonly IGruasService _gruasService;
        public PadronGruasController(ICatDictionary catDictionary,
                                     IGruasService gruasService)
        {
            _catDictionary = catDictionary;
            _gruasService = gruasService;
        }
        public IActionResult Index()
        {
            IEnumerable<Gruas2Model> listGruas =  _gruasService.GetAllGruas();
            var catTipoGruas = _catDictionary.GetCatalog("CatTiposGrua", "0");
            ViewBag.CatTipoGruas = new SelectList(catTipoGruas.CatalogList, "Id", "Text");
            return View(listGruas);
        }

        [HttpGet]
        public ActionResult ajax_BuscarGruas(string placas, string noEconomico, int? idTipoGrua)
        {
            var listPadronGruas = _gruasService.GetGruasToGrid(placas, noEconomico, idTipoGrua);
            return PartialView("_ListadoGruas", listPadronGruas);

        }
    }
}
