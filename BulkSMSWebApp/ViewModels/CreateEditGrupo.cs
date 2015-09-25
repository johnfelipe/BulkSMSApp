using BulkSMSWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulkSMSWebApp.ViewModels
{
    public class CreateEditGrupo
    {
        public Grupo GroupInstance { get; set; }
        public IList<Telefono> Telefonos { get; set; }
    }
}