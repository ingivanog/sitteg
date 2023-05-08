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
    public class DiasInhabilesController : Controller
    {
        DBContextInssoft dbContext = new DBContextInssoft();
        public IActionResult Index()
        {
            //var products = dbContext.Products.ToList();
            var ListDiasInhabilesModel = GetDiasInhabiles();

            return View(ListDiasInhabilesModel);

        }




        public JsonResult MunicipiosDDL()
        {
            //Data source
            var dataSource = GetDiasInhabiles().AsEnumerable();
            return Json(dataSource);
        }

        ///Crear metodo de update (post)


        #region Modal Action
        public ActionResult IndexModal()
        {
            var ListDiasInhabilesModel = GetDiasInhabiles();
            //return View("IndexModal");
            return View("Index", ListDiasInhabilesModel);
        }

        [HttpPost]
        public ActionResult AgregarParcialDiaInhabil()
        {
            SetDDLMunicipios();
            return PartialView("_Crear");
        }

        public ActionResult EditarParcial(int IdDiaInhabil)
        {
            var diasInhabilesModel = GetDiaInhabilByID(IdDiaInhabil);
            SetDDLMunicipios();
            return View("_Editar", diasInhabilesModel);
        }

        public ActionResult EliminarDiaInhabilParcial(int IdDiaInhabil)
        {
            var diasInhabilesModel = GetDiaInhabilByID(IdDiaInhabil);
            SetDDLMunicipios();
            return View("_Eliminar", diasInhabilesModel);
        }

        public JsonResult Municipios_Read()
        {
            var dataSource = new SelectList(dbContext.CatMunicipios.ToList(), "IdMunicipio", "Municipio");
            return Json(dataSource);
        }



        [HttpPost]
        public ActionResult CreatePartialModal(DiasInhabilesModel model)
        {
            var errors = ModelState.Values.Select(s => s.Errors);
            ModelState.Remove("fecha");
            if (ModelState.IsValid)
            {


                CreateDiaInhabil(model);
                var ListDiasInhabilesModel = GetDiasInhabiles();
                return PartialView("_ListaDiasInhabiles", ListDiasInhabilesModel);
            }
            //return View("Create");
            return PartialView("_Crear");
        }

        [HttpPost]
        public ActionResult EditarParcialModal(DiasInhabilesModel model)
        {
            bool switchDiasinhabiles = Request.Form["diasInhabilesSwitch"].Contains("true");
            model.Estatus = switchDiasinhabiles ? 1 : 0;
            var errors = ModelState.Values.Select(s => s.Errors);
            ModelState.Remove("fecha");
            if (ModelState.IsValid)
            {
                UpdateDiaInhabil(model);
                var ListDiasInhabilesModel = GetDiasInhabiles();
                return PartialView("_ListaDiasInhabiles", ListDiasInhabilesModel);
            }
            SetDDLMunicipios();
            return PartialView("_Editar");
        }

        [HttpPost]
        public ActionResult EliminarDiaParcialModal(DiasInhabilesModel model)
        {
            var errors = ModelState.Values.Select(s => s.Errors);
            ModelState.Remove("fecha");
            if (ModelState.IsValid)
            {


                DeleteDiaInhabil(model);
                var ListDiasInhabilesModel = GetDiasInhabiles();
                return PartialView("_ListaDiasInhabiles", ListDiasInhabilesModel);
            }
            SetDDLMunicipios();
            //return View("Create");
            return PartialView("_Eliminar");
        }
        public JsonResult GetDiasIn([DataSourceRequest] DataSourceRequest request)
        {
            var ListDiasInhabilesModel = GetDiasInhabiles();

            return Json(ListDiasInhabilesModel.ToDataSourceResult(request));
        }




        #endregion


        #region Acciones a base de datos

        public void CreateDiaInhabil(DiasInhabilesModel model)
        {
            DiasInhabiles diaInhabil = new DiasInhabiles();
            diaInhabil.idDiaInhabil = model.idDiaInhabil;
            diaInhabil.fecha = model.fecha;
            diaInhabil.idMunicipio = model.idMunicipio;
            diaInhabil.todosMunicipiosBool = model.todosMunicipiosBool;
            diaInhabil.todosMunicipiosDesc = model.todosMunicipiosDesc;
            diaInhabil.Estatus = 1;
            diaInhabil.FechaActualizacion = DateTime.Now;
            dbContext.DiasInhabiles.Add(diaInhabil);
            dbContext.SaveChanges();
        }

        public void UpdateDiaInhabil(DiasInhabilesModel model)
        {
            DiasInhabiles diaInhabil = new DiasInhabiles();
            diaInhabil.idDiaInhabil = model.idDiaInhabil;
            diaInhabil.fecha = model.fecha;
            diaInhabil.idMunicipio = model.idMunicipio;
            diaInhabil.todosMunicipiosDesc = model.todosMunicipiosDesc;
            diaInhabil.Estatus = model.Estatus;
            diaInhabil.FechaActualizacion = DateTime.Now;

            dbContext.Entry(diaInhabil).State = EntityState.Modified;
            dbContext.SaveChanges();

        }

        public void DeleteDiaInhabil(DiasInhabilesModel model)
        {
            DiasInhabiles diaInhabil = new DiasInhabiles();
            diaInhabil.idDiaInhabil = model.idDiaInhabil;
            diaInhabil.fecha = model.fecha;
            diaInhabil.idMunicipio = model.idMunicipio;
            diaInhabil.todosMunicipiosDesc = model.todosMunicipiosDesc;
            diaInhabil.Estatus = 0;
            diaInhabil.FechaActualizacion = DateTime.Now;
            dbContext.Entry(diaInhabil).State = EntityState.Modified;
            dbContext.SaveChanges();

        }

        private void SetDDLMunicipios()
        {
            ///Espacio en memoria de manera temporal que solo existe en la petición bool, list, string ,clases , selectlist
            ViewBag.Municipios = new SelectList(dbContext.CatMunicipios.ToList(), "IdMunicipio", "Municipio");
        }


        public DiasInhabilesModel GetDiaInhabilByID(int IdDiaInhabil)
        {

            var productEnitity = dbContext.DiasInhabiles.Find(IdDiaInhabil);

            var diaInhabilModel = (from diasInhabiles in dbContext.DiasInhabiles.ToList()
                                   select new DiasInhabilesModel

                                   {
                                       idDiaInhabil = diasInhabiles.idDiaInhabil,
                                       fecha = diasInhabiles.fecha,
                                       idMunicipio = diasInhabiles.idMunicipio,
                                       todosMunicipiosBool = diasInhabiles.todosMunicipiosBool,
                                       todosMunicipiosDesc = diasInhabiles.todosMunicipiosDesc,



                                   }).Where(w => w.idDiaInhabil == IdDiaInhabil).FirstOrDefault();

            return diaInhabilModel;
        }

        public List<DiasInhabilesModel> GetDiasInhabiles()
        {
            var ListDiasInhabilesModel = (from diasInhabiles in dbContext.DiasInhabiles.ToList()
                                          join municipio in dbContext.CatMunicipios.ToList()
                                          on diasInhabiles.idMunicipio equals municipio.IdMunicipio
                                          join estatus in dbContext.Estatus.ToList()
                                          on diasInhabiles.Estatus equals estatus.estatus



                                          select new DiasInhabilesModel
                                          {
                                              idDiaInhabil = diasInhabiles.idDiaInhabil,
                                              fecha = diasInhabiles.fecha,
                                              idMunicipio = diasInhabiles.idMunicipio,
                                              todosMunicipiosDesc = diasInhabiles.todosMunicipiosDesc,
                                              Estatus = diasInhabiles.Estatus,
                                              EstatusDesc = estatus.estatusDesc,
                                              Municipio = municipio.Municipio

                                          }).ToList();
            return ListDiasInhabilesModel;
        }
        #endregion



    }
}
