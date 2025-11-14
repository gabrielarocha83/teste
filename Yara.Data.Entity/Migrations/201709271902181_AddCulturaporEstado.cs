namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCulturaporEstado : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CulturaEstado",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        EstadoID = c.Guid(nullable: false),
                        CulturaID = c.Guid(nullable: false),
                        ProdutividadeMedia = c.Int(nullable: false),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PorcentagemFertilizanteCusto = c.Int(nullable: false),
                        MediaFertilizante = c.Int(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cultura", t => t.CulturaID)
                .ForeignKey("dbo.Estado", t => t.EstadoID)
                .Index(t => t.EstadoID)
                .Index(t => t.CulturaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CulturaEstado", "EstadoID", "dbo.Estado");
            DropForeignKey("dbo.CulturaEstado", "CulturaID", "dbo.Cultura");
            DropIndex("dbo.CulturaEstado", new[] { "CulturaID" });
            DropIndex("dbo.CulturaEstado", new[] { "EstadoID" });
            DropTable("dbo.CulturaEstado");
        }
    }
}
