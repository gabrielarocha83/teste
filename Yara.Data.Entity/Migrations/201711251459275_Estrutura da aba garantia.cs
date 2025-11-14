namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Estruturadaabagarantia : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContaClienteGarantia",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Codigo = c.Int(),
                        ValorGarantia = c.Decimal(precision: 18, scale: 2),
                        ValorGarantido = c.Decimal(precision: 18, scale: 2),
                        Vigencia = c.DateTime(),
                        VigenciaFim = c.DateTime(),
                        DataPagamento = c.DateTime(),
                        Motivo = c.String(unicode: false, storeType: "text"),
                        Observacao = c.String(unicode: false, storeType: "text"),
                        Grau = c.String(maxLength: 120, unicode: false),
                        Matricula = c.String(maxLength: 120, unicode: false),
                        TipoImovel = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        Cidade = c.String(maxLength: 120, unicode: false),
                        Uf = c.String(maxLength: 120, unicode: false),
                        Registro = c.String(unicode: false, storeType: "text"),
                        Laudo = c.String(unicode: false, storeType: "text"),
                        ValorForcada = c.Decimal(precision: 18, scale: 2),
                        ValorMercado = c.Decimal(precision: 18, scale: 2),
                        Produto = c.String(maxLength: 120, unicode: false),
                        Quantidade = c.String(maxLength: 120, unicode: false),
                        Empresa = c.String(maxLength: 120, unicode: false),
                        Area = c.String(maxLength: 120, unicode: false),
                        Monitoramento = c.String(maxLength: 120, unicode: false),
                        EmpresaMonitoramento = c.String(maxLength: 120, unicode: false),
                        OutrasGarantias = c.String(unicode: false, storeType: "text"),
                        TipoGarantia = c.String(maxLength: 3, unicode: false),
                        Status = c.String(maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ContaClienteParticipanteGarantia",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Documento = c.String(maxLength: 120, unicode: false),
                        Nome = c.String(maxLength: 120, unicode: false),
                        Garantido = c.Boolean(nullable: false),
                        ContaClienteGarantiaID = c.Guid(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContaClienteGarantia", t => t.ContaClienteGarantiaID)
                .Index(t => t.ContaClienteGarantiaID);
            
            CreateTable(
                "dbo.ContaClienteResponsavelGarantia",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Documento = c.String(maxLength: 120, unicode: false),
                        Nome = c.String(maxLength: 120, unicode: false),
                        TipoResponsabilidade = c.String(maxLength: 3, unicode: false),
                        ContaClienteGarantiaID = c.Guid(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContaClienteGarantia", t => t.ContaClienteGarantiaID)
                .Index(t => t.ContaClienteGarantiaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContaClienteResponsavelGarantia", "ContaClienteGarantiaID", "dbo.ContaClienteGarantia");
            DropForeignKey("dbo.ContaClienteParticipanteGarantia", "ContaClienteGarantiaID", "dbo.ContaClienteGarantia");
            DropIndex("dbo.ContaClienteResponsavelGarantia", new[] { "ContaClienteGarantiaID" });
            DropIndex("dbo.ContaClienteParticipanteGarantia", new[] { "ContaClienteGarantiaID" });
            DropTable("dbo.ContaClienteResponsavelGarantia");
            DropTable("dbo.ContaClienteParticipanteGarantia");
            DropTable("dbo.ContaClienteGarantia");
        }
    }
}
