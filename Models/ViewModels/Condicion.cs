using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models.ViewModels
{
    public class Condicion
    {


        [Key]
        public int IdCondicion {  get; set; } 
        public string Descripcion {  get; set; }

        [Display(Name = "Activo")]
        [Required]
        public bool IsActive {  get; set; }  

    }
}