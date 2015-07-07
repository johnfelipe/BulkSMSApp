using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulkSMSWebApp.Models
{
    public class Mensaje
    {
        public int ID { get; set; }
        public DateTime FechaMensaje { get; set; }
        public String CuerpoMensaje { get; set; }
        public int ContactoID { get; set; }
        public int EstadoID { get; set; }
        public int FlujoID { get; set; }

        // Propiedades de Navegación
        public virtual Contacto Contacto { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual FlujoMensaje Flujo_Mensaje { get; set; }
    }
}