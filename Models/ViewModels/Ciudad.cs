using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models.ViewModels
{
    public class Ciudad
    {  


        [Key]
        [Required]
        public int IdCiudad { get; set; }

        [Required]
        [StringLength(110)]
        public string Nombre { get; set; }  

    }
}