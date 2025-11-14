namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Estrutura_Bloqueio_Carregamento : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BloqueioLiberacaoCarregamento",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ProcessamentoCarteiraID = c.Guid(nullable: false),
                        Divisao = c.Int(nullable: false),
                        Item = c.Int(nullable: false),
                        Numero = c.String(maxLength: 120, unicode: false),
                        EnviadoSAP = c.Boolean(nullable: false),
                        Bloqueada = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProcessamentoCarteira", t => t.ProcessamentoCarteiraID)
                .Index(t => t.ProcessamentoCarteiraID);
            
            AddColumn("dbo.DivisaoRemessa", "BloqueioCarregamento", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BloqueioLiberacaoCarregamento", "ProcessamentoCarteiraID", "dbo.ProcessamentoCarteira");
            DropIndex("dbo.BloqueioLiberacaoCarregamento", new[] { "ProcessamentoCarteiraID" });
            DropColumn("dbo.DivisaoRemessa", "BloqueioCarregamento");
            DropTable("dbo.BloqueioLiberacaoCarregamento");
        }
    }
}
