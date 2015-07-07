using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkSMSWebApp.Models
{
    public class Contacto
    {
        public int ID { get; set; }
        public String Celular { get; set; }
        public String  Nombres { get; set; }
        public String Apellidos { get; set; }
        public String Email { get; set; }
        
        [Display(Name="Estado")]
        public int EstadoID { get; set; }

        [Display(Name = "Departamento")]
        public int? DepartamentoID { get; set; }


        // Propiedades de Navegacioon
        public virtual Estado Estado { get; set; }
        public virtual Departamento Departamento { get; set; }
        public virtual ICollection<Grupo> Grupos { get; set; }

        public String NombreCompleto
        {
            get
            {
                return Nombres + " " + Apellidos;
            }
        }
    }
}