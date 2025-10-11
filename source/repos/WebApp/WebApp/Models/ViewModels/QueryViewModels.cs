

using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModels
{
    public class QueryViewModels
    { 
        public int _Id { get; set; }    
        public string _Email { get; set; }
        public int? _Edad {  get; set; }

    }

    public class AddUserViewModel
    {
        [Required]
        [Display(Name = "Nombre Usuario")]
        public string _Nombre { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string _Clave { get; set; }
        [Required]
        [Display(Name = "Confirmar Password")]
        [DataType(DataType.Password)]
        [Compare("_Clave", ErrorMessage = "The password and confirmation password do not match.")]
        public string _ClaveConfirma { get; set; }
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "Inser your Email", MinimumLength = 6)]
        public string _Email { get; set; }
        [Required]
        [Display(Name = "Edad")]
        public int _Edad { get; set; }
    }

    public class EditUserViewModel
    {
        public int _Id { get; set; }


        [Required]
        [Display(Name = "Nombre Usuario")]
        public string _Nombre { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string _Clave { get; set; }
        [Required]
        [Display(Name = "Confirmar Password")]
        [DataType(DataType.Password)]
        [Compare("_Clave", ErrorMessage = "The password and confirmation password do not match.")]
        public string _ClaveConfirma { get; set; }
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "Inser your Email", MinimumLength = 6)]
        public string _Email { get; set; }
        [Required]
        [Display(Name = "Edad")]
        public int? _Edad { get; set; }
    }


}