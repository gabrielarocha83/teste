namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPrecoRegiaoCampo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLCPrecoPorRegiao", "ValorHaCultivavelParametro", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLCPrecoPorRegiao", "ValorHaNaoCultivavelParametro", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLCPrecoPorRegiao", "ModuloRuralParametro", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLCPrecoPorRegiao", "ModuloRuralParametro");
            DropColumn("dbo.PropostaLCPrecoPorRegiao", "ValorHaNaoCultivavelParametro");
            DropColumn("dbo.PropostaLCPrecoPorRegiao", "ValorHaCultivavelParametro");
        }
    }
}
