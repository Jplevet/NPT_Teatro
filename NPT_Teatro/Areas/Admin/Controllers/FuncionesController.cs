using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NPT_Teatro.AccesoDatos.Data.Repository;
using NPT_Teatro.Models.ViewModels;
using NPT_Teatro.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NPT_Teatro.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class FuncionesController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnviroment;

        public FuncionesController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnviroment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnviroment = hostingEnviroment;
        }

        [HttpGet]
        public IActionResult Index()
        {
           
            return View();

        }

        [HttpGet]
        public IActionResult Create()
        {
            FuncionVM funvm = new FuncionVM()
            {
                Funcion = new Models.Funcion(),
                ListaObras = _contenedorTrabajo.Obra.GetListaObras()
            };

            return View(funvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FuncionVM funVM)
        {
            /*if(ModelState.IsValid)
            {*/
                string rutaPrincipal = _hostingEnviroment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                if(funVM.Funcion.Id == 0)
                {
                    //Nuevo articulo
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\funciones");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    funVM.Funcion.UrlImagen = @"\imagenes\funciones\" + nombreArchivo + extension;

                    _contenedorTrabajo.Funcion.Add(funVM.Funcion);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
            /*}*/

            funVM.ListaObras = _contenedorTrabajo.Obra.GetListaObras();
            return View(funVM);

        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            FuncionVM funvm = new FuncionVM()
            {
                Funcion = new Models.Funcion(),
                ListaObras = _contenedorTrabajo.Obra.GetListaObras()
            };

            if (id != null)
            {
                funvm.Funcion = _contenedorTrabajo.Funcion.Get(id.GetValueOrDefault());
            }

            return View(funvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FuncionVM funVM)
        {
           /* if(ModelState.IsValid)
            {*/
                string rutaPrincipal = _hostingEnviroment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                //Obtener archivo por su ID
                var funcionDesdeDb = _contenedorTrabajo.Funcion.Get(funVM.Funcion.Id);

                if (archivos.Count() > 0)
                {
                    //Editar imagen
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\funciones");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var nuevaExtension = Path.GetExtension(archivos[0].FileName);
                    ///Obteniendo ruta del archivo nuevo y le sacamos los '\\'
                    var rutaImagen = Path.Combine(rutaPrincipal, funcionDesdeDb.UrlImagen.TrimStart('\\'));

                    if(System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }


                    //Subiendo el archivo otra vez
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + nuevaExtension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    funVM.Funcion.UrlImagen = @"\imagenes\funciones\" + nombreArchivo + nuevaExtension;

                    _contenedorTrabajo.Funcion.Update(funVM.Funcion);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //En caso de que la imagen ya exista y no se reemplace. Se conserva la que ya esta e la base
                    funVM.Funcion.UrlImagen = funcionDesdeDb.UrlImagen;
                }

                _contenedorTrabajo.Funcion.Update(funVM.Funcion);
                _contenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));
            //}

            /*funVM.ListaObras = _contenedorTrabajo.Obra.GetListaObras();
            return View(funVM);*/
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var funcionDesdeDb = _contenedorTrabajo.Funcion.Get(id);
            //Obtener la ruta de la imagen en el proyecto
            string rutaDirectorioPrincipal = _hostingEnviroment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, funcionDesdeDb.UrlImagen.TrimStart('\\'));

            if(System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }

            if(funcionDesdeDb == null)
            {
                return Json(new { success = false, message = "Error borrando función" });
            }

            _contenedorTrabajo.Funcion.Remove(funcionDesdeDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Función borrada con éxito" });

        }

        //Hacer metodo reserva
       

               




        #region LLAMADAS A LA API
        [HttpGet]

        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Funcion.GetAll(includeProperties: "Obra") });
        }

  
        #endregion


    }
}



