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
    public class SalariosMinimosController : Controller
    {
        DBContextInssoft dbContext = new DBContextInssoft();
        public IActionResult Index()
        {
            //var products = dbContext.Products.ToList();
            var ListSalariosModel = GetSalarios();

            return View(ListSalariosModel);

        }




        #region Modal Action
        public ActionResult IndexModal()
        {
            var ListSalariosModel = GetSalarios();
            //return View("IndexModal");
            return View("Index", ListSalariosModel);
        }

        [HttpPost]
        public ActionResult AgregarSalarioPacial()
        {
            //SetDDLDependencias();
            return PartialView("_Crear");
        }

        [HttpPost]
        public ActionResult EditarSalarioParcial(int IdSalario)
        {
            var salariosModel = GetSalarioByID(IdSalario);
            return View("_Editar", salariosModel);
        }

        [HttpPost]
        public ActionResult EliminarSalarioParcial(int IdSalario)
        {
            var salariosModel = GetSalarioByID(IdSalario);
            return View("_Eliminar", salariosModel);
        }

        public JsonResult Categories_Read()
        {
            var result = new SelectList(dbContext.SalariosMinimos.ToList(), "IdSalario", "Salario");
            return Json(result);
        }



        [HttpPost]
        public ActionResult CreatePartialModal(SalariosMinimosModel model)
        {
            var errors = ModelState.Values.Select(s => s.Errors);
            ModelState.Remove("Salario");
            if (ModelState.IsValid)
            {


                CreateSalario(model);
                var ListSalariosModel = GetSalarios();
                return PartialView("_ListaSalariosMinimos", ListSalariosModel);
            }
            //SetDDLCategories();
            //return View("Create");
            return PartialView("_Crear");
        }

        [HttpPost]
        public ActionResult UpdatePartialSalarioModal(SalariosMinimosModel model)
        {
            bool switchSalarios = Request.Form["salariosSwitch"].Contains("true");
            model.Estatus = switchSalarios ? 1 : 0;
            var errors = ModelState.Values.Select(s => s.Errors);
            ModelState.Remove("Salario");
            if (ModelState.IsValid)
            {
               
                UpdateSalario(model);
                var ListSalariosModel = GetSalarios();
                return PartialView("_ListaSalariosMinimos", ListSalariosModel);
            }
            //SetDDLCategories();
            //return View("Create");
            return PartialView("_Editar");
        }

        [HttpPost]
        public ActionResult EliminarSalarioModal(SalariosMinimosModel model)
        {
            var errors = ModelState.Values.Select(s => s.Errors);
            ModelState.Remove("IdSalario");
            if (ModelState.IsValid)
            {


                DeleteSalario(model);
                var ListSalariosModel = GetSalarios();
                return PartialView("_ListaSalariosMinimos", ListSalariosModel);
            }
            //SetDDLCategories();
            //return View("Create");
            return PartialView("_Eliminar");
        }

        public JsonResult GetMins([DataSourceRequest] DataSourceRequest request)
        {
            var ListSalariosModel = GetSalarios();

            return Json(ListSalariosModel.ToDataSourceResult(request));
        }




        #endregion


        #region Acciones a base de datos

        public void CreateSalario(SalariosMinimosModel model)
        {
            SalariosMinimos salario = new SalariosMinimos();
            salario.IdSalario = model.IdSalario;
            salario.Area = model.Area;
            salario.Salario = model.Salario;
            salario.Fecha = model.Fecha;
            salario.Estatus = 1;
            salario.FechaActualizacion = DateTime.Now;
            dbContext.SalariosMinimos.Add(salario);
            dbContext.SaveChanges();
        }

        public void UpdateSalario(SalariosMinimosModel model)
        {
            SalariosMinimos salario = new SalariosMinimos();
            salario.IdSalario = model.IdSalario;
            salario.Area = model.Area;
            salario.Salario = model.Salario;
            salario.Fecha = model.Fecha;
            salario.Estatus = model.Estatus;
            salario.FechaActualizacion = DateTime.Now;
            dbContext.Entry(salario).State = EntityState.Modified;
            dbContext.SaveChanges();

        }

        public void DeleteSalario(SalariosMinimosModel model)
        {
            SalariosMinimos salario = new SalariosMinimos();
            salario.IdSalario = model.IdSalario;
            salario.Area = model.Area;
            salario.Salario = model.Salario;
            salario.FechaActualizacion = DateTime.Now;
            salario.Estatus = 0;
            salario.FechaActualizacion = DateTime.Now;
            dbContext.Entry(salario).State = EntityState.Modified;
            dbContext.SaveChanges();

        }

        private void SetDDLSalarios()
        {
            ///Espacio en memoria de manera temporal que solo existe en la petición bool, list, string ,clases , selectlist
            ViewBag.Salarios = new SelectList(dbContext.SalariosMinimos.ToList(), "IdSalario", "Salario");
        }


        public SalariosMinimosModel GetSalarioByID(int IdSalario)
        {

            var productEnitity = dbContext.SalariosMinimos.Find(IdSalario);

            var SalarioModel = (from salariosMinimos in dbContext.SalariosMinimos.ToList()
                                select new SalariosMinimosModel

                                {
                                    IdSalario = salariosMinimos.IdSalario,
                                    Area = salariosMinimos.Area,
                                    Salario = salariosMinimos.Salario,
                                    Fecha = salariosMinimos.Fecha,
                                    Estatus = salariosMinimos.Estatus,



                                }).Where(w => w.IdSalario == IdSalario).FirstOrDefault();

            return SalarioModel;
        }

        /// <summary>
        /// Linq es una tecnologia de control de datos (excel, txt,EF,sqlclient etc)
        /// para la gestion un mejor control de la info
        /// </summary>
        /// <returns></returns>
        public List<SalariosMinimosModel> GetSalarios()
        {
            var ListSalariosModel = (from salariosMinimos in dbContext.SalariosMinimos.ToList()
                                     join estatus in dbContext.Estatus.ToList()
                                    on salariosMinimos.Estatus equals estatus.estatus
                                     where salariosMinimos.Estatus == 1

                                     select new SalariosMinimosModel
                                     {
                                         IdSalario = salariosMinimos.IdSalario,
                                         Area = salariosMinimos.Area,
                                         Salario = salariosMinimos.Salario,
                                         Fecha = salariosMinimos.Fecha,
                                         Estatus = salariosMinimos.Estatus,
                                         estatusDesc = estatus.estatusDesc,

                                     }).ToList();
            return ListSalariosModel;
        }
        #endregion



    }
}
