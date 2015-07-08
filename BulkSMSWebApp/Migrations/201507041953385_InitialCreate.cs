namespace BulkSMSWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacto",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Celular = c.String(),
                        Nombres = c.String(),
                        Apellidos = c.String(),
                        Email = c.String(),
                        EstadoID = c.Int(nullable: false),
                        DepartamentoID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Departamento", t => t.DepartamentoID)
                .ForeignKey("dbo.Estado", t => t.EstadoID)
                .Index(t => t.EstadoID)
                .Index(t => t.DepartamentoID);
            
            CreateTable(
                "dbo.Departamento",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NombreDepartameto = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Municipio",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        MunicipioNombre = c.String(),
                        DepartamentoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Departamento", t => t.DepartamentoID, cascadeDelete: true)
                .Index(t => t.DepartamentoID);
            
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NombreEstado = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Mensaje",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FechaMensaje = c.DateTime(nullable: false),
                        CuerpoMensaje = c.String(),
                        ContactoID = c.Int(nullable: false),
                        EstadoID = c.Int(nullable: false),
                        FlujoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Contacto", t => t.ContactoID, cascadeDelete: true)
                .ForeignKey("dbo.Estado", t => t.EstadoID)
                .ForeignKey("dbo.FlujoMensaje", t => t.FlujoID)
                .Index(t => t.ContactoID)
                .Index(t => t.EstadoID)
                .Index(t => t.FlujoID);
            
            CreateTable(
                "dbo.FlujoMensaje",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NombreFlujo = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Grupo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NombreGrupo = c.String(),
                        Descripcion = c.String(),
                        FechaCreacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.GrupoContacto",
                c => new
                    {
                        GrupoID = c.Int(nullable: false),
                        ContactoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GrupoID, t.ContactoID })
                .ForeignKey("dbo.Grupo", t => t.GrupoID, cascadeDelete: true)
                .ForeignKey("dbo.Contacto", t => t.ContactoID, cascadeDelete: true)
                .Index(t => t.GrupoID)
                .Index(t => t.ContactoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GrupoContacto", "ContactoID", "dbo.Contacto");
            DropForeignKey("dbo.GrupoContacto", "GrupoID", "dbo.Grupo");
            DropForeignKey("dbo.Contacto", "EstadoID", "dbo.Estado");
            DropForeignKey("dbo.Mensaje", "FlujoID", "dbo.FlujoMensaje");
            DropForeignKey("dbo.Mensaje", "EstadoID", "dbo.Estado");
            DropForeignKey("dbo.Mensaje", "ContactoID", "dbo.Contacto");
            DropForeignKey("dbo.Municipio", "DepartamentoID", "dbo.Departamento");
            DropForeignKey("dbo.Contacto", "DepartamentoID", "dbo.Departamento");
            DropIndex("dbo.GrupoContacto", new[] { "ContactoID" });
            DropIndex("dbo.GrupoContacto", new[] { "GrupoID" });
            DropIndex("dbo.Mensaje", new[] { "FlujoID" });
            DropIndex("dbo.Mensaje", new[] { "EstadoID" });
            DropIndex("dbo.Mensaje", new[] { "ContactoID" });
            DropIndex("dbo.Municipio", new[] { "DepartamentoID" });
            DropIndex("dbo.Contacto", new[] { "DepartamentoID" });
            DropIndex("dbo.Contacto", new[] { "EstadoID" });
            DropTable("dbo.GrupoContacto");
            DropTable("dbo.Grupo");
            DropTable("dbo.FlujoMensaje");
            DropTable("dbo.Mensaje");
            DropTable("dbo.Estado");
            DropTable("dbo.Municipio");
            DropTable("dbo.Departamento");
            DropTable("dbo.Contacto");
        }
    }
}
