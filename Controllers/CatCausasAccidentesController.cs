﻿using GuanajuatoAdminUsuarios.Entity;
using GuanajuatoAdminUsuarios.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Example.WebUI.Controllers
{
    public class CatCausasAccidentesController : Controller
    {
        DBContextInssoft dbContext = new DBContextInssoft();
        public IActionResult Index()
        {
            var ListCausasAccidentesModel = GetCausasAccidentes();

            return View(ListCausasAccidentesModel);

        }




        #region Modal Action
        public ActionResult IndexModal()
        {
            var ListCausasAccidentesModel = GetCausasAccidentes();
            return View("Index", ListCausasAccidentesModel);
        }

        [HttpPost]
        public ActionResult AgregarCausasAccidenteModal()
        {
            //SetDDLDependencias();
            return PartialView("_Crear");
        }

        public ActionResult EditarCausasAccidenteModal(int IdCausaAccidente)
        {
            var causasAccidentesModel = GetCausaAccidenteByID(IdCausaAccidente);
            return PartialView("_Editar", causasAccidentesModel);
        }

        public ActionResult EliminarCausasAccidenteModal(int IdCausaAccidente)
        {
            var causasAccidentesModel = GetCausaAccidenteByID(IdCausaAccidente);
            return PartialView("_Eliminar", causasAccidentesModel);
        }



        [HttpPost]
        public ActionResult AgregarCausaAccidente(CatCausasAccidentesModel model)
        {
            var errors = ModelState.Values.Select(s => s.Errors);
            ModelState.Remove("CausaAccidente");
            if (ModelState.IsValid)
            {


                CrearCausaAccidente(model);
                var ListCausasAccidentesModel = GetCausasAccidentes();
                return PartialView("_ListaCausasAccidentes", ListCausasAccidentesModel);
            }

            return PartialView("_Crear");
        }

        public ActionResult EditarCausaAccidenteMod(CatCausasAccidentesModel model)
        {
            bool switchCausasAccidentes = Request.Form["causaAccidenteSwitch"].Contains("true");
            model.Estatus = switchCausasAccidentes ? 1 : 0;
            var errors = ModelState.Values.Select(s => s.Errors);
            ModelState.Remove("CausaAccidente");
            if (ModelState.IsValid)
            {


                EditarCausaAccidente(model);
                var ListCausasAccidentesModel = GetCausasAccidentes();
                return PartialView("_ListaCausasAccidentes", ListCausasAccidentesModel);
            }
            return PartialView("_Editar");
        }

        public ActionResult EliminarCausaAccidenteMod(CatCausasAccidentesModel model)
        {
            var errors = ModelState.Values.Select(s => s.Errors);
            ModelState.Remove("CausaAccidente");
            if (ModelState.IsValid)
            {


                EliminaCausaAccidente(model);
                var ListCausasAccidentesModel = GetCausasAccidentes();
                return PartialView("_ListaCausasAccidentes", ListCausasAccidentesModel);
            }
            return PartialView("_Eliminar");
        }
        public JsonResult GetCausas([DataSourceRequest] DataSourceRequest request)
        {
            var ListCausasAccidentesModel = GetCausasAccidentes();

            return Json(ListCausasAccidentesModel.ToDataSourceResult(request));
        }




        #endregion


        #region Acciones a base de datos

        public void CrearCausaAccidente(CatCausasAccidentesModel model)
        {
            CatCausasAccidentes causa = new CatCausasAccidentes();
            causa.IdCausaAccidente = model.IdCausaAccidente;
            causa.CausaAccidente = model.CausaAccidente;
            causa.Estatus = 1;
            causa.FechaActualizacion = DateTime.Now;
            dbContext.CatCausasAccidentes.Add(causa);
            dbContext.SaveChanges();
        }

        public void EditarCausaAccidente(CatCausasAccidentesModel model)
        {
            CatCausasAccidentes causa = new CatCausasAccidentes();
            causa.IdCausaAccidente = model.IdCausaAccidente;
            causa.CausaAccidente = model.CausaAccidente;
            causa.Estatus = model.Estatus;
            causa.FechaActualizacion = DateTime.Now;
            dbContext.Entry(causa).State = EntityState.Modified;
            dbContext.SaveChanges();

        }

        public void EliminaCausaAccidente(CatCausasAccidentesModel model)
        {

            CatCausasAccidentes causa = new CatCausasAccidentes();
            causa.IdCausaAccidente = model.IdCausaAccidente;
            causa.CausaAccidente = model.CausaAccidente;
            causa.Estatus = 0;
            causa.FechaActualizacion = DateTime.Now;
            dbContext.Entry(causa).State = EntityState.Modified;
            dbContext.SaveChanges();

        }




        public CatCausasAccidentesModel GetCausaAccidenteByID(int IdCausaAccidente)
        {

            var productEnitity = dbContext.CatCausasAccidentes.Find(IdCausaAccidente);

            var causasAccidentesModel = (from catCausasAccidentes in dbContext.CatCausasAccidentes.ToList()
                                         select new CatCausasAccidentesModel

                                         {
                                             IdCausaAccidente = catCausasAccidentes.IdCausaAccidente,
                                             CausaAccidente = catCausasAccidentes.CausaAccidente,


                                         }).Where(w => w.IdCausaAccidente == IdCausaAccidente).FirstOrDefault();

            return causasAccidentesModel;
        }


        public List<CatCausasAccidentesModel> GetCausasAccidentes()
        {
            var ListCausasAccidentesModel = (from catCausasAccidentes in dbContext.CatCausasAccidentes.ToList()
                                             join estatus in dbContext.Estatus.ToList()
                                             on catCausasAccidentes.Estatus equals estatus.estatus
                                             select new CatCausasAccidentesModel
                                             {
                                                 IdCausaAccidente = catCausasAccidentes.IdCausaAccidente,
                                                 CausaAccidente = catCausasAccidentes.CausaAccidente,
                                                 estatusDesc = estatus.estatusDesc,

                                             }).ToList();
            return ListCausasAccidentesModel;
        }
        #endregion



    }
}
