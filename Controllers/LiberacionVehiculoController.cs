using GuanajuatoAdminUsuarios.Interfaces;
using GuanajuatoAdminUsuarios.Models;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
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

        [HttpGet]
        public ActionResult ajax_UpdateLiberacion(int Id)
        {
            var model = _liberacionVehiculoService.GetDepositoByID(Id);
            model.FechaIngreso.ToString("dd/MM/yyyy");
            return PartialView("_UpdateLiberacion", model);

        }


        [HttpPost]
        public ActionResult UpdateLiberacion(LiberacionVehiculoModel model, IFormFile ImageAcreditacionPropiedad, IFormFile ImageAcreditacionPersonalidad, IFormFile ImageReciboPago)
        {
            //if (model.AcreditacionPropiedad.ContentLength > 0)
            //{
            //MemoryStream memoryStream = new MemoryStream();
            //ImageOne.co = memoryStream.ToArray();

            try
            {

                if (ImageAcreditacionPropiedad != null)
                {
                    using (var ms1 = new MemoryStream())
                    {
                        ImageAcreditacionPropiedad.CopyTo(ms1);
                        model.AcreditacionPropiedad = ms1.ToArray();
                    }
                }
                if (ImageAcreditacionPersonalidad != null)
                {
                    using (var ms2 = new MemoryStream())
                    {
                        ImageAcreditacionPersonalidad.CopyTo(ms2);
                        model.AcreditacionPersonalidad = ms2.ToArray();
                    }
                }
                if (ImageReciboPago != null)
                {
                    using (var ms3 = new MemoryStream())
                    {
                        ImageReciboPago.CopyTo(ms3);
                        model.ReciboPago = ms3.ToArray();
                    }
                }


                var result = _liberacionVehiculoService.UpdateDeposito(model);
                //var imgByte = model.AcreditacionPropiedad;
                //return new FileContentResult(imgByte, "image/jpeg");



            }
            catch (Exception ex)
            {

            }
            return PartialView("_UpdateLiberacion", model);
        }


    }
}
