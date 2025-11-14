namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnexoEstruturadetabela : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Anexo", "DescricaoAbreviado", c => c.String(nullable: false, maxLength: 10, unicode: false));
            AddColumn("dbo.Anexo", "Formulario", c => c.Int(nullable: false));
            AddColumn("dbo.Anexo", "DescricaoFormulario", c => c.String(nullable: false, maxLength: 50, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Anexo", "DescricaoFormulario");
            DropColumn("dbo.Anexo", "Formulario");
            DropColumn("dbo.Anexo", "DescricaoAbreviado");
        }
    }
}
