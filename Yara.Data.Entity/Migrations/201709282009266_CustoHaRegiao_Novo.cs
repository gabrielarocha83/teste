namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustoHaRegiao_Novo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustoHaRegiao", "RegiaoID", "dbo.Regiao");
            DropIndex("dbo.CustoHaRegiao", new[] { "RegiaoID" });
            AddColumn("dbo.CustoHaRegiao", "CidadeID", c => c.Guid(nullable: false));
            AddColumn("dbo.CustoHaRegiao", "ValorHaCultivavel", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.CustoHaRegiao", "ValorHaNaoCultivavel", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.CustoHaRegiao", "ModuloRural", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.CustoHaRegiao", "CidadeID");
            AddForeignKey("dbo.CustoHaRegiao", "CidadeID", "dbo.Cidade", "ID");
            DropColumn("dbo.CustoHaRegiao", "Custo");
            DropColumn("dbo.CustoHaRegiao", "Descricao");
            DropColumn("dbo.CustoHaRegiao", "RegiaoID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustoHaRegiao", "RegiaoID", c => c.Guid(nullable: false));
            AddColumn("dbo.CustoHaRegiao", "Descricao", c => c.String(nullable: false, maxLength: 240, unicode: false));
            AddColumn("dbo.CustoHaRegiao", "Custo", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.CustoHaRegiao", "CidadeID", "dbo.Cidade");
            DropIndex("dbo.CustoHaRegiao", new[] { "CidadeID" });
            DropColumn("dbo.CustoHaRegiao", "ModuloRural");
            DropColumn("dbo.CustoHaRegiao", "ValorHaNaoCultivavel");
            DropColumn("dbo.CustoHaRegiao", "ValorHaCultivavel");
            DropColumn("dbo.CustoHaRegiao", "CidadeID");
            CreateIndex("dbo.CustoHaRegiao", "RegiaoID");
            AddForeignKey("dbo.CustoHaRegiao", "RegiaoID", "dbo.Regiao", "ID");
        }
    }
}
