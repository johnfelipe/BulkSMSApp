namespace BulkSMSWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_GrupoContactoToGrupoTel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GrupoContacto", "GrupoID", "dbo.Grupo");
            DropForeignKey("dbo.GrupoContacto", "ContactoID", "dbo.Contacto");
            DropIndex("dbo.GrupoContacto", new[] { "ContactoID" });
            DropTable("dbo.GrupoContacto");

            CreateTable(
                "dbo.GrupoContacto",
                c => new
                    {
                        GrupoID = c.Int(nullable: false),
                        TelefonoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GrupoID, t.TelefonoID })
                .ForeignKey("dbo.Grupo", t => t.GrupoID, cascadeDelete: true)
                .ForeignKey("dbo.Telefono", t => t.TelefonoID, cascadeDelete: true)
                .Index(t => t.TelefonoID);
            
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GrupoContacto",
                c => new
                    {
                        GrupoID = c.Int(nullable: false),
                        ContactoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GrupoID, t.ContactoID });
            
            DropForeignKey("dbo.GrupoContacto", "TelefonoID", "dbo.Telefono");
            DropForeignKey("dbo.GrupoContacto", "GrupoID", "dbo.Grupo");
            DropIndex("dbo.GrupoContacto", new[] { "TelefonoID" });
            DropTable("dbo.GrupoContacto");
            CreateIndex("dbo.GrupoContacto", "ContactoID");
            AddForeignKey("dbo.GrupoContacto", "ContactoID", "dbo.Contacto", "ContactoID", cascadeDelete: true);
            AddForeignKey("dbo.GrupoContacto", "GrupoID", "dbo.Grupo", "GrupoID", cascadeDelete: true);
        }
    }
}
