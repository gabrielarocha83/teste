namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropostaLCAdicionalStatusComite_Estrutura : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropostaLCAdicionalStatusComite",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 2, unicode: false),
                        Nome = c.String(maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PropostaLCAdicionalStatusComite");
        }
    }
}
