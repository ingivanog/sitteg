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
    public class CatDelegacionesOficinasTransporteController : Controller
    {
        DBContextInssoft dbContext = new DBContextInssoft();
        public IActionResult Index()
        {

            var ListDelegacionesOficinasTModel = GetDelegacionesOficinas();

            return View("Index", ListDelegacionesOficinasTModel);

        }




        #region Modal Action
        public ActionResult IndexModal()
        {
            var ListDelegacionesOficinasTModel = GetDelegacionesOficinas();
            return View("Index", ListDelegacionesOficinasTModel);
        }

        [HttpPost]
        public ActionResult AgregarDelegacionOficinaModal()
        {
            SetDDLMunicpios();
            return PartialView("_Crear");
        }

        public ActionResult EditarDelegacionOficinaModal(int IdOficinaTransporte)
        {
            SetDDLMunicpios();
            var delegacionOficinaModel = GetDelegacionOficinaByID(IdOficinaTransporte);
            return PartialView("_Editar", delegacionOficinaModel);
        }

        public ActionResult EliminarDelegacionOficinaModal(int IdOficinaTransporte)
        {
            SetDDLMunicpios();
            var delegacionOficinaModel = GetDelegacionOficinaByID(IdOficinaTransporte);
            return PartialView("_Eliminar", delegacionOficinaModel);
        }



        [HttpPost]
        public ActionResult AgregarDelegacionOficinaMod(CatDelegacionesOficinasTransporteModel model)
        {
            var errors = ModelState.Values.Select(s => s.Errors);
            ModelState.Remove("NombreOficina");
            if (ModelState.IsValid)
            {


                CrearDelegacionOficina(model);
                var ListDelegacionesOficinasTModel = GetDelegacionesOficinas();
                return PartialView("_ListDelegacionesOficinas", ListDelegacionesOficinasTModel);
            }

            return PartialView("_Crear");
        }

        public ActionResult EditarDelegacionOficinaMod(CatDelegacionesOficinasTransporteModel model)
        {
            bool switchDelegacionOficinas = Request.Form["delegacionOficinaSwitch"].Contains("true");
            model.Estatus = switchDelegacionOficinas ? 1 : 0;
            var errors = ModelState.Values.Select(s => s.Errors);
            ModelState.Remove("NombreOficina");
            if (ModelState.IsValid)
            {


                EditarDelegacionOficina(model);
                var ListDelegacionesOficinasTModel = GetDelegacionesOficinas();
                return PartialView("_ListDelegacionesOficinas", ListDelegacionesOficinasTModel);
            }
            return PartialView("_Editar");
        }

        public ActionResult EliminarDelegacionOficinaMod(CatDelegacionesOficinasTransporteModel model)
        {
            var errors = ModelState.Values.Select(s => s.Errors);
            ModelState.Remove("NombreOficina");
            if (ModelState.IsValid)
            {


                EliminaDelegacionOficina(model);
                var ListDelegacionesOficinasTModel = GetDelegacionesOficinas();
                return PartialView("_ListDelegacionesOficinas", ListDelegacionesOficinasTModel);
            }
            return PartialView("_Eliminar");
        }
        public JsonResult GetDelegacionOfs([DataSourceRequest] DataSourceRequest request)
        {
            var ListDelegacionesOficinasTModel = GetDelegacionesOficinas();

            return Json(ListDelegacionesOficinasTModel.ToDataSourceResult(request));
        }

        private void SetDDLMunicpios()
        {
            ViewBag.Municipios = new SelectList(dbContext.CatMunicipios.ToList(), "IdMunicipio", "Municipio");
        }



        #endregion


        #region Acciones a base de datos

        public void CrearDelegacionOficina(CatDelegacionesOficinasTransporteModel model)
        {
            CatDelegacionesOficinasTransporte delOficina = new CatDelegacionesOficinasTransporte();
            delOficina.IdOficinaTransporte = model.IdOficinaTransporte;
            delOficina.NombreOficina = model.NombreOficina;
            delOficina.JefeOficina = model.JefeOficina;
            delOficina.IdMunicipio = model.IdMunicipio;
            delOficina.Estatus = 1;
            delOficina.FechaActualizacion = DateTime.Now;
            dbContext.CatDelegacionesOficinasTransporte.Add(delOficina);
            dbContext.SaveChanges();
        }

        public void EditarDelegacionOficina(CatDelegacionesOficinasTransporteModel model)
        {
            CatDelegacionesOficinasTransporte delOficina = new CatDelegacionesOficinasTransporte();
            delOficina.IdOficinaTransporte = model.IdOficinaTransporte;
            delOficina.NombreOficina = model.NombreOficina;
            delOficina.JefeOficina = model.JefeOficina;
            delOficina.IdMunicipio = model.IdMunicipio;
            delOficina.Estatus = model.Estatus;
            delOficina.FechaActualizacion = DateTime.Now;
            dbContext.Entry(delOficina).State = EntityState.Modified;
            dbContext.SaveChanges();

        }

        public void EliminaDelegacionOficina(CatDelegacionesOficinasTransporteModel model)
        {
            CatDelegacionesOficinasTransporte delOficina = new CatDelegacionesOficinasTransporte();
            delOficina.IdOficinaTransporte = model.IdOficinaTransporte;
            delOficina.NombreOficina = model.NombreOficina;
            delOficina.JefeOficina = model.JefeOficina;
            delOficina.IdMunicipio = model.IdMunicipio;
            delOficina.Estatus = 0;
            delOficina.FechaActualizacion = DateTime.Now;
            dbContext.Entry(delOficina).State = EntityState.Modified;
            dbContext.SaveChanges();

        }




        public CatDelegacionesOficinasTransporteModel GetDelegacionOficinaByID(int IdOficinaTransporte)
        {

            var productEnitity = dbContext.CatFactoresOpcionesAccidentes.Find(IdOficinaTransporte);

            var delegacionesOficinasModel = (from catDelegacionesOficinasTransporte in dbContext.CatDelegacionesOficinasTransporte.ToList()
                                             select new CatDelegacionesOficinasTransporteModel

                                             {
                                                 IdOficinaTransporte = catDelegacionesOficinasTransporte.IdOficinaTransporte,
                                                 NombreOficina = catDelegacionesOficinasTransporte.NombreOficina,
                                                 JefeOficina = catDelegacionesOficinasTransporte.JefeOficina,
                                                 IdMunicipio = catDelegacionesOficinasTransporte.IdMunicipio,
                                                 Estatus = catDelegacionesOficinasTransporte.Estatus,


                                             }).Where(w => w.IdOficinaTransporte == IdOficinaTransporte).FirstOrDefault();

            return delegacionesOficinasModel;
        }


        public List<CatDelegacionesOficinasTransporteModel> GetDelegacionesOficinas()
        {
            var ListDelegacionesOficinasModel = (from catDelegacionesOficinasTransporte in dbContext.CatDelegacionesOficinasTransporte.ToList()
                                                 join catMunicipios in dbContext.CatMunicipios.ToList()
                                                 on catDelegacionesOficinasTransporte.IdMunicipio equals catMunicipios.IdMunicipio
                                                 join estatus in dbContext.Estatus.ToList()
                                                 on catDelegacionesOficinasTransporte.Estatus equals estatus.estatus
                                                 select new CatDelegacionesOficinasTransporteModel
                                                 {
                                                     IdOficinaTransporte = catDelegacionesOficinasTransporte.IdOficinaTransporte,
                                                     NombreOficina = catDelegacionesOficinasTransporte.NombreOficina,
                                                     JefeOficina = catDelegacionesOficinasTransporte.JefeOficina,
                                                     IdMunicipio = catDelegacionesOficinasTransporte.IdMunicipio,
                                                     Municipio = catMunicipios.Municipio,
                                                     Estatus = catDelegacionesOficinasTransporte.Estatus,
                                                     estatusDesc = estatus.estatusDesc,

                                                 }).ToList();
            return ListDelegacionesOficinasModel;
        }
        #endregion



    }
}
