using Microsoft.AspNetCore.Mvc.Rendering;
using NPT_Teatro.AccesoDatos.Data.Repository;
using NPT_Teatro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NPT_Teatro.AccesoDatos.Data
{
    public class FuncionRepository : Repository<Funcion>, IFuncionRepository
    {
        private readonly ApplicationDbContext _db;
        public FuncionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
      

        public void Update(Funcion funcion)
        {
            var objDesdeDb = _db.Funcion.FirstOrDefault(s => s.Id == funcion.Id);
            objDesdeDb.Fecha = funcion.Fecha;
            objDesdeDb.UrlImagen = funcion.UrlImagen;
            objDesdeDb.ObraId = funcion.ObraId;
            objDesdeDb.Cupo = funcion.Cupo;


            //Se hace directamente desde el controlador
            //_db.SaveChanges();
        }
    }
}
