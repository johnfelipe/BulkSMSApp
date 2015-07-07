using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulkSMSWebApp.Models
{
    public class Grupo
    {
        public int ID { get; set; }
        public String NombreGrupo { get; set; }
        public String Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }

        //Propiedades de Navegacion 
        public virtual ICollection<Contacto> Contactos { get; set; }
    }
}