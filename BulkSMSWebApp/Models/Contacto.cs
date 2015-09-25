using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkSMSWebApp.Models
{
    public class Contacto
    {
        public int ContactoID { get; set; }

        [Required(ErrorMessage="El campo Nombres es requerido")]
        [MaxLength(36, ErrorMessage = "El texto ingresado excede el límite de caractéres permitidos")]
        [MinLength(3, ErrorMessage = "El Nombre debe tener al menos 3 caracteres")]
        public String Nombres { get; set; }

        [Required(ErrorMessage = "El campo Apellidos es requerido")]
        [MinLength(3, ErrorMessage = "El Apellido debe tener al menos 3 caracteres")]
        [MaxLength(36, ErrorMessage = "El texto ingresado excede el límite de caractéres permitidos")]
        public String Apellidos { get; set; }

        [MaxLength(36, ErrorMessage = "El texto ingresado excede el límite de caractéres permitidos")]
        [EmailAddress(ErrorMessage = "El Email ingresado no tiene el formato correcto")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [Display(Name = "Estado")]
        public int EstadoID { get; set; }

        [Display(Name = "Departamento")]
        public int? DepartamentoID { get; set; }

        [Display(Name = "Nombre Completo")]
        public String NombreCompleto
        {
            get
            {
                return Nombres + " " + Apellidos;
            }
        }

        // Propiedades de Navegacioon
        public virtual Estado Estado { get; set; }
        public virtual Departamento Departamento { get; set; }
        public virtual ICollection<Telefono> Telefonos { get; set; }

    }
}