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

        [MaxLength(36, ErrorMessage = "El texto ingresado excede el límite de caractéres permitidos")]
        public String Nombres { get; set; }

        [MaxLength(36, ErrorMessage = "El texto ingresado excede el límite de caractéres permitidos")]
        public String Apellidos { get; set; }


        [MaxLength(36, ErrorMessage = "El texto ingresado excede el límite de caractéres permitidos")]
        [DataType(DataType.EmailAddress,ErrorMessage="El Email ingresado no es válido")]
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
        public virtual ICollection<Grupo> Grupos { get; set; }
        public virtual ICollection<Telefono> Telefonos { get; set; }

     
       
    }
}