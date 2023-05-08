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
    public class CatFactoresOpcionesAccidentesController : Controller
    {
        DBContextInssoft dbContext = new DBContextInssoft();
        public IActionResult Index()
        {

            var ListFactoresOpcionesAccidentesModel = GetFactoresOpcionesAccidentes();

            return View(ListFactoresOpcionesAccidentesModel);

        }




        #region Modal Action
        public ActionResult IndexModal()
        {
            var ListFactoresOpcionesAccidentesModel = GetFactoresOpcionesAccidentes();
            return View("Index", ListFactoresOpcionesAccidentesModel);
        }

        [HttpPost]
        public ActionResult AgregarFactoresOpcionesAccidenteModal()
        {
            SetDDLFactores();
            return PartialView("_Crear");
        }

        public ActionResult EditarFactoresOpcionesAccidenteModal(int IdFactoropcionAccidente)
        {
            SetDDLFactores();
            var factoresOpcionesAccidentesModel = GetFactorOpcionAccidenteByID(IdFactoropcionAccidente);
            return PartialView("_Editar", factoresOpcionesAccidentesModel);
        }

        public ActionResult EliminarFactoresOpcionesAccidenteModal(int IdFactoropcionAccidente)
        {
            SetDDLFactores();
            var factoresOpcionesAccidentesModel = GetFactorOpcionAccidenteByID(IdFactoropcionAccidente);
            return PartialView("_Eliminar", factoresOpcionesAccidentesModel);
        }



        [HttpPost]
        public ActionResult AgregarFactorOpcionAccidente(CatFactoresOpcionesAccidentesModel model)
        {
            var errors = ModelState.Values.Select(s => s.Errors);
            ModelState.Remove("FactorOpcionAccidente");
            if (ModelState.IsValid)
            {


                CrearFactorOpcionAccidente(model);
                var ListFactoresOpcionesAccidentesModel = GetFactoresOpcionesAccidentes();
                return PartialView("_ListaFactoresOpcionesAccidentes", ListFactoresOpcionesAccidentesModel);
            }

            return PartialView("_Crear");
        }

        public ActionResult EditarFactorOpcionAccidenteMod(CatFactoresOpcionesAccidentesModel model)
        {
            bool switchFactoresOpciones = Request.Form["factoresOpcionesSwitch"].Contains("true");
            model.Estatus = switchFactoresOpciones ? 1 : 0;
            var errors = ModelState.Values.Select(s => s.Errors);
            ModelState.Remove("FactorOpcionAccidente");
            if (ModelState.IsValid)
            {


                EditarFactorOpcionAccidente(model);
                var ListFactoresOpcionesAccidentesModel = GetFactoresOpcionesAccidentes();
                return PartialView("_ListaFactoresOpcionesAccidentes", ListFactoresOpcionesAccidentesModel);
            }
            return PartialView("_Editar");
        }

        public ActionResult EliminarFactorOpcionAccidenteMod(CatFactoresOpcionesAccidentesModel model)
        {
            var errors = ModelState.Values.Select(s => s.Errors);
            ModelState.Remove("FactorOpcionAccidente");
            if (ModelState.IsValid)
            {


                EliminaFactorOpcionAccidente(model);
                var ListFactoresAccidentesModel = GetFactoresOpcionesAccidentes();
                return PartialView("_ListaFactoresOpcionesAccidentes", ListFactoresAccidentesModel);
            }
            return PartialView("_Eliminar");
        }
        public JsonResult GetFactoresOpciones([DataSourceRequest] DataSourceRequest request)
        {
            var ListFactoresOpcionesAccidentesModel = GetFactoresOpcionesAccidentes();

            return Json(ListFactoresOpcionesAccidentesModel.ToDataSourceResult(request));
        }

        private void SetDDLFactores()
        {
            ///Espacio en memoria de manera temporal que solo existe en la petición bool, list, string ,clases , selectlist
            ViewBag.Factores = new SelectList(dbContext.CatFactoresAccidentes.ToList(), "IdFactorAccidente", "FactorAccidente");
        }



        #endregion


        #region Acciones a base de datos

        public void CrearFactorOpcionAccidente(CatFactoresOpcionesAccidentesModel model)
        {
            CatFactoresOpcionesAccidentes factorOpcion = new CatFactoresOpcionesAccidentes();
            factorOpcion.IdFactorOpcionAccidente = model.IdFactorOpcionAccidente;
            factorOpcion.FactorOpcionAccidente = model.FactorOpcionAccidente;
            factorOpcion.IdFactorAccidente = model.IdFactorAccidente;
            factorOpcion.Estatus = 1;
            factorOpcion.FechaActualizacion = DateTime.Now;
            dbContext.CatFactoresOpcionesAccidentes.Add(factorOpcion);
            dbContext.SaveChanges();
        }

        public void EditarFactorOpcionAccidente(CatFactoresOpcionesAccidentesModel model)
        {
            CatFactoresOpcionesAccidentes factorOpcion = new CatFactoresOpcionesAccidentes();
            factorOpcion.IdFactorOpcionAccidente = model.IdFactorOpcionAccidente;
            factorOpcion.FactorOpcionAccidente = model.FactorOpcionAccidente;
            factorOpcion.IdFactorAccidente = model.IdFactorAccidente;
            factorOpcion.Estatus = model.Estatus;
            factorOpcion.FechaActualizacion = DateTime.Now;
            dbContext.Entry(factorOpcion).State = EntityState.Modified;
            dbContext.SaveChanges();

        }

        public void EliminaFactorOpcionAccidente(CatFactoresOpcionesAccidentesModel model)
        {

            CatFactoresOpcionesAccidentes factorOpcion = new CatFactoresOpcionesAccidentes();
            factorOpcion.IdFactorOpcionAccidente = model.IdFactorOpcionAccidente;
            factorOpcion.FactorOpcionAccidente = model.FactorOpcionAccidente;
            factorOpcion.IdFactorAccidente = model.IdFactorAccidente;
            factorOpcion.Estatus = 0;
            factorOpcion.FechaActualizacion = DateTime.Now;
            dbContext.Entry(factorOpcion).State = EntityState.Modified;
            dbContext.SaveChanges();

        }




        public CatFactoresOpcionesAccidentesModel GetFactorOpcionAccidenteByID(int IdFactorOpcionAccidente)
        {

            var productEnitity = dbContext.CatFactoresOpcionesAccidentes.Find(IdFactorOpcionAccidente);

            var factoresOpcionesAccidentesModel = (from catFactoresOpcionesAccidentes in dbContext.CatFactoresOpcionesAccidentes.ToList()
                                                   select new CatFactoresOpcionesAccidentesModel

                                                   {
                                                       IdFactorOpcionAccidente = catFactoresOpcionesAccidentes.IdFactorOpcionAccidente,
                                                       FactorOpcionAccidente = catFactoresOpcionesAccidentes.FactorOpcionAccidente,
                                                       IdFactorAccidente = catFactoresOpcionesAccidentes.IdFactorAccidente,
                                                       Estatus = catFactoresOpcionesAccidentes.Estatus,


                                                   }).Where(w => w.IdFactorOpcionAccidente == IdFactorOpcionAccidente).FirstOrDefault();

            return factoresOpcionesAccidentesModel;
        }


        public List<CatFactoresOpcionesAccidentesModel> GetFactoresOpcionesAccidentes()
        {
            var ListFactoresOpcionesAccidentesModel = (from catFactoresOpcionesAccidentes in dbContext.CatFactoresOpcionesAccidentes.ToList()
                                                       join catFactoresAccidentes in dbContext.CatFactoresAccidentes.ToList()
                                                       on catFactoresOpcionesAccidentes.IdFactorAccidente equals catFactoresAccidentes.IdFactorAccidente
                                                       join estatus in dbContext.Estatus.ToList()
                                                       on catFactoresAccidentes.Estatus equals estatus.estatus
                                                       select new CatFactoresOpcionesAccidentesModel
                                                       {
                                                           IdFactorOpcionAccidente = catFactoresOpcionesAccidentes.IdFactorOpcionAccidente,
                                                           FactorOpcionAccidente = catFactoresOpcionesAccidentes.FactorOpcionAccidente,
                                                           IdFactorAccidente = catFactoresOpcionesAccidentes.IdFactorAccidente,
                                                           FactorAccidente = catFactoresAccidentes.FactorAccidente,
                                                           Estatus = catFactoresOpcionesAccidentes.Estatus,
                                                           estatusDesc = estatus.estatusDesc,

                                                       }).ToList();
            return ListFactoresOpcionesAccidentesModel;
        }
        #endregion



    }
}
