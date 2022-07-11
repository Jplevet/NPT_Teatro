using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

 namespace NPT_Teatro.Models
{
    public class Funcion
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        [Display(Name = "Fecha y hora de la función")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "La imagen es obligatoria")]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string UrlImagen { get; set; }

        [Required]
        public int ObraId { get; set; }

        [ForeignKey("ObraId")]
        public Obra Obra { get; set; }

        public int Cupo {get; set;}



    }
}
