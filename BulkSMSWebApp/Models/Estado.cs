using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BulkSMSWebApp.Models
{
    public class Estado
    {
        public int ID { get; set; }
        
        [Display(Name="Estado")]
        public String NombreEstado { get; set; }


        // Propiedades de Navegación
        public virtual ICollection<Contacto> Contactos { get; set; }
        public virtual ICollection<Mensaje> Mensajes { get; set; }
    }
}
