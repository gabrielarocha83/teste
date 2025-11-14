namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropostaLCAdicionalAcompanhamento_Estrutura : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropostaLCAdicionalAcompanhamento",
                c => new
                    {
                        PropostaLCAdicionalID = c.Guid(nullable: false),
                        UsuarioID = c.Guid(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.PropostaLCAdicionalID, t.UsuarioID })
                .ForeignKey("dbo.PropostaLCAdicional", t => t.PropostaLCAdicionalID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.PropostaLCAdicionalID)
                .Index(t => t.UsuarioID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaLCAdicionalAcompanhamento", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.PropostaLCAdicionalAcompanhamento", "PropostaLCAdicionalID", "dbo.PropostaLCAdicional");
            DropIndex("dbo.PropostaLCAdicionalAcompanhamento", new[] { "UsuarioID" });
            DropIndex("dbo.PropostaLCAdicionalAcompanhamento", new[] { "PropostaLCAdicionalID" });
            DropTable("dbo.PropostaLCAdicionalAcompanhamento");
        }
    }
}
