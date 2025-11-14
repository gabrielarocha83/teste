namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCodSAPLiberacaoFluxoGrupoEconomico : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LiberacaoGrupoEconomicoFluxo", "CodigoSap", c => c.String(nullable: false, maxLength: 10, unicode: false));
            CreateIndex("dbo.LiberacaoGrupoEconomicoFluxo", "CodigoSap");
            AddForeignKey("dbo.LiberacaoGrupoEconomicoFluxo", "CodigoSap", "dbo.EstruturaComercial", "CodigoSap");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LiberacaoGrupoEconomicoFluxo", "CodigoSap", "dbo.EstruturaComercial");
            DropIndex("dbo.LiberacaoGrupoEconomicoFluxo", new[] { "CodigoSap" });
            DropColumn("dbo.LiberacaoGrupoEconomicoFluxo", "CodigoSap");
        }
    }
}
