namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AcompanhamentoPropostaAbono : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropostaAbonoAcompanhamento",
                c => new
                    {
                        PropostaAbonoID = c.Guid(nullable: false),
                        UsuarioID = c.Guid(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.PropostaAbonoID, t.UsuarioID })
                .ForeignKey("dbo.PropostaAbono", t => t.PropostaAbonoID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.PropostaAbonoID)
                .Index(t => t.UsuarioID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaAbonoAcompanhamento", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.PropostaAbonoAcompanhamento", "PropostaAbonoID", "dbo.PropostaAbono");
            DropIndex("dbo.PropostaAbonoAcompanhamento", new[] { "UsuarioID" });
            DropIndex("dbo.PropostaAbonoAcompanhamento", new[] { "PropostaAbonoID" });
            DropTable("dbo.PropostaAbonoAcompanhamento");
        }
    }
}
