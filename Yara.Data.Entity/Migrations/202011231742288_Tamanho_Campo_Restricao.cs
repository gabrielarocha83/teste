namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tamanho_Campo_Restricao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TribunalJustica",
                c => new
                    {
                        Documento = c.String(nullable: false, maxLength: 120, unicode: false),
                        DataCriacao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Documento);
            
            AlterColumn("dbo.LogDivisaoRemessaLiberacao", "Restricao", c => c.String(maxLength: 1000, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LogDivisaoRemessaLiberacao", "Restricao", c => c.String(maxLength: 8000, unicode: false));
            DropTable("dbo.TribunalJustica");
        }
    }
}
