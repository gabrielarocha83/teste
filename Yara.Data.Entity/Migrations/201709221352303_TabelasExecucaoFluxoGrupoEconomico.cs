namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabelasExecucaoFluxoGrupoEconomico : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContaCliente_GrupoEconomico", "ContaClienteID", "dbo.ContaCliente");
            DropForeignKey("dbo.ContaCliente_GrupoEconomico", "GrupoEconomicoID", "dbo.GrupoEconomico");
            DropIndex("dbo.ContaCliente_GrupoEconomico", new[] { "ContaClienteID" });
            DropIndex("dbo.ContaCliente_GrupoEconomico", new[] { "GrupoEconomicoID" });
            CreateTable(
                "dbo.StatusGrupoEconomicoFluxo",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 3, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                        Nome = c.String(maxLength: 120, unicode: false),
                        Descricao = c.String(maxLength: 120, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.GrupoEconomicoMembro",
                c => new
                    {
                        ContaClienteID = c.Guid(nullable: false),
                        GrupoEconomicoID = c.Guid(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                        StatusGrupoEconomicoFluxoID = c.String(nullable: false, maxLength: 3, unicode: false),
                        MembroPrincipal = c.Boolean(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ContaClienteID, t.GrupoEconomicoID })
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .ForeignKey("dbo.GrupoEconomico", t => t.GrupoEconomicoID)
                .ForeignKey("dbo.StatusGrupoEconomicoFluxo", t => t.StatusGrupoEconomicoFluxoID)
                .Index(t => t.ContaClienteID)
                .Index(t => t.GrupoEconomicoID)
                .Index(t => t.StatusGrupoEconomicoFluxoID);
            
            CreateTable(
                "dbo.LiberacaoGrupoEconomicoFluxo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        SolicitanteGrupoEconomicoID = c.Guid(nullable: false),
                        FluxoGrupoEconomicoID = c.Guid(nullable: false),
                        UsuarioID = c.Guid(nullable: false),
                        StatusGrupoEconomicoFluxoID = c.String(nullable: false, maxLength: 3, unicode: false),
                        GrupoEconomicoID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FluxoGrupoEconomico", t => t.FluxoGrupoEconomicoID)
                .ForeignKey("dbo.GrupoEconomico", t => t.GrupoEconomicoID)
                .ForeignKey("dbo.SolicitanteGrupoEconomico", t => t.SolicitanteGrupoEconomicoID)
                .ForeignKey("dbo.StatusGrupoEconomicoFluxo", t => t.StatusGrupoEconomicoFluxoID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.SolicitanteGrupoEconomicoID)
                .Index(t => t.FluxoGrupoEconomicoID)
                .Index(t => t.UsuarioID)
                .Index(t => t.StatusGrupoEconomicoFluxoID)
                .Index(t => t.GrupoEconomicoID);
            
            CreateTable(
                "dbo.SolicitanteGrupoEconomico",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.GrupoEconomico", "TipoRelacaoGrupoEconomicoID", c => c.Guid(nullable: false));
            AddColumn("dbo.GrupoEconomico", "StatusGrupoEconomicoFluxoID", c => c.Guid(nullable: false));
            AddColumn("dbo.GrupoEconomico", "StatusGrupoEconomicoFluxo_ID", c => c.String(maxLength: 3, unicode: false));
            AddColumn("dbo.GrupoEconomico", "ContaCliente_ID", c => c.Guid());
            CreateIndex("dbo.GrupoEconomico", "TipoRelacaoGrupoEconomicoID");
            CreateIndex("dbo.GrupoEconomico", "StatusGrupoEconomicoFluxo_ID");
            CreateIndex("dbo.GrupoEconomico", "ContaCliente_ID");
            AddForeignKey("dbo.GrupoEconomico", "StatusGrupoEconomicoFluxo_ID", "dbo.StatusGrupoEconomicoFluxo", "ID");
            AddForeignKey("dbo.GrupoEconomico", "TipoRelacaoGrupoEconomicoID", "dbo.TipoRelacaoGrupoEconomico", "ID");
            AddForeignKey("dbo.GrupoEconomico", "ContaCliente_ID", "dbo.ContaCliente", "ID");
            DropColumn("dbo.GrupoEconomico", "CodigoPrincipal");
            DropColumn("dbo.GrupoEconomico", "Tipo");
            DropTable("dbo.ContaCliente_GrupoEconomico");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ContaCliente_GrupoEconomico",
                c => new
                    {
                        ContaClienteID = c.Guid(nullable: false),
                        GrupoEconomicoID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ContaClienteID, t.GrupoEconomicoID });
            
            AddColumn("dbo.GrupoEconomico", "Tipo", c => c.String(nullable: false, maxLength: 120, unicode: false));
            AddColumn("dbo.GrupoEconomico", "CodigoPrincipal", c => c.String(nullable: false, maxLength: 10, unicode: false));
            DropForeignKey("dbo.LiberacaoGrupoEconomicoFluxo", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.LiberacaoGrupoEconomicoFluxo", "StatusGrupoEconomicoFluxoID", "dbo.StatusGrupoEconomicoFluxo");
            DropForeignKey("dbo.LiberacaoGrupoEconomicoFluxo", "SolicitanteGrupoEconomicoID", "dbo.SolicitanteGrupoEconomico");
            DropForeignKey("dbo.LiberacaoGrupoEconomicoFluxo", "GrupoEconomicoID", "dbo.GrupoEconomico");
            DropForeignKey("dbo.LiberacaoGrupoEconomicoFluxo", "FluxoGrupoEconomicoID", "dbo.FluxoGrupoEconomico");
            DropForeignKey("dbo.GrupoEconomicoMembro", "StatusGrupoEconomicoFluxoID", "dbo.StatusGrupoEconomicoFluxo");
            DropForeignKey("dbo.GrupoEconomicoMembro", "GrupoEconomicoID", "dbo.GrupoEconomico");
            DropForeignKey("dbo.GrupoEconomicoMembro", "ContaClienteID", "dbo.ContaCliente");
            DropForeignKey("dbo.GrupoEconomico", "ContaCliente_ID", "dbo.ContaCliente");
            DropForeignKey("dbo.GrupoEconomico", "TipoRelacaoGrupoEconomicoID", "dbo.TipoRelacaoGrupoEconomico");
            DropForeignKey("dbo.GrupoEconomico", "StatusGrupoEconomicoFluxo_ID", "dbo.StatusGrupoEconomicoFluxo");
            DropIndex("dbo.LiberacaoGrupoEconomicoFluxo", new[] { "GrupoEconomicoID" });
            DropIndex("dbo.LiberacaoGrupoEconomicoFluxo", new[] { "StatusGrupoEconomicoFluxoID" });
            DropIndex("dbo.LiberacaoGrupoEconomicoFluxo", new[] { "UsuarioID" });
            DropIndex("dbo.LiberacaoGrupoEconomicoFluxo", new[] { "FluxoGrupoEconomicoID" });
            DropIndex("dbo.LiberacaoGrupoEconomicoFluxo", new[] { "SolicitanteGrupoEconomicoID" });
            DropIndex("dbo.GrupoEconomicoMembro", new[] { "StatusGrupoEconomicoFluxoID" });
            DropIndex("dbo.GrupoEconomicoMembro", new[] { "GrupoEconomicoID" });
            DropIndex("dbo.GrupoEconomicoMembro", new[] { "ContaClienteID" });
            DropIndex("dbo.GrupoEconomico", new[] { "ContaCliente_ID" });
            DropIndex("dbo.GrupoEconomico", new[] { "StatusGrupoEconomicoFluxo_ID" });
            DropIndex("dbo.GrupoEconomico", new[] { "TipoRelacaoGrupoEconomicoID" });
            DropColumn("dbo.GrupoEconomico", "ContaCliente_ID");
            DropColumn("dbo.GrupoEconomico", "StatusGrupoEconomicoFluxo_ID");
            DropColumn("dbo.GrupoEconomico", "StatusGrupoEconomicoFluxoID");
            DropColumn("dbo.GrupoEconomico", "TipoRelacaoGrupoEconomicoID");
            DropTable("dbo.SolicitanteGrupoEconomico");
            DropTable("dbo.LiberacaoGrupoEconomicoFluxo");
            DropTable("dbo.GrupoEconomicoMembro");
            DropTable("dbo.StatusGrupoEconomicoFluxo");
            CreateIndex("dbo.ContaCliente_GrupoEconomico", "GrupoEconomicoID");
            CreateIndex("dbo.ContaCliente_GrupoEconomico", "ContaClienteID");
            AddForeignKey("dbo.ContaCliente_GrupoEconomico", "GrupoEconomicoID", "dbo.GrupoEconomico", "ID");
            AddForeignKey("dbo.ContaCliente_GrupoEconomico", "ContaClienteID", "dbo.ContaCliente", "ID");
        }
    }
}
