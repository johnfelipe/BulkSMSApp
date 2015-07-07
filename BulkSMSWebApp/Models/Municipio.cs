using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkSMSWebApp.Models
{
    public class Municipio
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        

        [Display(Name="Municipio")]
        public String MunicipioNombre { get; set; }

        public int DepartamentoID { get; set; }

        //Propiedades de Navegacion 
        public virtual Departamento Departamento { get; set; }
    }
}
