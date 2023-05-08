﻿using GuanajuatoAdminUsuarios.Entity;
using GuanajuatoAdminUsuarios.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Example.WebUI.Controllers
{
    public class CatHospitalesController : Controller
    {
        DBContextInssoft dbContext = new DBContextInssoft();
        public IActionResult Index()
        {

            var ListHospitalesModel = GetHospitales();

            return View(ListHospitalesModel);

        }




        #region Modal Action
        public ActionResult IndexModal()
        {
            var ListHospitalesModel = GetHospitales();
            return View("Index", ListHospitalesModel);
        }

        [HttpPost]
        public ActionResult AgregarHospitalModal()
        {
            SetDDLMunicipios();
            return PartialView("_Crear");
        }

        public ActionResult EditarHospitalModal(int IdHospital)
        {
            SetDDLMunicipios();
            var hospitalesModel = GetHospitalByID(IdHospital);
            return PartialView("_Editar", hospitalesModel);
        }

        public ActionResult EliminarHospitalModal(int IdHospital)
        {
            SetDDLMunicipios();
            var hospitalesModel = GetHospitalByID(IdHospital);
            return PartialView("_Eliminar", hospitalesModel);
        }



        [HttpPost]
        public ActionResult AgregarHospital(CatHospitalesModel model)
        {
            var errors = ModelState.Values.Select(s => s.Errors);
            ModelState.Remove("NombreHospital");
            if (ModelState.IsValid)
            {


                CrearHospital(model);
                var ListHospitalesModel = GetHospitales();
                return PartialView("_ListaHospitales", ListHospitalesModel);
            }

            return PartialView("_Crear");
        }

        public ActionResult EditarHospitalMod(CatHospitalesModel model)
        {
            bool switchHospitales = Request.Form["hospitalesSwitch"].Contains("true");
            model.Estatus = switchHospitales ? 1 : 0;
            var errors = ModelState.Values.Select(s => s.Errors);
            ModelState.Remove("NombreHospital");
            if (ModelState.IsValid)
            {


                EditarHospital(model);
                var ListHospitalesModel = GetHospitales();
                return PartialView("_ListaHospitales", ListHospitalesModel);
            }
            return PartialView("_Editar");
        }

        public ActionResult EliminarHospitalMod(CatHospitalesModel model)
        {
            var errors = ModelState.Values.Select(s => s.Errors);
            ModelState.Remove("NombreHospital");
            if (ModelState.IsValid)
            {

                EliminarHospital(model);
                var ListHospitalesModel = GetHospitales();
                return PartialView("_ListaHospitales", ListHospitalesModel);
            }
            return PartialView("_Eliminar");
        }
        public JsonResult GetHospitalesLista([DataSourceRequest] DataSourceRequest request)
        {
            var ListHospitalesModel = GetHospitales();

            return Json(ListHospitalesModel.ToDataSourceResult(request));
        }

        private void SetDDLMunicipios()
        {
            ///Espacio en memoria de manera temporal que solo existe en la petición bool, list, string ,clases , selectlist
            ViewBag.Municipios = new SelectList(dbContext.CatMunicipios.ToList(), "IdMunicipio", "Municipio");
        }



        #endregion


        #region Acciones a base de datos

        public void CrearHospital(CatHospitalesModel model)
        {
            CatHospitales hospital = new CatHospitales();
            hospital.IdHospital = model.IdHospital;
            hospital.NombreHospital = model.NombreHospital;
            hospital.IdMunicipio = model.IdMunicipio;
            hospital.Estatus = 1;
            hospital.FechaActualizacion = DateTime.Now;
            dbContext.CatHospitales.Add(hospital);
            dbContext.SaveChanges();
        }

        public void EditarHospital(CatHospitalesModel model)
        {
            CatHospitales hospital = new CatHospitales();
            hospital.IdHospital = model.IdHospital;
            hospital.NombreHospital = model.NombreHospital;
            hospital.IdMunicipio = model.IdMunicipio;
            hospital.Estatus = model.Estatus;
            hospital.FechaActualizacion = DateTime.Now;
            dbContext.Entry(hospital).State = EntityState.Modified;
            dbContext.SaveChanges();

        }

        public void EliminarHospital(CatHospitalesModel model)
        {
            CatHospitales hospital = new CatHospitales();
            hospital.IdHospital = model.IdHospital;
            hospital.NombreHospital = model.NombreHospital;
            hospital.IdMunicipio = model.IdMunicipio;
            hospital.Estatus = 0;
            hospital.FechaActualizacion = DateTime.Now;
            dbContext.Entry(hospital).State = EntityState.Modified;
            dbContext.SaveChanges();

        }




        public CatHospitalesModel GetHospitalByID(int IdHospital)
        {

            var productEnitity = dbContext.CatHospitales.Find(IdHospital);

            var factoresHospitalesModel = (from catHospitales in dbContext.CatHospitales.ToList()
                                           select new CatHospitalesModel

                                           {
                                               IdHospital = catHospitales.IdHospital,
                                               NombreHospital = catHospitales.NombreHospital,
                                               IdMunicipio = catHospitales.IdMunicipio,
                                               Estatus = catHospitales.Estatus,


                                           }).Where(w => w.IdHospital == IdHospital).FirstOrDefault();

            return factoresHospitalesModel;
        }


        public List<CatHospitalesModel> GetHospitales()
        {
            var ListHospitalesModel = (from catHospitales in dbContext.CatHospitales.ToList()
                                       join Municipios in dbContext.CatMunicipios.ToList()
                                       on catHospitales.IdMunicipio equals Municipios.IdMunicipio
                                       join estatus in dbContext.Estatus.ToList()
                                       on catHospitales.Estatus equals estatus.estatus
                                       select new CatHospitalesModel
                                       {
                                           IdHospital = catHospitales.IdHospital,
                                           NombreHospital = catHospitales.NombreHospital,
                                           IdMunicipio = catHospitales.IdMunicipio,
                                           Municipio = Municipios.Municipio,
                                           Estatus = catHospitales.Estatus,
                                           estatusDesc = estatus.estatusDesc,

                                       }).ToList();
            return ListHospitalesModel;
        }
        #endregion



    }
}
