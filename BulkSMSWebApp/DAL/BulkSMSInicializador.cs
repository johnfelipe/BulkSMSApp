using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BulkSMSWebApp.Models;

namespace BulkSMSWebApp.DAL
{
    public class BulkSMSInicializador : System.Data.Entity.DropCreateDatabaseIfModelChanges<BulkSMSContext>
    {
        protected override void Seed(BulkSMSContext context)
        {

            #region Estado

            var estados= new List<Estado>
            {
                new Estado{NombreEstado="Activo"},
                new Estado{NombreEstado="Inactivo"},
                new Estado{NombreEstado="Enviado"},
                new Estado{NombreEstado="No enviado"},
                new Estado{NombreEstado="Leído"},
                new Estado{NombreEstado="No Leído"},
                new Estado{NombreEstado="Eliminado"},
            };

            estados.ForEach(e => context.Estados.Add(e));
            context.SaveChanges();

            #endregion

            #region FlujoMensaje

            var flujo = new List<FlujoMensaje>
            {
                new FlujoMensaje{NombreFlujo="Enviado"},
                new FlujoMensaje{NombreFlujo="Recibido"}

            };

            flujo.ForEach(f => context.Flujos.Add(f));
            context.SaveChanges();

            #endregion

            #region Departamento

            var Departamentos = new List<Departamento>
            {
                new Departamento{NombreDepartameto="Chinandega"},
                new Departamento{NombreDepartameto="León"},
                new Departamento{NombreDepartameto="Managua"},
                new Departamento{NombreDepartameto="Masaya"},
                new Departamento{NombreDepartameto="Granada"},
                new Departamento{NombreDepartameto="Carazo"},
                new Departamento{NombreDepartameto="Rivas"},
                new Departamento{NombreDepartameto="Nueva Segovia"},
                new Departamento{NombreDepartameto="Madríz"},
                new Departamento{NombreDepartameto="Estelí"},
                new Departamento{NombreDepartameto="Jinotega"},
                new Departamento{NombreDepartameto="Matagalpa"},
                new Departamento{NombreDepartameto="Boaco"},
                new Departamento{NombreDepartameto="Chontales"},
                new Departamento{NombreDepartameto="Rio San Juan"},
                new Departamento{NombreDepartameto="RAAN"},
                new Departamento{NombreDepartameto="RAAS"}
            };

            Departamentos.ForEach(d => context.Departamentos.Add(d));
            context.SaveChanges();

            #endregion


            #region Municipio

            var municipios = new List<Municipio>
            {
                new Municipio{ID=001, MunicipioNombre="Managua", DepartamentoID=3},
                new Municipio{ID=003, MunicipioNombre="Tipitapa", DepartamentoID=3},
                new Municipio{ID=008, MunicipioNombre="Ciudad Sandino", DepartamentoID=3},
                new Municipio{ID=007, MunicipioNombre="Ticuantepe", DepartamentoID=3},
                new Municipio{ID=081, MunicipioNombre="Chinandega",DepartamentoID=1},
                new Municipio{ID=086, MunicipioNombre="El Viejo",DepartamentoID=1},
                new Municipio{ID=201, MunicipioNombre="Granada",DepartamentoID=5},
                new Municipio{ID=287, MunicipioNombre="Nagarote", DepartamentoID=2},
                new Municipio{ID=281, MunicipioNombre="León", DepartamentoID=2}
            };

            municipios.ForEach(m => context.Municipios.Add(m));
            context.SaveChanges();

            #endregion


            #region Contacto

            var contactos = new List<Contacto>
            {
                new Contacto{Nombres="Dulce Mária", Apellidos="Pérez Alonzo", Email="dperez@outlook.com", Celular="+50584026619", EstadoID=1, DepartamentoID=3},
                new Contacto{Nombres="Francis Alejandra", Apellidos="Hernández", Email="fale25@gmail.com", Celular="+50587962069", EstadoID=1, DepartamentoID=3},
                new Contacto{Nombres="Adriana Jireh", Apellidos="Delgado Medina", Email="adrireh@hotmail.com", Celular="+50589497568", EstadoID=1, DepartamentoID=3},
                new Contacto{Nombres="Patricia Del Carmen", Apellidos="Medina Salgado", Email="psmedina@gmail.com", Celular="+50583268495", EstadoID=1, DepartamentoID=3},
                new Contacto{Nombres="Roberto", Apellidos="Velasquez", Email="robertv22@gmail.com", Celular="+50581160194", EstadoID=1, DepartamentoID=3},
                new Contacto{Nombres="Manuel", Apellidos="Trejos", Email="mtrejos@hotmail.com", Celular="+50586135451", EstadoID=1, DepartamentoID=3},
                new Contacto{Nombres="German Antonio", Apellidos="Noguera", Email="noguer92@gmail.com", Celular="+50578253601", EstadoID=1, DepartamentoID=3},
                new Contacto{Nombres="Adela", Apellidos="Maradiaga López", Email="acml67@hotmail.com", Celular="+50557165739", EstadoID=1, DepartamentoID=3},
                new Contacto{Nombres="Giovanni", Apellidos="Miranda Pérez", Email="giomperez@hotmail.com", Celular="+50588422756", EstadoID=1, DepartamentoID=3},
            };

            contactos.ForEach(c => context.Contactos.Add(c));
            context.SaveChanges();

            #endregion

            #region Mensajes

            var mensajes = new List<Mensaje>
            {
                //Mensajes Recibidos
                new Mensaje{FechaMensaje=DateTime.Now, CuerpoMensaje="ALTA PROMO", FlujoID=1, ContactoID=1, EstadoID=5},
                new Mensaje{FechaMensaje=DateTime.Now.AddDays(-1), CuerpoMensaje="ALTA PROMO", FlujoID=1, ContactoID=2, EstadoID=5},
                new Mensaje{FechaMensaje=DateTime.Now.AddDays(-1.3), CuerpoMensaje="ALTA PROMO", FlujoID=1,  ContactoID=3, EstadoID=5},
                new Mensaje{FechaMensaje=DateTime.Now.AddDays(-1.5), CuerpoMensaje="ALTA PROMO", FlujoID=1, ContactoID=4, EstadoID=5},

                //Mensajes Enviados
                new Mensaje{FechaMensaje=DateTime.Now.AddDays(-0.5), CuerpoMensaje="Envia 'ALTA PROMO' al 5051234567 y gana un mes de INTERNET GRATIS", FlujoID=2, ContactoID=1, EstadoID=3},
                new Mensaje{FechaMensaje=DateTime.Now.AddDays(-1.5), CuerpoMensaje="Envia 'ALTA PROMO' al 5051234567 y gana un mes de INTERNET GRATIS", FlujoID=2, ContactoID=2, EstadoID=3},
                new Mensaje{FechaMensaje=DateTime.Now.AddDays(-1.5), CuerpoMensaje="Envia 'ALTA PROMO' al 5051234567 y gana un mes de INTERNET GRATIS", FlujoID=2, ContactoID=3, EstadoID=3},
                new Mensaje{FechaMensaje=DateTime.Now.AddDays(-0.5), CuerpoMensaje="Envia 'ALTA PROMO' al 5051234567 y gana un mes de INTERNET GRATIS", FlujoID=2, ContactoID=4, EstadoID=3}
            };

            mensajes.ForEach(m => context.Mensajes.Add(m));
            context.SaveChanges();
            #endregion
        }
    }
}