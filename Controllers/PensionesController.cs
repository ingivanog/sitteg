using GuanajuatoAdminUsuarios.Interfaces;
using GuanajuatoAdminUsuarios.Models;
using GuanajuatoAdminUsuarios.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace GuanajuatoAdminUsuarios.Controllers
{
    public class PensionesController : Controller
    {
        #region DPIServices

        private readonly IPensionesService _pensionesService;
        private readonly IDelegacionesService _delegacionesService;
        private readonly IMunicipiosService _municipiosService;
        private readonly IResponsableService _responsableService;
        

        public PensionesController(
            IPensionesService pensionesService,
            IDelegacionesService delegacionesService,
            IMunicipiosService municipiosService,
            IResponsableService responsableService
            )
        {
            _pensionesService = pensionesService;
            _delegacionesService = delegacionesService;
            _municipiosService = municipiosService;
            _responsableService = responsableService;
        }


        #endregion

        public IActionResult Index()
        {
            PensionesBusquedaModel searchModel = new PensionesBusquedaModel();
            List<PensionesModel> ListPensiones = _pensionesService.GetAllPensiones();
            searchModel.ListPensiones = ListPensiones;
            return View(searchModel);
        }


        public JsonResult Delegacion_Read()
        {
            var result = new SelectList(_delegacionesService.GetDelegaciones(), "IdDelegacion", "Delegacion");
            return Json(result);
        }

        public JsonResult Municipios_Read()
        {
            var result = new SelectList(_municipiosService.GetMunicipios(), "IdMunicipio", "Municipio");
            return Json(result);
        }

        public JsonResult Responsable_Read()
        {
            var result = new SelectList(_responsableService.GetResponsables(), "idResponsable", "responsable");
            return Json(result);
        }


        [HttpPost]
        public ActionResult ajax_BuscarPensiones(PensionesBusquedaModel model)
        {
            var ListPensionesModel = _pensionesService.GetPensiones(model);
            return PartialView("_ListadoPensiones", ListPensionesModel);

        }


        [HttpPost]
        public ActionResult CreatePartial()
        {
            return PartialView("_Create");
        }


        [HttpPost]
        public ActionResult CreatePartialModal(PensionesModel model)
        {
            //var errors = ModelState.Values.Select(s => s.Errors);
            //ModelState.Remove("CategoryName");
            if (ModelState.IsValid)
            {
                //Crear el Pension ...


                List<PensionesModel> ListPensiones = _pensionesService.GetAllPensiones();
                return PartialView("_ListadoPensiones", ListPensiones);
            }
            //SetDDLCategories();
            //return View("Create");
            return PartialView("_Create");
        }


        [HttpGet]
        public ActionResult ajax_UpdatePartial(int id)
        {
            return PartialView("_Update");
        }


        [HttpPost]
        public ActionResult UpdatePartialModal(PensionesModel model)
        {
            //var errors = ModelState.Values.Select(s => s.Errors);
            //ModelState.Remove("CategoryName");
            if (ModelState.IsValid)
            {
                //Modificar el Pension ...


                List<PensionesModel> ListPensiones = _pensionesService.GetAllPensiones();
                return PartialView("_ListadoPensiones", ListPensiones);
            }
            //SetDDLCategories();
            //return View("Create");
            return PartialView("_Create");
        }
    }
}
