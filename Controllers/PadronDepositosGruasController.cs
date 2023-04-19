using GuanajuatoAdminUsuarios.Interfaces;
using GuanajuatoAdminUsuarios.Models;
using GuanajuatoAdminUsuarios.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GuanajuatoAdminUsuarios.Controllers
{
    public class PadronDepositosGruasController : Controller
    {


        private readonly IPadronDepositosGruasService _padronDepositosGruasService;
        private readonly IGruasService _gruasService;
        private readonly IMunicipiosService _municipiosService;
        private readonly IConcesionariosService _concesionariosService;
        

        public PadronDepositosGruasController(IPadronDepositosGruasService padronDepositosGruasService,
             IGruasService gruasService, IMunicipiosService municipiosService, IConcesionariosService concesionariosService
            )
        {
            _padronDepositosGruasService = padronDepositosGruasService;
            _gruasService = gruasService;
            _municipiosService = municipiosService;
            _concesionariosService = concesionariosService;
        }


        public IActionResult Index()
        {
            PadronDepositosGruasBusquedaModel searchModel = new PadronDepositosGruasBusquedaModel();
            List<PadronDepositosGruasModel> listPadronDepositosGruas = _padronDepositosGruasService.GetAllPadronDepositosGruas();
            searchModel.ListPadronDepositosGruas = listPadronDepositosGruas;
            return View(searchModel);
        }

        [HttpPost]
        public ActionResult ajax_BuscarPadron(PadronDepositosGruasBusquedaModel model)
        {
            var ListPadronDepositosGruas = _padronDepositosGruasService.GetPadronDepositosGruas(model);
            return PartialView("_ListadoPadron", ListPadronDepositosGruas);

        }
        public JsonResult Municipios_Read()
        {
            var result = new SelectList(_municipiosService.GetMunicipios(), "IdMunicipio", "Municipio");
            return Json(result);
        }

        public JsonResult Concesionarios_Read()
        {
            var result = new SelectList(_concesionariosService.GetConcesionarios(), "IdConcesionario", "Concesionario");
            return Json(result);
        }

        public JsonResult Deposito_Read()
        {
            var result = new SelectList(_concesionariosService.GetConcesionarios(), "IdConcesionario", "Concesionario");
            return Json(result);
        }

        public JsonResult TipoGrua_Read()
        {
            var result = new SelectList(_gruasService.GetTipoGruas(), "IdTipoGrua", "TipoGrua");
            return Json(result);
        }



    }
}
