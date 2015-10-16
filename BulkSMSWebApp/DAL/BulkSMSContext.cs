using BulkSMSWebApp.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using BulkSMSWebApp.ViewModels;

namespace BulkSMSWebApp.DAL
{
    public class BulkSMSContext : DbContext
    {
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Mensaje> Mensajes { get; set; }
        public DbSet<FlujoMensaje> Flujos { get; set; }
        public DbSet<Telefono> Telefonos { get; set; }
 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Manejamos una relacion de muchos a muchos creando una tabla intermedia
            modelBuilder.Entity<Grupo>()
                .HasMany(c => c.Telefonos)
                .WithMany(i => i.Grupos)
                .Map(t => t.MapLeftKey("GrupoID")
                            .MapRightKey("TelefonoID")
                            .ToTable("GrupoContacto"));


            // Manejamos una relación de muchos mensajes con muchos telefonos a través de una tabla intermedia llamada MensajeTelefono
            modelBuilder.Entity<Mensaje>()
                .HasMany(t => t.Telefonos)
                .WithMany(m => m.Mensajes)
                .Map(t => t.MapLeftKey("MensajeID")
                            .MapRightKey("TelefonoID")
                            .ToTable("MensajeTelefono"));

           // le decimos que EstadoID es ForeingKey en la tabla Contacto pero deshabilitamos la eliminacion en cascada
            modelBuilder.Entity<Contacto>()
                .HasRequired(e => e.Estado)
                .WithMany(c => c.Contactos)
                .HasForeignKey(d => d.EstadoID)
                .WillCascadeOnDelete(false);

            // le decimos que EstadoID es ForeingKey en la tabla Mensaje pero deshabilitamos la eliminacion en cascada
            modelBuilder.Entity<Mensaje>()
                .HasRequired(e => e.Estado)
                .WithMany(m => m.Mensajes)
                .HasForeignKey(me => me.EstadoID)
                .WillCascadeOnDelete(false);

            // le decimos que FlujoID es ForeingKey en la tabla Mensaje pero deshabilitamos la eliminacion en cascada
            modelBuilder.Entity<Mensaje>()
                .HasRequired(f => f.Flujo_Mensaje)
                .WithMany(m => m.Mensajes)
                .HasForeignKey(me => me.FlujoID)
                .WillCascadeOnDelete(false);

            // Manejamos una relacion entre teléfono y contacto (contactoID es llave foranea en la tabla Telefono)
            modelBuilder.Entity<Telefono>()
                .HasRequired(f => f.Contacto)
                .WithMany(t => t.Telefonos)
                .WillCascadeOnDelete(false);
        }

    }

}