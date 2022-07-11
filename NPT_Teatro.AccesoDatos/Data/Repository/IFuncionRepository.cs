using Microsoft.AspNetCore.Mvc.Rendering;
using NPT_Teatro.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPT_Teatro.AccesoDatos.Data.Repository
{
    public interface IFuncionRepository : IRepository<Funcion>
    {
       

        void Update(Funcion funcion);
    }
}
