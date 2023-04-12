using GuanajuatoAdminUsuarios.Interfaces;
using GuanajuatoAdminUsuarios.Models;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GuanajuatoAdminUsuarios.Controllers
{
    public class LiberacionVehiculoController : Controller
    {
        #region DPIServices

        private readonly IPlacaServices _placaServices;
        private readonly IMarcasVehiculos _marcaServices;
        private readonly ILiberacionVehiculoService _liberacionVehiculoService;

        public LiberacionVehiculoController(IPlacaServices placaServices,
            IMarcasVehiculos marcaServices, ILiberacionVehiculoService liberacionVehiculoService)
        {
            _placaServices = placaServices;
            _marcaServices = marcaServices;
            _liberacionVehiculoService = liberacionVehiculoService;
        }


        #endregion

        public IActionResult Index()
        {
            LiberacionVehiculoBusquedaModel searchModel = new LiberacionVehiculoBusquedaModel();
            List<LiberacionVehiculoModel> ListDepositos = _liberacionVehiculoService.GetAllTopDepositos();
            searchModel.ListDepositosLiberacion = ListDepositos;
            return View(searchModel);
        }


        public JsonResult Placas_Read()
        {
            var result = new SelectList(_placaServices.GetPlacasByDelegacionId(1), "IdDepositos", "Placa");
            return Json(result);
        }

        public JsonResult Marcas_Read()
        {
            var result = new SelectList(_marcaServices.GetMarcas(), "IdMarcaVehiculo", "MarcaVehiculo");
            return Json(result);
        }

        [HttpPost]
        public ActionResult ajax_BuscarVehiculo(LiberacionVehiculoBusquedaModel model)
        {
            var ListProuctModel = _liberacionVehiculoService.GetDepositos(model);
            return PartialView("_ListadoVehiculos", ListProuctModel);

        }

    }
}
