using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPT_Teatro.Models.ViewModels
{
    public class FuncionVM
    {
        public Funcion Funcion { get; set; }

        public IEnumerable<SelectListItem> ListaObras { get; set; }
    }
}
