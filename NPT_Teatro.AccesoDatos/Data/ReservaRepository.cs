using Microsoft.AspNetCore.Mvc.Rendering;
using NPT_Teatro.AccesoDatos.Data.Repository;
using NPT_Teatro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NPT_Teatro.AccesoDatos.Data
{
    public class ReservaRepository : Repository<Reserva>, IReservaRepository
    {
        private readonly ApplicationDbContext _db;
        public ReservaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
      

        public void Update(Reserva reserva)
        {
            var objDesdeDb = _db.Reserva.FirstOrDefault(s => s.Id == reserva.Id);

            objDesdeDb.Email = reserva.Email;
            
            objDesdeDb.CantEntradas = reserva.CantEntradas;

            objDesdeDb.FuncionId = reserva.FuncionId;


            //Se hace directamente desde el controlador
            //_db.SaveChanges();
        }
    }
}
