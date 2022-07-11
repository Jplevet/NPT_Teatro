using NPT_Teatro.Models;

namespace NPT_Teatro.AccesoDatos.Data.Repository
{ 
    public interface IReservaRepository : IRepository<Reserva>
    {
        void Update(Reserva reserva);
    }
}