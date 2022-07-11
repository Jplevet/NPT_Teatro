using System;
using System.Collections.Generic;
using System.Text;

namespace NPT_Teatro.AccesoDatos.Data.Repository
{
    public interface IContenedorTrabajo : IDisposable
    {
        IObraRepository Obra { get; }
        IFuncionRepository Funcion { get; }
        IUsuarioRepository Usuario { get;}
        IReservaRepository Reserva { get; }
        
        void Save();
    }
}
