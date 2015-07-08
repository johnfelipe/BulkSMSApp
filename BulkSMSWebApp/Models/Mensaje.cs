using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BulkSMSWebApp.Models
{
    public class Mensaje
    {
        public int ID { get; set; }
        
        [Display(Name="Fecha")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaMensaje { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(160,ErrorMessage="El máximo de caracteres permitidos es de 160")]
        [Required(ErrorMessage="Debe específicar el cuerpo del mensaje")]
        [Display(Name = "Mensaje")]
        public String CuerpoMensaje { get; set; }

        [Display(Name = "Contacto")]
        public int ContactoID { get; set; }

        [Display(Name = "Estado")]
        public int EstadoID { get; set; }

        [Display(Name = "Flujo")]
        public int FlujoID { get; set; }

        // Propiedades de Navegación
        public virtual Contacto Contacto { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual FlujoMensaje Flujo_Mensaje { get; set; }
    }
}