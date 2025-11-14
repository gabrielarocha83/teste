namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabelaLiberacaoCarteira : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LiberacaoCarteira",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ProcessamentoCarteiraID = c.Guid(nullable: false),
                        Divisao = c.Int(nullable: false),
                        Item = c.Int(nullable: false),
                        Numero = c.String(maxLength: 120, unicode: false),
                        EnviadoSAP = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProcessamentoCarteira", t => t.ProcessamentoCarteiraID)
                .Index(t => t.ProcessamentoCarteiraID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LiberacaoCarteira", "ProcessamentoCarteiraID", "dbo.ProcessamentoCarteira");
            DropIndex("dbo.LiberacaoCarteira", new[] { "ProcessamentoCarteiraID" });
            DropTable("dbo.LiberacaoCarteira");
        }
    }
}
