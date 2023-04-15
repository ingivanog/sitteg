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

namespace GuanajuatoAdminUsuarios.Controllers
{
    public class TransitoTransporteController : Controller
    {
        private readonly ITransitoTransporteService _transitoTransporteService;

        public TransitoTransporteController(ITransitoTransporteService transitoTransporteService)
        {
            _transitoTransporteService = transitoTransporteService;
        }

        public IActionResult Index()
        {
            TransitoTransporteBusquedaModel searchModel = new TransitoTransporteBusquedaModel();
            List<TransitoTransporteModel> listTransitoTransporte = _transitoTransporteService.GetAllTransitoTransporte();
            searchModel.ListTransitoTransporte = listTransitoTransporte;
            return View(searchModel);
        }

        [HttpPost]
        public ActionResult ajax_BuscarTransito(TransitoTransporteBusquedaModel model)
        {
            var ListTransitoModel = _transitoTransporteService.GetTransitoTransportes(model);
            return PartialView("_ListadoVehiculos", ListTransitoModel);

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
            //var model = _liberacionVehiculoService.GetDepositoByID(Id);
            //model.FechaIngreso.ToString("dd/MM/yyyy");
            return PartialView("_UpdateLiberacion", null);

        }




    }
}
