using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BulkSMSWebApp.Models
{
    public class Departamento
    {
        public int ID { get; set; }
       
        [Display(Name="Departamento")]
        public String NombreDepartameto { get; set; }


        //Propiedades de Navegación
        public virtual ICollection<Municipio> Municipios { get; set; }

        public virtual ICollection<Contacto> Contactos { get; set; }

    }
}
