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
    public class CatOficinasRentaController : Controller
    {
        DBContextInssoft dbContext = new DBContextInssoft();






        #region Modal Action
        public ActionResult Index()
        {
            var ListOficinasRentaModel = GetOficinas();

            return View("Index", ListOficinasRentaModel);
        }

        public ActionResult Index2(int idDelegacion)
        {
            var ListOficinasRentaModel = ObtenerPorDel(idDelegacion);
            return View("Index2", ListOficinasRentaModel);
        }

        [HttpPost]
        public ActionResult AgregarOficinaRentaModal()
        {
            SetDDLDelegaciones();
            return PartialView("_Crear");
        }

        /// <summary>
        /// //////////////
        /// </summary>
        /// 

        /// 
        /// <returns></returns>

        public ActionResult EditarOficinaRentaModal(int IdOficinaRenta)
        {
            SetDDLDelegaciones();
            var oficinasRentaModel = GetOficinaRentaByID(IdOficinaRenta);
            return PartialView("_Editar", oficinasRentaModel);
        }

        public ActionResult EliminarOficinaRentaModal(int IdOficinaRenta)
        {
            SetDDLDelegaciones();
            var oficinasRentaModel = GetOficinaRentaByID(IdOficinaRenta);
            return PartialView("_Eliminar", oficinasRentaModel);
        }



        [HttpPost]
        public ActionResult AgregarOficinaRenta(CatOficinasRentaModel model)
        {
            var errors = ModelState.Values.Select(s => s.Errors);
            ModelState.Remove("NombreOficina");
            if (ModelState.IsValid)
            {


                CrearOficinaRenta(model);
                var ListOficinasRentaModel = GetOficinas();
                return PartialView("_ListaOficinasRenta", ListOficinasRentaModel);
            }
            SetDDLDelegaciones();
            return PartialView("_Crear");
        }

        public ActionResult EditarOficinaRentaMod(CatOficinasRentaModel model)
        {
            bool switchOficinasRenta = Request.Form["oficinasRentaSwitch"].Contains("true");
            model.Estatus = switchOficinasRenta ? 1 : 0;
            var errors = ModelState.Values.Select(s => s.Errors);
            ModelState.Remove("NombreOficina");
            if (ModelState.IsValid)
            {


                EditarOficinaRenta(model);
                var ListOficinasRentaModel = GetOficinas();
                return PartialView("_ListaOficinasRenta", ListOficinasRentaModel);
            }
            SetDDLDelegaciones();
            return PartialView("_Editar");
        }

        public ActionResult EliminarOficinasRentaMod(CatOficinasRentaModel model)
        {
            var errors = ModelState.Values.Select(s => s.Errors);
            ModelState.Remove("NombreOficina");
            if (ModelState.IsValid)
            {


                EliminarOficinaRenta(model);
                var ListOficinasRentaModel = GetOficinas();
                return PartialView("_ListaOficinasRenta", ListOficinasRentaModel);
            }
            SetDDLDelegaciones();
            return PartialView("_Eliminar");
        }

        public JsonResult GetOfiRent([DataSourceRequest] DataSourceRequest request)
        {
            var ListOficinasRentaModel = GetOficinas();

            return Json(ListOficinasRentaModel.ToDataSourceResult(request));
        }

        public JsonResult GetOfiPorDel([DataSourceRequest] DataSourceRequest request, int idDelegacion)
        {
            var ListOficinasRentaModel = ObtenerPorDel(idDelegacion);

            return Json(ListOficinasRentaModel.ToDataSourceResult(request));
        }




        #endregion


        #region Acciones a base de datos

        public void CrearOficinaRenta(CatOficinasRentaModel model)
        {
            CatOficinasRenta oficina = new CatOficinasRenta();
            oficina.IdOficinaRenta = model.IdOficinaRenta;
            oficina.NombreOficina = model.NombreOficina;
            oficina.IdDelegacion = model.IdDelegacion;
            oficina.Estatus = 1;
            oficina.FechaActualizacion = DateTime.Now;
            dbContext.CatOficinasRenta.Add(oficina);
            dbContext.SaveChanges();
        }

        public void EditarOficinaRenta(CatOficinasRentaModel model)
        {
            CatOficinasRenta oficina = new CatOficinasRenta();
            oficina.IdOficinaRenta = model.IdOficinaRenta;
            oficina.NombreOficina = model.NombreOficina;
            oficina.IdDelegacion = model.IdDelegacion;
            oficina.Estatus = model.Estatus;
            oficina.FechaActualizacion = DateTime.Now;
            dbContext.Entry(oficina).State = EntityState.Modified;
            dbContext.SaveChanges();

        }

        public void EliminarOficinaRenta(CatOficinasRentaModel model)
        {

            CatOficinasRenta oficina = new CatOficinasRenta();
            oficina.IdOficinaRenta = model.IdOficinaRenta;
            oficina.NombreOficina = model.NombreOficina;
            oficina.IdDelegacion = model.IdDelegacion;
            oficina.Estatus = 0;
            oficina.FechaActualizacion = DateTime.Now;
            dbContext.Entry(oficina).State = EntityState.Modified;
            dbContext.SaveChanges();


        }

        private void SetDDLDelegaciones()
        {
            ///Espacio en memoria de manera temporal que solo existe en la petición bool, list, string ,clases , selectlist
            ViewBag.Delegaciones = new SelectList(dbContext.Delegaciones.ToList(), "IdDelegacion", "Delegacion");
        }


        public CatOficinasRentaModel GetOficinaRentaByID(int IdOficinaRenta)
        {

            var productEnitity = dbContext.CatOficinasRenta.Find(IdOficinaRenta);

            var oficinasRentaModel = (from catOficinasRenta in dbContext.CatOficinasRenta.ToList()
                                      select new CatOficinasRentaModel

                                      {
                                          IdOficinaRenta = catOficinasRenta.IdOficinaRenta,
                                          NombreOficina = catOficinasRenta.NombreOficina,


                                      }).Where(w => w.IdOficinaRenta == IdOficinaRenta).FirstOrDefault();

            return oficinasRentaModel;
        }

        /// <summary>
        /// Linq es una tecnologia de control de datos (excel, txt,EF,sqlclient etc)
        /// para la gestion un mejor control de la info
        /// </summary>
        /// <returns></returns>
        public List<CatOficinasRentaModel> GetOficinas()
        {
            var ListOficinasRentaModel = (from catOficinasRenta in dbContext.CatOficinasRenta.ToList()
                                          join delegaciones in dbContext.Delegaciones.ToList()
                                          on catOficinasRenta.IdDelegacion equals delegaciones.IdDelegacion
                                          join estatus in dbContext.Estatus.ToList()
                                          on catOficinasRenta.Estatus equals estatus.estatus
                                          select new CatOficinasRentaModel
                                          {
                                              IdOficinaRenta = catOficinasRenta.IdOficinaRenta,
                                              NombreOficina = catOficinasRenta.NombreOficina,
                                              DelegacionDesc = delegaciones.Delegacion,
                                              IdDelegacion = delegaciones.IdDelegacion,
                                              estatusDesc = estatus.estatusDesc,


                                          }).ToList();
            return ListOficinasRentaModel;
        }

        public List<CatOficinasRentaModel> ObtenerPorDel(int IdDelegacion)
        {
            var ListaPorDelegacion = (from catOficinasRenta in dbContext.CatOficinasRenta.ToList()
                                      join delegaciones in dbContext.Delegaciones.ToList()
                                      on catOficinasRenta.IdDelegacion equals delegaciones.IdDelegacion
                                      select new CatOficinasRentaModel
                                      {
                                          IdOficinaRenta = catOficinasRenta.IdOficinaRenta,
                                          NombreOficina = catOficinasRenta.NombreOficina,
                                          DelegacionDesc = delegaciones.Delegacion,
                                          IdDelegacion = delegaciones.IdDelegacion,


                                      }).Where(w => w.IdDelegacion == IdDelegacion).ToList();
            return ListaPorDelegacion;
        }

    }
}
#endregion