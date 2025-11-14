namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmpresaTituloComentario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TituloComentario", "Empresa", c => c.String(nullable: false, maxLength: 4, fixedLength: true, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TituloComentario", "Empresa");
        }
    }
}
