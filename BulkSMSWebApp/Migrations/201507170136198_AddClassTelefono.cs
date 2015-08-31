namespace BulkSMSWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClassTelefono : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Telefono",
            //    c => new
            //        {
            //            TelefonoID = c.Int(nullable: false, identity: true),
            //            NumeroTel = c.String(nullable: false, maxLength: 12),
            //            Discriminador = c.String(nullable: false),
            //            ContactoID = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.TelefonoID)
            //    .ForeignKey("dbo.Contacto", t => t.ContactoID)
            //    .Index(t => t.ContactoID);
            
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.Telefono", "ContactoID", "dbo.Contacto");
            //DropIndex("dbo.Telefono", new[] { "ContactoID" });
            //DropTable("dbo.Telefono");
        }
    }
}
