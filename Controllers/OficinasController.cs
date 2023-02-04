﻿using GuanajuatoAdminUsuarios.Data;
using GuanajuatoAdminUsuarios.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuanajuatoAdminUsuarios.Controllers
{
    [Authorize]
    public class OficinasController : Controller
    {
        Oficinas catalogoOficina = new Oficinas();
        Entidades catalogoEntidad = new Entidades();

        // GET: CatOficinasController
        [HttpGet("oficinas")]
        public IActionResult Inicio()
        {
             return View("../Catalogos/Oficinas/Inicio");
        }


        public ActionResult GetOficinas([DataSourceRequest] DataSourceRequest request)
        {
            List<Oficina> listOficinas = new List<Oficina>();

            listOficinas = catalogoOficina.GetOficinas();


            DataSourceResult gridOficinas = listOficinas.ToDataSourceResult(request, cm => new
            {
                Id = cm.Id,
                Descripcion = cm.Descripcion,
                Estatus = cm.Estatus
            });
            return Json(gridOficinas);
        }

        // GET: CatOficinasController/Create
        [HttpGet("oficinas/crear")]
        public IActionResult Crear()
        {
            return View("../Catalogos/Oficinas/Crear");
        }

        // POST: CatOficinasController/Create
        [HttpPost("oficinas/crear")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("Descripcion,IdEntidad")] Oficina oficina)
        {
            if (ModelState.IsValid)
            {
                var idUsuario = User.FindFirst("IdUsuario").Value;
                catalogoOficina.GuardaOficina(oficina.Descripcion, oficina.IdEntidad, Int32.Parse(idUsuario));
                return RedirectToAction(nameof(Index));
            }
            return View("../Catalogos/Oficinas/Crear", oficina);
        }

        [HttpGet("oficinas/editar/{id}")]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var catOficina = catalogoOficina.GetOficinaById(id);

            return View("../Catalogos/Oficinas/Editar", catOficina);
        }

        [HttpPost("oficinas/editar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarOficina([Bind("Id,Descripcion,IdEntidad,Estatus")] Oficina oficina)
        {
             if (ModelState.IsValid)
            {
                var idUsuario = User.FindFirst("IdUsuario").Value;
                catalogoOficina.ActualizaOficina(oficina.Id, oficina.Descripcion, oficina.IdEntidad, oficina.Estatus, idUsuario);
             return RedirectToAction(nameof(Inicio));
            }

             return View("../Catalogos/Oficinas/Editar", oficina);
        }

        public JsonResult GetEntidadesAjax()
        {
            var entidades = catalogoEntidad.GetEntidades();

            return Json(entidades, new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}