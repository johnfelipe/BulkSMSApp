using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BulkSMSWebApp.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "El Campo Email es Obligatorio")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "El Email ingresado no es válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El Campo Contraseña es Obligatorio")]
        [MaxLength(36, ErrorMessage = "El texto ingresado excede el límite de caractéres permitidos")]
        [DataType(DataType.Password, ErrorMessage = "La Contraseña ingresada no es válida")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Recordar mis datos?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El Campo Nombres es Obligatorio")]
        [MaxLength(36, ErrorMessage = "El Nombre debe tener como máximo 36 caracteres")]
        [MinLength(3, ErrorMessage = "El Nombre debe tener al menos 3 caracteres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El Campo Apellidos es Obligatorio")]
        [MaxLength(36, ErrorMessage = "El Apellido debe tener como máximo 36 caracteres")]
        [MinLength(3, ErrorMessage = "El Apellido debe tener al menos 3 caracteres")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El Campo Email es Obligatorio")]
        [MaxLength(36, ErrorMessage = "El Email debe tener como máximo 36 caracteres")]
        [EmailAddress(ErrorMessage = "El Email ingresado no tiene el formato correcto")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El Campo Contraseña es Obligatorio")]
        [StringLength(100, ErrorMessage = "La Contraseña debe tener al menos 6 caracteres", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "La Contraseña ingresada no es válida")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password, ErrorMessage = "La Contraseña ingresada no es válida")]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas ingresadas no coinciden")]
        public string ConfirmPassword { get; set; }

        public int? EstadoID { get; set; }
        public DateTime fecha_registro { get; set; }

        [Required(ErrorMessage = "Seleccione un tipo de Usuario")]
        [Display(Name = "Tipo de Usuario")]
        public string Rol { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "El Campo Email es Obligatorio")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La contraseña debe tener 6 caracteres como mínimo", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas ingresadas no coinciden")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Ingresa tu Email")]
        public string Email { get; set; }
    }
}
