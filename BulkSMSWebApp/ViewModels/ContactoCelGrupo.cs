using BulkSMSWebApp.Models;
using System.Collections.Generic;


namespace BulkSMSWebApp.ViewModels
{
    public class ContactoCelGrupo
    {
        public IEnumerable<Contacto> Contactos { get; set; }
        public IEnumerable<Telefono> Telefonos { get; set; }
        public IEnumerable<Grupo> Grupos { get; set; }
    }
}