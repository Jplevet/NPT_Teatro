using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NPT_Teatro.Models
{
   public class Obra
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingresar nombre de la obra")]
        [Display(Name ="Nombre Obra")]
        public string Nombre { get; set; }

    }
}
