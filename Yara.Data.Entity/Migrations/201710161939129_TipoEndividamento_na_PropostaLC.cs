namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TipoEndividamento_na_PropostaLC : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropostaLCTipoEndividamento",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        TipoEndividamentoID = c.Guid(),
                        CurtoPrazo = c.Decimal(precision: 18, scale: 2),
                        LongoPrazo = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .ForeignKey("dbo.TipoEndividamento", t => t.TipoEndividamentoID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.TipoEndividamentoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaLCTipoEndividamento", "TipoEndividamentoID", "dbo.TipoEndividamento");
            DropForeignKey("dbo.PropostaLCTipoEndividamento", "PropostaLCID", "dbo.PropostaLC");
            DropIndex("dbo.PropostaLCTipoEndividamento", new[] { "TipoEndividamentoID" });
            DropIndex("dbo.PropostaLCTipoEndividamento", new[] { "PropostaLCID" });
            DropTable("dbo.PropostaLCTipoEndividamento");
        }
    }
}
