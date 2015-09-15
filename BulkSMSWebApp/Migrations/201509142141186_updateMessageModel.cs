namespace BulkSMSWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateMessageModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mensaje", "EnviadoPor", c => c.String());
            AddColumn("dbo.Mensaje", "DesactivadoPor", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mensaje", "DesactivadoPor");
            DropColumn("dbo.Mensaje", "EnviadoPor");
        }
    }
}
