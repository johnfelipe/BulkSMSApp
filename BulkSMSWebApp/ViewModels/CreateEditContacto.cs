using BulkSMSWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulkSMSWebApp.ViewModels
{
    public class CreateEditContacto
    {
        public Contacto Contactos { get; set; }
        public List<Telefono> Telefonos { get; set; }
    }
}