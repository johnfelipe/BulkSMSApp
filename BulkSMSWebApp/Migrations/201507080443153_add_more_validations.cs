namespace BulkSMSWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_more_validations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacto", "Celular", c => c.String(nullable: false, maxLength: 12));
            AlterColumn("dbo.Contacto", "Nombres", c => c.String(maxLength: 36));
            AlterColumn("dbo.Contacto", "Apellidos", c => c.String(maxLength: 36));
            AlterColumn("dbo.Contacto", "Email", c => c.String(maxLength: 36));
            AlterColumn("dbo.Mensaje", "CuerpoMensaje", c => c.String(nullable: false, maxLength: 160));
            AlterColumn("dbo.Grupo", "NombreGrupo", c => c.String(nullable: false, maxLength: 36));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Grupo", "NombreGrupo", c => c.String());
            AlterColumn("dbo.Mensaje", "CuerpoMensaje", c => c.String());
            AlterColumn("dbo.Contacto", "Email", c => c.String());
            AlterColumn("dbo.Contacto", "Apellidos", c => c.String());
            AlterColumn("dbo.Contacto", "Nombres", c => c.String());
            AlterColumn("dbo.Contacto", "Celular", c => c.String());
        }
    }
}
