namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SolicitantePropostaLCComite : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SolicitanteFluxoComite",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UsuarioID = c.Guid(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.UsuarioID);
            
            AddColumn("dbo.PropostaLCComite", "PropostaLcComiteSolicitanteID", c => c.Guid(nullable: false));
            CreateIndex("dbo.PropostaLCComite", "PropostaLcComiteSolicitanteID");
            AddForeignKey("dbo.PropostaLCComite", "PropostaLcComiteSolicitanteID", "dbo.SolicitanteFluxoComite", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaLCComite", "PropostaLcComiteSolicitanteID", "dbo.SolicitanteFluxoComite");
            DropForeignKey("dbo.SolicitanteFluxoComite", "UsuarioID", "dbo.Usuario");
            DropIndex("dbo.SolicitanteFluxoComite", new[] { "UsuarioID" });
            DropIndex("dbo.PropostaLCComite", new[] { "PropostaLcComiteSolicitanteID" });
            DropColumn("dbo.PropostaLCComite", "PropostaLcComiteSolicitanteID");
            DropTable("dbo.SolicitanteFluxoComite");
        }
    }
}
