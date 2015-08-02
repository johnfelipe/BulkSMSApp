namespace BulkSMSWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeEntityTelephoneColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Telefono", "Descripcion", c => c.String(nullable: false, maxLength: 20));
            DropColumn("dbo.Telefono", "Discriminador");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Telefono", "Discriminador", c => c.String(nullable: false));
            DropColumn("dbo.Telefono", "Descripcion");
        }
    }
}
