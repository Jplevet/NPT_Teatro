using Microsoft.AspNetCore.Mvc.Rendering;
using NPT_Teatro.AccesoDatos.Data.Repository;
using NPT_Teatro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NPT_Teatro.AccesoDatos.Data
{
    public class ObraRepository : Repository<Obra>, IObraRepository
    {
        private readonly ApplicationDbContext _db;
        public ObraRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public IEnumerable<SelectListItem> GetListaObras()
        {
            return _db.Obra.Select(i => new SelectListItem()
            {
                Text = i.Nombre,
                Value = i.Id.ToString()
            }) ;
        }

        public void Update(Obra obra)
        {
            var objDesdeDb = _db.Obra.FirstOrDefault(s => s.Id == obra.Id);
            objDesdeDb.Nombre = obra.Nombre;

            _db.SaveChanges();
        }
    }
}
