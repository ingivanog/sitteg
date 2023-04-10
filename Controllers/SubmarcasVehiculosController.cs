﻿using GuanajuatoAdminUsuarios.Entity;
using GuanajuatoAdminUsuarios.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// Accion que redirige a la vista
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            //SetDDLSubmarcas();
            return View();
        }

        /// <summary>
        /// Accion que recupera los datos de la vista para insertar en BDD
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(SubmarcasVehiculosModel model)
        {
            var errors = ModelState.Values.Select(s => s.Errors);
            ModelState.Remove("NombreSubmarca");
            if (ModelState.IsValid)
            {
                //Crear el producto

                CreateSubmarca(model);
                return RedirectToAction("Index");
            }
           // SetDDLSubmarcas();
            return View("Create");
        }


        [HttpGet]
        public IActionResult Update(int IdSubmarca)
        {
            //aqui con productId debemos Consultar el producto para mostrar los datos actuales en la vista, para que sean modificados
           // SetDDLSubmarcas();
            var submarcasModel = GetSubmarcaByID(IdSubmarca);
            return View(submarcasModel);
        }


        [HttpPost]
        public IActionResult Update(SubmarcasVehiculosModel submarcasModel)
        {
            ModelState.Remove("NombreSubmarca");
            if (ModelState.IsValid)
            {
                //Modificiacion del registro
                UpdateSubmarca(submarcasModel);
                return RedirectToAction("Index");
            }
            //SetDDLSubmarcas();
            return View("Update");
        }

        [HttpGet]
        public IActionResult Delete(int IdSubmarca)
        {
            //aqui con productId debemos Consultar el producto para mostrar los datos actuales en la vista, para que sean modificados
           // SetDDLSubmarcas();
            var submarcasModel = GetSubmarcaByID(IdSubmarca);
            return View(submarcasModel);
        }


        [HttpPost]
        public IActionResult Delete(SubmarcasVehiculosModel submarcasModel)
        {
            ModelState.Remove("NombreSubmarca");
            if (ModelState.IsValid)
            {
                //Modificiacion del registro
                DeleteSubmarca(submarcasModel);
                return RedirectToAction("Index");
            }
           // SetDDLSubmarcas();
            return View("Delete");
        }



        ///Crear metodo de update (post)


        #region Modal Action
        public ActionResult IndexModal()
        {
            var ListSubmarcasModel = GetSubmarcas();
            //return View("IndexModal");
            return View("IndexModal", ListSubmarcasModel);
        }

        [HttpPost]
        public ActionResult AgregarParcial()
        {
            //SetDDLDependencias();
            return PartialView("_Create");
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
            return PartialView("_Create");
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
            submarca.estatus = 1;
            dbContext.SubmarcasVehiculos.Add(submarca);
            dbContext.SaveChanges();
        }

        public void UpdateSubmarca(SubmarcasVehiculosModel model)
        {
            //Sera mas rapido con automapeo de clases
            SubmarcasVehiculo submarca = new SubmarcasVehiculo();
            submarca.IdSubmarca = model.IdSubmarca;
            submarca.NombreSubmarca = model.NombreSubmarca;
            //Lineas de codigo para la modificacion del producto con EF
            dbContext.Entry(submarca).State = EntityState.Modified;
            dbContext.SaveChanges();

        }

        public void DeleteSubmarca(SubmarcasVehiculosModel model)
        {
            //Sera mas rapido con automapeo de clases
            SubmarcasVehiculo submarca = new SubmarcasVehiculo();
            submarca.IdSubmarca = model.IdSubmarca;
            submarca.NombreSubmarca = model.NombreSubmarca;

            dbContext.SubmarcasVehiculos.Remove(submarca);
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
            var ListSubmarcasModel = (from SubmarcasVehiculo  in dbContext.SubmarcasVehiculos.ToList()
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
                                      join Estatus in dbContext.Estatus.ToList()
                                      on submarcasVehiculo.estatus equals Estatus.estatus
                                      select new SubmarcasVehiculosModel

                                         {
                                             IdSubmarca = submarcasVehiculo.IdSubmarca,
                                             NombreSubmarca = submarcasVehiculo.NombreSubmarca,
                                             Estatus= Estatus.estatus,
                                             estatusDesc=Estatus.estatusDesc

                                         }).ToList();
            return ListSubmarcasModel;
        }
        #endregion



    }
}
