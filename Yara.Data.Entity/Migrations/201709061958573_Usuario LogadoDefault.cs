namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsuarioLogadoDefault : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "EmpresaLogada", c => c.String(maxLength: 120, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "EmpresaLogada");
        }
    }
}
