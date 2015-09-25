namespace BulkSMSWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEstadoIDToGroupClass : DbMigration
    {
        public override void Up()
        {         
            AddColumn("dbo.Grupo", "EstadoID", c => c.Int(nullable: false));
            AddForeignKey("dbo.Grupo", "EstadoID", "dbo.Estado", "EstadoID", cascadeDelete: false);
            CreateIndex("dbo.Grupo", "EstadoID");
        }       
        public override void Down()
        {
            DropForeignKey("dbo.Grupo", "EstadoID", "dbo.Estado");
            DropIndex("dbo.Grupo", new[] { "EstadoID" });
            DropColumn("dbo.Grupo", "EstadoID");
        }
    }
}
