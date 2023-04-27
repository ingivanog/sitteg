using GuanajuatoAdminUsuarios.Interfaces;
using GuanajuatoAdminUsuarios.Models;
using GuanajuatoAdminUsuarios.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

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
            IEnumerable<Gruas2Model> listGruas = _gruasService.GetAllGruas();
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

        /// <summary>
        /// Accion que redirige a la vista
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ajax_create()
        {
            var catDelegacione = _catDictionary.GetCatalog("CatDelegaciones", "0");
            var catClasificacionGruas = _catDictionary.GetCatalog("CatClasificacionGruas", "0");
            var catTipoGruas = _catDictionary.GetCatalog("CatTiposGrua", "0");
            var catSituacionGruas = _catDictionary.GetCatalog("CatSituacionGruas", "0");
            ViewBag.CatDelegaciones = new SelectList(catDelegacione.CatalogList, "Id", "Text");
            ViewBag.CatClasificacionGruas = new SelectList(catClasificacionGruas.CatalogList, "Id", "Text");
            ViewBag.CatTipoGruas = new SelectList(catTipoGruas.CatalogList, "Id", "Text");
            ViewBag.CatSituacionGruas = new SelectList(catSituacionGruas.CatalogList, "Id", "Text");
            return PartialView("_CrearGrua", new Gruas2Model());
        }

        [HttpPost]
        public IActionResult ajax_createGrua(Gruas2Model model)
        {
            //var model = json.ToObject<Gruas2Model>();
            var errors = ModelState.Values.Select(s => s.Errors);
            if (ModelState.IsValid)
            {
                int index = _gruasService.CrearGrua(model);
                var listPadronGruas = _gruasService.GetAllGruas();
                return PartialView("_ListadoGruas", listPadronGruas);
            }
            return RedirectToAction("Index");
        }
    }
}
