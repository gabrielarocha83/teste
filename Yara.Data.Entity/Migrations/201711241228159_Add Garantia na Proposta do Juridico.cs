namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGarantianaPropostadoJuridico : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropostaJuridicoGarantia",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaJuridicoID = c.Guid(nullable: false),
                        TipoGarantiaID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaJuridico", t => t.PropostaJuridicoID)
                .ForeignKey("dbo.TipoGarantia", t => t.TipoGarantiaID)
                .Index(t => t.PropostaJuridicoID)
                .Index(t => t.TipoGarantiaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaJuridicoGarantia", "TipoGarantiaID", "dbo.TipoGarantia");
            DropForeignKey("dbo.PropostaJuridicoGarantia", "PropostaJuridicoID", "dbo.PropostaJuridico");
            DropIndex("dbo.PropostaJuridicoGarantia", new[] { "TipoGarantiaID" });
            DropIndex("dbo.PropostaJuridicoGarantia", new[] { "PropostaJuridicoID" });
            DropTable("dbo.PropostaJuridicoGarantia");
        }
    }
}
