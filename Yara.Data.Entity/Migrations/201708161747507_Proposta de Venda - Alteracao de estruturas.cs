namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropostadeVendaAlteracaodeestruturas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropostaLCStatus",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.PropostaLC", "PropostaLCStatusID", c => c.Guid(nullable: false));
            CreateIndex("dbo.PropostaLC", "PropostaLCStatusID");
            AddForeignKey("dbo.PropostaLC", "PropostaLCStatusID", "dbo.PropostaLCStatus", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaLC", "PropostaLCStatusID", "dbo.PropostaLCStatus");
            DropIndex("dbo.PropostaLC", new[] { "PropostaLCStatusID" });
            DropColumn("dbo.PropostaLC", "PropostaLCStatusID");
            DropTable("dbo.PropostaLCStatus");
        }
    }
}
