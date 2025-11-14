namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdemVendaBloqueioManual : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StatusOrdemVendas",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Status = c.String(maxLength: 120, unicode: false),
                        Descricao = c.String(maxLength: 120, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StatusOrdemVendas");
        }
    }
}
