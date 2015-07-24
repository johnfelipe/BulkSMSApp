using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BulkSMSWebApp.Models
{
    public class FlujoMensaje
    {
        public int FlujoMensajeID { get; set; }

        [MaxLength(16, ErrorMessage = "El texto ingresado excede el límite de caractéres permitidos")]
        public String NombreFlujo { get; set; }

        //Propiedades de Navegación
        public virtual ICollection<Mensaje> Mensajes { get; set; }
    }
}