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
        [Required(ErrorMessage = "Campo Obligatorio")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "El Email ingresado no es válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        [MaxLength(36, ErrorMessage = "El texto ingresado excede el límite de caractéres permitidos")]
        [DataType(DataType.Password, ErrorMessage = "La Contraseña ingresada no es válida")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Recordar mis datos?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Campo Obligatorio")]
        [MaxLength(36, ErrorMessage = "El texto ingresado excede el límite de caractéres permitidos")]
        [MinLength(3, ErrorMessage = "El Nombre debe tener al menos 3 caracteres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        [MaxLength(36, ErrorMessage = "El texto ingresado excede el límite de caractéres permitidos")]
        [MinLength(3, ErrorMessage = "El Apellido debe tener al menos 3 caracteres")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        [MaxLength(36, ErrorMessage = "El texto ingresado excede el límite de caractéres permitidos")]
        [EmailAddress(ErrorMessage = "El Email ingresado no es válido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        [StringLength(100, ErrorMessage = "La Contraseña debe tener al menos 6 caracteres", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "La Contraseña ingresada no es válida")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password, ErrorMessage = "La Contraseña ingresada no es válida")]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage = "Ambas contraseñas ingresadas deben coincidir")]
        public string ConfirmPassword { get; set; }
        public DateTime fecha_registro { get; set; }
        //public string Estado { get; set; }
        
        [Required(ErrorMessage="Campo Requerido")]
        [Display(Name="Tipo de Usuario")]
        public string Rol { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Campo Obligatorio")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
