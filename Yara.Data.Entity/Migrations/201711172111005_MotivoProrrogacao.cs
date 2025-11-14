namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MotivoProrrogacao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MotivoProrrogacao",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Nome, unique: true, name: "Index");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.MotivoProrrogacao", "Index");
            DropTable("dbo.MotivoProrrogacao");
        }
    }
}
