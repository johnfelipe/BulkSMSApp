namespace BulkSMSWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeContactoValidations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacto", "Nombres", c => c.String(nullable: false, maxLength: 36));
            AlterColumn("dbo.Contacto", "Apellidos", c => c.String(nullable: false, maxLength: 36));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacto", "Apellidos", c => c.String(maxLength: 36));
            AlterColumn("dbo.Contacto", "Nombres", c => c.String(maxLength: 36));
        }
    }
}
