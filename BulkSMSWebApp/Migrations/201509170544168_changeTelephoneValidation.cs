namespace BulkSMSWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeTelephoneValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Telefono", "NumeroTel", c => c.String(maxLength: 12));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Telefono", "NumeroTel", c => c.String(nullable: false, maxLength: 12));
        }
    }
}
