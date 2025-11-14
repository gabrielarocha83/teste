namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnexoLayoutProposta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Anexo", "LayoutsProposta", c => c.String(maxLength: 255, unicode: false));
            DropColumn("dbo.Anexo", "DescricaoAbreviado");
            DropColumn("dbo.Anexo", "Formulario");
            DropColumn("dbo.Anexo", "DescricaoFormulario");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Anexo", "DescricaoFormulario", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AddColumn("dbo.Anexo", "Formulario", c => c.Int(nullable: false));
            AddColumn("dbo.Anexo", "DescricaoAbreviado", c => c.String(nullable: false, maxLength: 10, unicode: false));
            DropColumn("dbo.Anexo", "LayoutsProposta");
        }
    }
}
