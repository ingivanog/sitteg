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
    public class SubmarcasVehiculosController : Controller
    {
        DBContextInssoft dbContext = new DBContextInssoft();
        public IActionResult Index()
        {
            //var products = dbContext.Products.ToList();
            var ListSubmarcasModel = GetSubmarcas();

            return View(ListSubmarcasModel);

        }


        #region Modal Action
        public ActionResult IndexModal()
        {
            var ListSubmarcasModel = GetSubmarcas();
            //return View("IndexModal");
            return View("Index", ListSubmarcasModel);
        }

        [HttpPost]
        public ActionResult AgregarSubmarcaParcial()
        {
            SetDDLMarcas();
            return PartialView("_Crear");
        }

        public ActionResult EditarSubmarcaParcial(int IdSubmarca)
        {
            SetDDLMarcas();
            var submarcasModel = GetSubmarcaByID(IdSubmarca);
            return View("_Editar", submarcasModel);
        }

        public ActionResult EliminarSubmarcaParcial(int IdSubmarca)
        {
            SetDDLMarcas();
            var submarcasModel = GetSubmarcaByID(IdSubmarca);
            return View("_Eliminar", submarcasModel);
        }

        public JsonResult Categories_Read()
        {
            var result = new SelectList(dbContext.SubmarcasVehiculos.ToList(), "IdSubmarca", "NombreSubmarca");
            return Json(result);
        }



        [HttpPost]
        public ActionResult CreatePartialModal(SubmarcasVehiculosModel model)
        {
            var errors = ModelState.Values.Select(s => s.Errors);
            ModelState.Remove("NombreSubmarca");
            if (ModelState.IsValid)
            {
                //Crear el producto

                CreateSubmarca(model);
                var ListSubmarcasModel = GetSubmarcas();
                return PartialView("_ListaSubmarcas", ListSubmarcasModel);
            }
            SetDDLMarcas();
            //return View("Create");
            return PartialView("_Crear");
        }

        [HttpPost]
        public ActionResult EditarSubmarca(SubmarcasVehiculosModel model)
        {
            bool switchSubmarcas = Request.Form["submarcasSwitch"].Contains("true");
            model.Estatus = switchSubmarcas ? 1 : 0;
            var errors = ModelState.Values.Select(s => s.Errors);
            ModelState.Remove("NombreSubmarca");
            if (ModelState.IsValid)
            {
                //Crear el producto

                UpdateSubmarca(model);
                var ListSubmarcasModel = GetSubmarcas();
                return PartialView("_ListaSubmarcas", ListSubmarcasModel);
            }
            SetDDLMarcas();
            //return View("Create");
            return PartialView("_Editar");
        }

        [HttpPost]
        public ActionResult EliminarSubmarca(SubmarcasVehiculosModel model)
        {
            var errors = ModelState.Values.Select(s => s.Errors);
            ModelState.Remove("NombreSubmarca");
            if (ModelState.IsValid)
            {
                //Crear el producto

                DeleteSubmarca(model);
                var ListSubmarcasModel = GetSubmarcas();
                return PartialView("_ListaSubmarcas", ListSubmarcasModel);
            }
            SetDDLMarcas();
            //return View("Create");
            return PartialView("_Eliminar");
        }


        public JsonResult GetSubs([DataSourceRequest] DataSourceRequest request)
        {
            var ListSubmarcasModel = GetSubmarcas();

            return Json(ListSubmarcasModel.ToDataSourceResult(request));
        }




        #endregion


        #region Acciones a base de datos

        public void CreateSubmarca(SubmarcasVehiculosModel model)
        {
            SubmarcasVehiculo submarca = new SubmarcasVehiculo();
            submarca.IdSubmarca = model.IdSubmarca;
            submarca.NombreSubmarca = model.NombreSubmarca;
            submarca.IdMarcaVehiculo = model.IdMarcaVehiculo;
            submarca.estatus = 1;
            submarca.FechaActualizacion = DateTime.Now;
            dbContext.SubmarcasVehiculos.Add(submarca);
            dbContext.SaveChanges();
        }

        public void UpdateSubmarca(SubmarcasVehiculosModel model)
        {
            //Sera mas rapido con automapeo de clases
            SubmarcasVehiculo submarca = new SubmarcasVehiculo();
            submarca.IdSubmarca = model.IdSubmarca;
            submarca.NombreSubmarca = model.NombreSubmarca;
            submarca.IdMarcaVehiculo = model.IdMarcaVehiculo;
            submarca.estatus = model.Estatus;
            submarca.FechaActualizacion = DateTime.Now;
            dbContext.Entry(submarca).State = EntityState.Modified;
            dbContext.SaveChanges();

        }

        public void DeleteSubmarca(SubmarcasVehiculosModel model)
        {
            //Sera mas rapido con automapeo de clases
            SubmarcasVehiculo submarca = new SubmarcasVehiculo();
            submarca.IdSubmarca = model.IdSubmarca;
            submarca.NombreSubmarca = model.NombreSubmarca;
            submarca.IdMarcaVehiculo = model.IdMarcaVehiculo;
            submarca.estatus = 0;
            submarca.FechaActualizacion = DateTime.Now;
            dbContext.Entry(submarca).State = EntityState.Modified;
            dbContext.SaveChanges();

        }

        private void SetDDLMarcas()
        {
            ///Espacio en memoria de manera temporal que solo existe en la petición bool, list, string ,clases , selectlist
            ViewBag.Marcas = new SelectList(dbContext.MarcasVehiculos.ToList(), "IdMarcaVehiculo", "MarcaVehiculo");
        }

        [HttpPost]
        public IActionResult MarcasDDL(SubmarcasVehiculo submarcasModel)
        {
            GetSubmarcasWithMarcas();
            var catID = submarcasModel.IdMarcaVehiculo;
            return View();
        }


        public SubmarcasVehiculosModel GetSubmarcaByID(int IdSubmarca)
        {

            var productEnitity = dbContext.SubmarcasVehiculos.Find(IdSubmarca);

            var submarcaModel = (from submarcasVehiculos in dbContext.SubmarcasVehiculos.ToList()
                                 select new SubmarcasVehiculosModel

                                 {
                                     IdSubmarca = submarcasVehiculos.IdSubmarca,
                                     NombreSubmarca = submarcasVehiculos.NombreSubmarca,


                                 }).Where(w => w.IdSubmarca == IdSubmarca).FirstOrDefault();

            return submarcaModel;
        }

        /// <summary>
        /// Linq es una tecnologia de control de datos (excel, txt,EF,sqlclient etc)
        /// para la gestion un mejor control de la info
        /// </summary>
        /// <returns></returns>
        /// 
        public List<SubmarcasVehiculosModel> GetSubmarcasWithMarcas()
        {
            var ListSubmarcasModel = (from SubmarcasVehiculo in dbContext.SubmarcasVehiculos.ToList()
                                      join MarcasVehiculo in dbContext.MarcasVehiculos.ToList()
                                      on SubmarcasVehiculo.IdMarcaVehiculo equals MarcasVehiculo.IdMarcaVehiculo
                                      select new SubmarcasVehiculosModel
                                      {
                                          IdSubmarca = SubmarcasVehiculo.IdSubmarca,
                                          NombreSubmarca = SubmarcasVehiculo.NombreSubmarca,
                                          IdMarcaVehiculo = MarcasVehiculo.IdMarcaVehiculo,


                                      }).ToList();
            return ListSubmarcasModel;
        }
        public List<SubmarcasVehiculosModel> GetSubmarcas()
        {
            var ListSubmarcasModel = (from submarcasVehiculo in dbContext.SubmarcasVehiculos.ToList()
                                      join marcasVehiculos in dbContext.MarcasVehiculos.ToList()
                                      on submarcasVehiculo.IdMarcaVehiculo equals marcasVehiculos.IdMarcaVehiculo
                                      join estatus in dbContext.Estatus.ToList()
                                      on submarcasVehiculo.estatus equals estatus.estatus
                                      where submarcasVehiculo.estatus == 1
                                      select new SubmarcasVehiculosModel

                                      {
                                          IdSubmarca = submarcasVehiculo.IdSubmarca,
                                          NombreSubmarca = submarcasVehiculo.NombreSubmarca,
                                          Estatus = estatus.estatus,
                                          estatusDesc = estatus.estatusDesc,
                                          IdMarcaVehiculo = submarcasVehiculo.IdMarcaVehiculo,
                                          MarcaVehiculo = marcasVehiculos.MarcaVehiculo


                                      }).ToList();
            return ListSubmarcasModel;
        }
        #endregion



    }
}
