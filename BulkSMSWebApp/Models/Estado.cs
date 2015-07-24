using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BulkSMSWebApp.Models
{
    public class Estado
    {
        public int EstadoID { get; set; }

        [MaxLength(16, ErrorMessage = "El texto ingresado excede el límite de caractéres permitidos")]
        [Display(Name="Estado")]
        public String NombreEstado { get; set; }


        // Propiedades de Navegación
        public virtual ICollection<Contacto> Contactos { get; set; }
        public virtual ICollection<Mensaje> Mensajes { get; set; }
    }
}
