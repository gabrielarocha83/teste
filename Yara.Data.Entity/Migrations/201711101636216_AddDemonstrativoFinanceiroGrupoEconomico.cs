namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDemonstrativoFinanceiroGrupoEconomico : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropostaLCGrupoEconomico",
                c => new
                    {
                        PropostaLCID = c.Guid(nullable: false),
                        Documento = c.String(nullable: false, maxLength: 20, unicode: false),
                        PotencialPatrimonial = c.Decimal(precision: 18, scale: 2),
                        DemonstrativoID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PropostaLCID, t.Documento });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PropostaLCGrupoEconomico");
        }
    }
}
