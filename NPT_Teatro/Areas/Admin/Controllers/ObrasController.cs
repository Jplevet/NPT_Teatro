using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPT_Teatro.AccesoDatos.Data.Repository;
using NPT_Teatro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPT_Teatro.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ObrasController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public ObrasController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Obra obra)
        {
            if(ModelState.IsValid)
            {
                _contenedorTrabajo.Obra.Add(obra);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(obra);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Obra obra = new Obra();
            obra = _contenedorTrabajo.Obra.Get(id);

            if(obra == null)
            {
                return NotFound();
            }
            return View(obra);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Obra obra)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Obra.Update(obra);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(obra);
        }

        #region LLAMADAS A LA API
        [HttpGet]

        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Obra.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _contenedorTrabajo.Obra.Get(id);
            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Error al intentar borrar obra" });
            }

            _contenedorTrabajo.Obra.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Obra borrada" });
        }


        #endregion
    }
}
