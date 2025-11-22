

using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModels
{
    public class TipoPropiedad
    { 
        public int IdTipoPropiedad { get; set; }
        [Required]
        public string Descripcion { get; set; }

        [Display(Name=  "Activo")]
        [Required]
        public bool Activo { get; set; }  

        public DateTime FechaRegistro { get; set; }


    }
}