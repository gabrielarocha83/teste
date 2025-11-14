namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropostaLCDemonstrativo1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropostaLCDemonstrativo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        NomeArquivo = c.String(maxLength: 255, unicode: false),
                        Html = c.String(unicode: false, storeType: "text"),
                        Tipo = c.String(maxLength: 2, unicode: false),
                        DataUpload = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PropostaLCDemonstrativo");
        }
    }
}
