using BulkSMSWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulkSMSWebApp.ViewModels
{
    public class MensajeEntrante
    {
        public int MensajeID { get; set; }
        public string Nombres { get; set; }
        public string Telefono { get; set; }
        public string  CuerpoMensaje { get; set; }
        public DateTime FechaMensaje { get; set; }
        public int EstadoID { get; set; }
        public int FlujoID { get; set; }
    }

    public class MensajeEnviado
    {
        public int MensajeID { get; set; }
        public string EnviadoPor { get; set; }
        public string CuerpoMensaje { get; set; }
        public DateTime FechaMensaje { get; set; }
        public int EstadoID { get; set; }
        public int FlujoID { get; set; }
 
    }

    public class DestinosEnviados
    {
        public string Telefono { get; set; }
        public string Contacto { get; set; }
    }

    public class Eliminados : MensajeEntrante
    {
        public string EnviadoPor { get; set; }
        public string DesactivadoPor { get; set; }  
    }
}