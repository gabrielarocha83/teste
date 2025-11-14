namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cambio : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cambio",
                c => new
                    {
                        InicioValidade = c.DateTime(nullable: false),
                        MoedaDe = c.String(nullable: false, maxLength: 5, unicode: false),
                        MoedaPara = c.String(nullable: false, maxLength: 5, unicode: false),
                        FatorDe = c.Int(nullable: false),
                        FatorPara = c.Int(nullable: false),
                        Taxa = c.Decimal(nullable: false, precision: 9, scale: 5),
                    })
                .PrimaryKey(t => new { t.InicioValidade, t.MoedaDe, t.MoedaPara });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cambio");
        }
    }
}
