using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulkSMSWebApp.ViewModels
{
    public class GrupoContacto
    {
        public int GrupoID { get; set; }
        public int TelefonoID { get; set; }
        public string Nombres { get; set; }
        public string NombreDepartameto { get; set; }
        public string NumeroTel { get; set; }
        public string Descripcion { get; set; }
    }
}