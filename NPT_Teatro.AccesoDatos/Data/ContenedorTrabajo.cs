using NPT_Teatro.AccesoDatos.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPT_Teatro.AccesoDatos.Data
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly ApplicationDbContext _db;

        public ContenedorTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Obra = new ObraRepository(_db);
            Funcion = new FuncionRepository(_db);
            Usuario = new UsuarioRepository(_db);
            Reserva = new ReservaRepository(_db);
        }
        public IObraRepository Obra { get; private set; }
        public IFuncionRepository Funcion { get; private set; }
        public IUsuarioRepository Usuario { get; private set; }
        public IReservaRepository Reserva { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
