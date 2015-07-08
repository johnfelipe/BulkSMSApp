using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BulkSMSWebApp.Models
{
    public class Grupo
    {
        public int ID { get; set; }
        
        [Required(ErrorMessage="Debe específicar un Nombre de Grupo")]
        [MaxLength(36, ErrorMessage = "El texto excede el límite de caractéres permitidos")]
        [Display(Name="Nombre del Grupo")]
        public String NombreGrupo { get; set; }
        public String Descripcion { get; set; }
        
        [Display(Name="Fecha de Creación")]
        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; }

        //Propiedades de Navegacion 
        public virtual ICollection<Contacto> Contactos { get; set; }
    }
}