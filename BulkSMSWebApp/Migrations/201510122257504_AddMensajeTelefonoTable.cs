namespace BulkSMSWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMensajeTelefonoTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Mensaje", "ContactoID", "dbo.Contacto");
            DropIndex("dbo.Mensaje", new[] { "ContactoID" });
            CreateTable(
                "dbo.MensajeTelefono",
                c => new
                    {
                        MensajeID = c.Int(nullable: false),
                        TelefonoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MensajeID, t.TelefonoID })
                .ForeignKey("dbo.Mensaje", t => t.MensajeID, cascadeDelete: true)
                .ForeignKey("dbo.Telefono", t => t.TelefonoID, cascadeDelete: true)
                .Index(t => t.MensajeID)
                .Index(t => t.TelefonoID);
            
            DropColumn("dbo.Mensaje", "ContactoID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Mensaje", "ContactoID", c => c.Int(nullable: false));
            DropForeignKey("dbo.MensajeTelefono", "TelefonoID", "dbo.Telefono");
            DropForeignKey("dbo.MensajeTelefono", "MensajeID", "dbo.Mensaje");
            DropIndex("dbo.MensajeTelefono", new[] { "TelefonoID" });
            DropIndex("dbo.MensajeTelefono", new[] { "MensajeID" });
            DropTable("dbo.MensajeTelefono");
            CreateIndex("dbo.Mensaje", "ContactoID");
            AddForeignKey("dbo.Mensaje", "ContactoID", "dbo.Contacto", "ContactoID", cascadeDelete: true);
        }
    }
}
