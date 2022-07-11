using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPT_Teatro.AccesoDatos.Data.Repository;
using NPT_Teatro.Models;
using NPT_Teatro.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NPT_Teatro.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnviroment;

        public HomeController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnviroment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnviroment = hostingEnviroment;
        }

        public IActionResult Index()
        {
            HomeVM homeVm = new HomeVM()
            {
                ListaFunciones = _contenedorTrabajo.Funcion.GetAll()
            };
            return View(homeVm);
        }

        public IActionResult Details(int id)
        {
            var funcionDesdeDb = _contenedorTrabajo.Funcion.GetFirstOrDefault(a => a.Id ==id);
            return View(funcionDesdeDb);
        }


        [HttpGet]
        public IActionResult Reservas(int? id)
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
        public IActionResult Reservas(FuncionVM funVM)
        {
            /* if(ModelState.IsValid)
             {*/
            string rutaPrincipal = _hostingEnviroment.WebRootPath;
            var archivos = HttpContext.Request.Form.Files;
            //Obtener archivo por su ID
            var funcionDesdeDb = _contenedorTrabajo.Funcion.Get(funVM.Funcion.Id);

         

            _contenedorTrabajo.Funcion.Update(funVM.Funcion);
            _contenedorTrabajo.Save();

            return RedirectToAction(nameof(Index));
            //}

            /*funVM.ListaObras = _contenedorTrabajo.Obra.GetListaObras();
            return View(funVM);*/
        }
    public IActionResult Reservar(FuncionVM funVM, int cantReservas, UsuarioVM usuarioVM)
         {

            
            var funcionDesdeDb = _contenedorTrabajo.Funcion.Get(funVM.Funcion.Id);
            var usuarioDesdeDb = _contenedorTrabajo.Usuario.Get(usuarioVM.Usuario.Id);

            funcionDesdeDb.Cupo = funcionDesdeDb.Cupo - cantReservas;

            funVM.Funcion.UrlImagen = funcionDesdeDb.UrlImagen;
            funVM.Funcion.ObraId = funcionDesdeDb.ObraId;
            funVM.Funcion.Obra = funcionDesdeDb.Obra;
            funVM.Funcion.Id = funcionDesdeDb.Id;
            funVM.Funcion.Fecha = funcionDesdeDb.Fecha;

            Reserva reserva = new Reserva(usuarioVM.Usuario.Email, cantReservas, funcionDesdeDb.Id);
            CrearReserva(reserva);

            _contenedorTrabajo.Funcion.Update(funcionDesdeDb);
            _contenedorTrabajo.Save();


            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult CreearReserva()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearReserva(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Reserva.Add(reserva);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(reserva);
        }
    }



}
