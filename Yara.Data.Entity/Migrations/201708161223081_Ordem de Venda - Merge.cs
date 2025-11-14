namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdemdeVendaMerge : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrdemVendaFluxo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        SolicitanteFluxoID = c.Guid(nullable: false),
                        FluxoLimiteCreditoID = c.Guid(nullable: false),
                        DivisaoRemessaDivisao = c.Int(nullable: false),
                        UsuarioID = c.Guid(nullable: false),
                        Status = c.String(maxLength: 2, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SolicitanteFluxo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SolicitanteFluxo");
            DropTable("dbo.OrdemVendaFluxo");
        }
    }
}
