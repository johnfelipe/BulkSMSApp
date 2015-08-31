namespace BulkSMSWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeteñephonevalidation : DbMigration
    {
        public override void Up()
        {
            //DropColumn("dbo.Telefono", "Descripcion");
            //AddColumn("dbo.Telefono", "Descripcion", c => c.String(nullable: false));
            //AlterColumn("dbo.Telefono", "Descripcion", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.Telefono", "Descripcion", c => c.String(nullable: false, maxLength: 20));
          
        }
    }
}
