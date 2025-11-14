namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mudancas_Fluxo_Aprovacao_LC : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.FluxoLiberacaoLimiteCredito", name: "PerfilID", newName: "PrimeiroPerfilID");
            RenameIndex(table: "dbo.FluxoLiberacaoLimiteCredito", name: "IX_PerfilID", newName: "IX_PrimeiroPerfilID");
            AddColumn("dbo.FluxoLiberacaoLimiteCredito", "SegmentoID", c => c.Guid(nullable: false));
            AddColumn("dbo.FluxoLiberacaoLimiteCredito", "SegundoPerfilID", c => c.Guid());
            CreateIndex("dbo.FluxoLiberacaoLimiteCredito", "SegmentoID");
            CreateIndex("dbo.FluxoLiberacaoLimiteCredito", "SegundoPerfilID");
            AddForeignKey("dbo.FluxoLiberacaoLimiteCredito", "SegmentoID", "dbo.Segmento", "ID");
            AddForeignKey("dbo.FluxoLiberacaoLimiteCredito", "SegundoPerfilID", "dbo.Perfil", "ID");
            DropColumn("dbo.FluxoLiberacaoLimiteCredito", "OrdemLiberacao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FluxoLiberacaoLimiteCredito", "OrdemLiberacao", c => c.Int(nullable: false));
            DropForeignKey("dbo.FluxoLiberacaoLimiteCredito", "SegundoPerfilID", "dbo.Perfil");
            DropForeignKey("dbo.FluxoLiberacaoLimiteCredito", "SegmentoID", "dbo.Segmento");
            DropIndex("dbo.FluxoLiberacaoLimiteCredito", new[] { "SegundoPerfilID" });
            DropIndex("dbo.FluxoLiberacaoLimiteCredito", new[] { "SegmentoID" });
            DropColumn("dbo.FluxoLiberacaoLimiteCredito", "SegundoPerfilID");
            DropColumn("dbo.FluxoLiberacaoLimiteCredito", "SegmentoID");
            RenameIndex(table: "dbo.FluxoLiberacaoLimiteCredito", name: "IX_PrimeiroPerfilID", newName: "IX_PerfilID");
            RenameColumn(table: "dbo.FluxoLiberacaoLimiteCredito", name: "PrimeiroPerfilID", newName: "PerfilID");
        }
    }
}
