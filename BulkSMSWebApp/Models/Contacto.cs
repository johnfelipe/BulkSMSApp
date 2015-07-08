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
        public int ID { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [RegularExpression(@"^[505-]?[5|8|7][0-9]{7}$", ErrorMessage = "Especifique un número de Celular válido")]
        [MinLength(8, ErrorMessage = "Especifique un número de Celular válido"), MaxLength(12, ErrorMessage = "El Número ingresado excede el límite de caractéres")]
        public String Celular { get; set; }

        [MaxLength(36, ErrorMessage = "El texto excede el límite de caractéres permitidos")]
        public String Nombres { get; set; }

        [MaxLength(36, ErrorMessage = "El texto excede el límite de caractéres permitidos")]
        public String Apellidos { get; set; }


        [MaxLength(36, ErrorMessage = "El texto excede el límite de caractéres permitidos")]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }


        [Required(ErrorMessage = "Campo Requerido")]
        [Display(Name = "Estado")]
        public int EstadoID { get; set; }

        [Display(Name = "Departamento")]
        public int? DepartamentoID { get; set; }


        // Propiedades de Navegacioon
        public virtual Estado Estado { get; set; }
        public virtual Departamento Departamento { get; set; }
        public virtual ICollection<Grupo> Grupos { get; set; }

        [Display(Name = "Nombre Completo")]
        public String NombreCompleto
        {
            get
            {
                return Nombres + " " + Apellidos;
            }
        }
    }
}