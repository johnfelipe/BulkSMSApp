using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulkSMSWebApp.Models
{
    public class FlujoMensaje
    {
        public int ID { get; set; }
        public String NombreFlujo { get; set; }

        //Propiedades de Navegación

        public virtual ICollection<Mensaje> Mensajes { get; set; }
    }
}