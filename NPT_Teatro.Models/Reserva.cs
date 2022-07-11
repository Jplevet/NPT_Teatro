using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NPT_Teatro.Models
{
   public class Reserva
    {

        [Key]
        public int Id { get; set; }
       
        public string Email { get; set; }
        public int CantEntradas { get; set; }

        public int FuncionId { get; set; }
        [ForeignKey("FuncionId")]
        public Funcion Funcion { get; set; }


        public Reserva(string Email, int CantEntradas, int FuncionId)
        {
            
            Email = this.Email;
            CantEntradas = this.CantEntradas;
            FuncionId = this.FuncionId;
            
        }

    }
}
