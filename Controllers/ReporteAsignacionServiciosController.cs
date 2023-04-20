using GuanajuatoAdminUsuarios.Interfaces;
using GuanajuatoAdminUsuarios.Models;
using GuanajuatoAdminUsuarios.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GuanajuatoAdminUsuarios.Controllers
{
    public class ReporteAsignacionServiciosController : Controller
    {
        private readonly IPadronDepositosGruasService _padronDepositosGruasService;
        private readonly IGruasService _gruasService;
        private readonly IEventoService _eventoService;
        private readonly IReporteAsignacionService _reporteAsignacionService;


        public ReporteAsignacionServiciosController(IPadronDepositosGruasService padronDepositosGruasService,
             IGruasService gruasService, IEventoService eventoService,
             IReporteAsignacionService reporteAsignacionService
            )
        {
            _padronDepositosGruasService = padronDepositosGruasService;
            _gruasService = gruasService;
            _eventoService = eventoService;
            _reporteAsignacionService = reporteAsignacionService;
        }
        public IActionResult Index()
        {
            ReporteAsignacionBusquedaModel searchModel = new ReporteAsignacionBusquedaModel();
            List<ReporteAsignacionModel> listReporteAsignacion = _reporteAsignacionService.GetAllReporteAsignaciones();
            searchModel.ListReporteAsignacion = listReporteAsignacion;
            return View(searchModel);
        }

        [HttpPost]
        public ActionResult ajax_BuscarReporte(ReporteAsignacionBusquedaModel model)
        {
            var listReporteAsignacion = _reporteAsignacionService.GetAllReporteAsignaciones(model);
            return PartialView("_ListadoReporteAsignacion", listReporteAsignacion);

        }

        public JsonResult Evento_Read()
        {
            var result = new SelectList(_eventoService.GetEventos(), "IdEvento", "Evento");
            return Json(result);
        }

        public JsonResult Pension_Read()
        {
            var result = new SelectList(_padronDepositosGruasService.GetPensiones(), "IdPension", "Pension");
            return Json(result);
        }

        public JsonResult Grua_Read()
        {
            var result = new SelectList(_gruasService.GetGruas(), "IdGrua", "Placas");
            return Json(result);
        }
    }
}
