namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProcessamentoCarteira : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProcessamentoCarteira",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Cliente = c.String(nullable: false, maxLength: 10, unicode: false),
                        DataHora = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Motivo = c.String(maxLength: 255, unicode: false),
                        Detalhes = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProcessamentoCarteira");
        }
    }
}
