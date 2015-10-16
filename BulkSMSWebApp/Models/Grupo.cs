using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BulkSMSWebApp.Models
{
    public class Grupo
    {
        public int GrupoID { get; set; }
        
        [Required(ErrorMessage="Debe específicar un Nombre de Grupo")]
        [MaxLength(36, ErrorMessage = "El texto ingresado excede el límite de caractéres permitidos")]
        [Display(Name="Nombre del Grupo")]
        public String NombreGrupo { get; set; }

        [MaxLength(150, ErrorMessage = "El texto ingresado excede el límite de caractéres permitidos")]
        [MinLength(12, ErrorMessage="El texto ingresado es demasiado corto")]
        public String Descripcion { get; set; }
        
        [Display(Name="Fecha de Creación")]
        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; }
        public int EstadoID { get; set; }

        //propiedades de navegación
        public virtual IList<Telefono> Telefonos { get; set; }
        public virtual Estado Estado { get; set; }
    }
}