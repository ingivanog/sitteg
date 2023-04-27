using GuanajuatoAdminUsuarios.Interfaces;
using GuanajuatoAdminUsuarios.Models;
using GuanajuatoAdminUsuarios.Services;
using GuanajuatoAdminUsuarios.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using static GuanajuatoAdminUsuarios.Utils.CatalogosEnums;
using System.Linq;
using System.Net;

namespace GuanajuatoAdminUsuarios.Controllers
{
    public class TransitoTransporteController : Controller
    {
        private readonly ITransitoTransporteService _transitoTransporteService;
        private readonly IDependencias _dependeciaService;
        private readonly IGruasService _gruasService;
        private readonly IPdfGenerator<TransitoTransporteModel> _pdfService;


        public TransitoTransporteController(ITransitoTransporteService transitoTransporteService,
            IDependencias dependeciaService, IGruasService gruasService,
            IPdfGenerator<TransitoTransporteModel> pdfService
            )
        {
            _transitoTransporteService = transitoTransporteService;
            _dependeciaService = dependeciaService;
            _gruasService = gruasService;
            _pdfService = pdfService;
        }

        public IActionResult Index()
        {
            TransitoTransporteBusquedaModel searchModel = new TransitoTransporteBusquedaModel();
            List<TransitoTransporteModel> listTransitoTransporte = _transitoTransporteService.GetAllTransitoTransporte();
            searchModel.ListTransitoTransporte = listTransitoTransporte;
            return View(searchModel);
        }

        public FileResult CreatePdf()
        {
            string[] columnas = new string[] { "Uno", "Dos", "Tres", "Cuatro" };
            List<TransitoTransporteModel> listTransitoTransporte = _transitoTransporteService.GetAllTransitoTransporte();
            var result = _pdfService.CreatePdf(listTransitoTransporte, "ejemplo", 4, "Transportes", columnas);
            return File(result.Item1, "application/pdf", result.Item2);
        }


        [HttpPost]
        public ActionResult ajax_BuscarTransito(TransitoTransporteBusquedaModel model)
        {
            var ListTransitoModel = _transitoTransporteService.GetTransitoTransportes(model);
            return PartialView("_ListadoTransitoTransporte", ListTransitoModel);

        }

        public JsonResult Delegacion_Read()
        {
            var result = new SelectList(_transitoTransporteService.GetDelegaciones(), "IdDelegacion", "Delegacion");
            return Json(result);
        }

        public JsonResult Pension_Read()
        {
            var result = new SelectList(_transitoTransporteService.GetPensiones(), "IdPension", "Pension");
            return Json(result);
        }

        public JsonResult Dependencia_Read()
        {
            var result = new SelectList(_dependeciaService.GetDependencias(), "IdDependencia", "NombreDependencia");
            return Json(result);
        }

        public JsonResult Estatus_Read()
        {
            var directions = from EstatusTransitoTransporte d in Enum.GetValues(typeof(EstatusTransitoTransporte))
                             select new { ID = (int)d, Name = d.ToString() };
            var result = new SelectList(directions, "ID", "Name");

            //var result = new SelectList(_transitoTransporteService.GetPensiones(), "IdPension", "Pension");
            return Json(result);
        }


        [HttpGet]
        public ActionResult ajax_DetailGruas(int Id)
        {
            var model = _gruasService.GetGruasConcesionariosByIdCocesionario(Id);
            return PartialView("_GruasConcesionarioDetail", model);

        }

        [HttpPost]
        public ActionResult ajax_DeleteTransito(string ids)
        {
            string[] idsList = ids.Split(",");
            var result = _transitoTransporteService.DeleteTransitoTransporte(Convert.ToInt32(idsList[0]), Convert.ToInt32(idsList[1]));
            if (result > 0)
            {
                List<TransitoTransporteModel> ListTransitoTransporteModel = _transitoTransporteService.GetAllTransitoTransporte();
                return PartialView("_ListadoTransitoTransporte", ListTransitoTransporteModel);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }
        }


    }
}
