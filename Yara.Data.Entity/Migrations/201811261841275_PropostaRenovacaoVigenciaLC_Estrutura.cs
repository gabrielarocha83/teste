namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropostaRenovacaoVigenciaLC_Estrutura : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropostaRenovacaoVigenciaLC",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        NumeroInternoProposta = c.Int(nullable: false),
                        PropostaLCStatusID = c.String(maxLength: 2, unicode: false),
                        ResponsavelID = c.Guid(nullable: false),
                        ResponsavelNome = c.String(maxLength: 120, unicode: false),
                        Montante = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataNovaVigencia = c.DateTime(nullable: false),
                        EmpresaID = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Empresa", t => t.EmpresaID)
                .ForeignKey("dbo.PropostaLCStatus", t => t.PropostaLCStatusID)
                .ForeignKey("dbo.Usuario", t => t.ResponsavelID)
                .Index(t => t.PropostaLCStatusID)
                .Index(t => t.ResponsavelID)
                .Index(t => t.EmpresaID);
            
            CreateTable(
                "dbo.PropostaRenovacaoVigenciaLCCliente",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ContaClienteID = c.Guid(nullable: false),
                        NomeCliente = c.String(nullable: false, maxLength: 120, unicode: false),
                        CodigoCliente = c.String(nullable: false, maxLength: 10, unicode: false),
                        Apelido = c.String(maxLength: 120, unicode: false),
                        Documento = c.String(maxLength: 120, unicode: false),
                        TipoCliente = c.String(maxLength: 50, unicode: false),
                        NomeGrupo = c.String(maxLength: 120, unicode: false),
                        ClassificacaoGrupo = c.String(maxLength: 120, unicode: false),
                        DataVigenciaLC = c.DateTime(),
                        Rating = c.String(maxLength: 120, unicode: false),
                        ValorLC = c.Decimal(precision: 18, scale: 2),
                        Top10 = c.Boolean(),
                        DataConsultaSerasa = c.DateTime(),
                        RestricaoSerasa = c.Int(),
                        RestricaoYara = c.Boolean(),
                        RestricaoSerasaGrupo = c.Boolean(),
                        RestricaoYaraGrupo = c.Boolean(),
                        CodigoPropostaLC = c.String(maxLength: 12, unicode: false),
                        ValorPropostaLC = c.Decimal(precision: 18, scale: 2),
                        PropostaLCStatus = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        CodigoPropostaAC = c.String(maxLength: 12, unicode: false),
                        ValorPropostaAC = c.Decimal(precision: 18, scale: 2),
                        PropostaACStatus = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        DataUltimaCompra = c.DateTime(),
                        DataValidadeGarantia = c.DateTime(),
                        Representante = c.String(maxLength: 120, unicode: false),
                        CTC = c.String(maxLength: 120, unicode: false),
                        GC = c.String(maxLength: 120, unicode: false),
                        Diretoria = c.String(maxLength: 120, unicode: false),
                        Analista = c.String(maxLength: 120, unicode: false),
                        Apto = c.Boolean(nullable: false),
                        PropostaRenovacaoVigenciaLCID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaRenovacaoVigenciaLC", t => t.PropostaRenovacaoVigenciaLCID)
                .Index(t => t.PropostaRenovacaoVigenciaLCID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaRenovacaoVigenciaLC", "ResponsavelID", "dbo.Usuario");
            DropForeignKey("dbo.PropostaRenovacaoVigenciaLC", "PropostaLCStatusID", "dbo.PropostaLCStatus");
            DropForeignKey("dbo.PropostaRenovacaoVigenciaLC", "EmpresaID", "dbo.Empresa");
            DropForeignKey("dbo.PropostaRenovacaoVigenciaLCCliente", "PropostaRenovacaoVigenciaLCID", "dbo.PropostaRenovacaoVigenciaLC");
            DropIndex("dbo.PropostaRenovacaoVigenciaLCCliente", new[] { "PropostaRenovacaoVigenciaLCID" });
            DropIndex("dbo.PropostaRenovacaoVigenciaLC", new[] { "EmpresaID" });
            DropIndex("dbo.PropostaRenovacaoVigenciaLC", new[] { "ResponsavelID" });
            DropIndex("dbo.PropostaRenovacaoVigenciaLC", new[] { "PropostaLCStatusID" });
            DropTable("dbo.PropostaRenovacaoVigenciaLCCliente");
            DropTable("dbo.PropostaRenovacaoVigenciaLC");
        }
    }
}
