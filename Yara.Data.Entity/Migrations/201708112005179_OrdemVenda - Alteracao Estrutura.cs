namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdemVendaAlteracaoEstrutura : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.OrdemVenda");
            CreateTable(
                "dbo.DivisaoRemessa",
                c => new
                    {
                        Divisao = c.Int(nullable: false, identity: false),
                        OrdemVendaNumero = c.String(nullable: false, maxLength: 10, unicode: false),
                        ItemOrdemVendaItem = c.Int(nullable: false),
                        QtdPedida = c.Decimal(nullable: false, precision: 15, scale: 3),
                        QtdConfirmada = c.Decimal(nullable: false, precision: 15, scale: 3),
                        UnidadeMedida = c.String(nullable: false, maxLength: 3, unicode: false),
                        DataOrganizacao = c.DateTime(nullable: false),
                        DataPreparacao = c.DateTime(nullable: false),
                        DataCarregamento = c.DateTime(nullable: false),
                        DataSaida = c.DateTime(nullable: false),
                        Status = c.String(nullable: false, maxLength: 2, unicode: false),
                        Motivo = c.String(nullable: false, maxLength: 3, unicode: false),
                        Bloqueio = c.String(nullable: false, maxLength: 2, unicode: false),
                    })
                .PrimaryKey(t => t.Divisao)
                .ForeignKey("dbo.ItemOrdemVenda", t => t.ItemOrdemVendaItem)
                .ForeignKey("dbo.OrdemVenda", t => t.OrdemVendaNumero)
                .Index(t => t.OrdemVendaNumero)
                .Index(t => t.ItemOrdemVendaItem);
            
            CreateTable(
                "dbo.ItemOrdemVenda",
                c => new
                    {
                        Item = c.Int(nullable: false, identity: false),
                        OrdemVendaNumero = c.String(nullable: false, maxLength: 10, unicode: false),
                        Material = c.String(nullable: false, maxLength: 18, unicode: false),
                        Centro = c.String(nullable: false, maxLength: 4, unicode: false),
                        Deposito = c.String(nullable: false, maxLength: 4, unicode: false),
                        CondPagto = c.String(nullable: false, maxLength: 4, fixedLength: true, unicode: false),
                        CondFrete = c.String(nullable: false, maxLength: 3, fixedLength: true, unicode: false),
                        Moeda = c.String(nullable: false, maxLength: 4, unicode: false),
                        TaxaCambio = c.Decimal(nullable: false, precision: 9, scale: 5),
                        DataEfetivaFixa = c.DateTime(nullable: false),
                        MotivoRecus = c.String(nullable: false, maxLength: 2, unicode: false),
                        QtdPedida = c.Decimal(nullable: false, precision: 15, scale: 3),
                        QtdEntregue = c.Decimal(nullable: false, precision: 15, scale: 3),
                        UnidadeMedida = c.String(nullable: false, maxLength: 3, unicode: false),
                        ValorUnitario = c.Decimal(nullable: false, precision: 13, scale: 2),
                    })
                .PrimaryKey(t => t.Item)
                .ForeignKey("dbo.OrdemVenda", t => t.OrdemVendaNumero)
                .Index(t => t.OrdemVendaNumero);
            
            AddColumn("dbo.OrdemVenda", "Numero", c => c.String(nullable: false, maxLength: 10, unicode: false));
            AddColumn("dbo.OrdemVenda", "Tipo", c => c.String(nullable: false, maxLength: 4, fixedLength: true, unicode: false));
            AddColumn("dbo.OrdemVenda", "Canal", c => c.String(nullable: false, maxLength: 2, fixedLength: true, unicode: false));
            AddColumn("dbo.OrdemVenda", "Setor", c => c.String(nullable: false, maxLength: 2, fixedLength: true, unicode: false));
            AddColumn("dbo.OrdemVenda", "OrgVendas", c => c.String(nullable: false, maxLength: 4, fixedLength: true, unicode: false));
            AddColumn("dbo.OrdemVenda", "CodigoCtc", c => c.String(nullable: false, maxLength: 3, fixedLength: true, unicode: false));
            AddColumn("dbo.OrdemVenda", "CodigoGc", c => c.String(nullable: false, maxLength: 4, fixedLength: true, unicode: false));
            AddColumn("dbo.OrdemVenda", "Emissor", c => c.String(nullable: false, maxLength: 10, unicode: false));
            AddColumn("dbo.OrdemVenda", "Pagador", c => c.String(nullable: false, maxLength: 10, unicode: false));
            AddColumn("dbo.OrdemVenda", "Representante", c => c.String(nullable: false, maxLength: 10, unicode: false));
            AddColumn("dbo.OrdemVenda", "CondPagto", c => c.String(nullable: false, maxLength: 4, fixedLength: true, unicode: false));
            AddColumn("dbo.OrdemVenda", "CondFrete", c => c.String(nullable: false, maxLength: 3, fixedLength: true, unicode: false));
            AddColumn("dbo.OrdemVenda", "PedidoCliente", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AddColumn("dbo.OrdemVenda", "Moeda", c => c.String(nullable: false, maxLength: 5, unicode: false));
            AddColumn("dbo.OrdemVenda", "TaxaCambio", c => c.Decimal(nullable: false, precision: 9, scale: 5));
            AddColumn("dbo.OrdemVenda", "Cultura", c => c.String(nullable: false, maxLength: 3, unicode: false));
            AddColumn("dbo.OrdemVenda", "BloqueioRemessa", c => c.String(nullable: false, maxLength: 2, unicode: false));
            AddColumn("dbo.OrdemVenda", "DataEfetivaFixa", c => c.DateTime(nullable: false));
            AddColumn("dbo.OrdemVenda", "DataEmissao", c => c.DateTime(nullable: false));
            AddColumn("dbo.OrdemVenda", "DataModificacao", c => c.DateTime(nullable: false));
            AddColumn("dbo.OrdemVenda", "UltimaAtualizacao", c => c.DateTime(nullable: false));
            AddPrimaryKey("dbo.OrdemVenda", "Numero");
            DropColumn("dbo.OrdemVenda", "ID");
            DropColumn("dbo.OrdemVenda", "ClienteID");
            DropColumn("dbo.OrdemVenda", "NumeroOrdem");
            DropColumn("dbo.OrdemVenda", "NumeroNotaFiscal");
            DropColumn("dbo.OrdemVenda", "Titulo");
            DropColumn("dbo.OrdemVenda", "TipoTitulo");
            DropColumn("dbo.OrdemVenda", "ItemOrdem");
            DropColumn("dbo.OrdemVenda", "Vencimento");
            DropColumn("dbo.OrdemVenda", "MoedaDocumento");
            DropColumn("dbo.OrdemVenda", "ValorMoedaDocumento");
            DropColumn("dbo.OrdemVenda", "ValorReais");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrdemVenda", "ValorReais", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.OrdemVenda", "ValorMoedaDocumento", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.OrdemVenda", "MoedaDocumento", c => c.String(nullable: false, maxLength: 120, unicode: false));
            AddColumn("dbo.OrdemVenda", "Vencimento", c => c.DateTime(nullable: false));
            AddColumn("dbo.OrdemVenda", "ItemOrdem", c => c.String(maxLength: 120, unicode: false));
            AddColumn("dbo.OrdemVenda", "TipoTitulo", c => c.String(nullable: false, maxLength: 120, unicode: false));
            AddColumn("dbo.OrdemVenda", "Titulo", c => c.Int(nullable: false));
            AddColumn("dbo.OrdemVenda", "NumeroNotaFiscal", c => c.Int(nullable: false));
            AddColumn("dbo.OrdemVenda", "NumeroOrdem", c => c.Int(nullable: false));
            AddColumn("dbo.OrdemVenda", "ClienteID", c => c.Guid(nullable: false));
            AddColumn("dbo.OrdemVenda", "ID", c => c.Guid(nullable: false));
            DropForeignKey("dbo.DivisaoRemessa", "OrdemVendaNumero", "dbo.OrdemVenda");
            DropForeignKey("dbo.DivisaoRemessa", "ItemOrdemVendaItem", "dbo.ItemOrdemVenda");
            DropForeignKey("dbo.ItemOrdemVenda", "OrdemVendaNumero", "dbo.OrdemVenda");
            DropIndex("dbo.ItemOrdemVenda", new[] { "OrdemVendaNumero" });
            DropIndex("dbo.DivisaoRemessa", new[] { "ItemOrdemVendaItem" });
            DropIndex("dbo.DivisaoRemessa", new[] { "OrdemVendaNumero" });
            DropPrimaryKey("dbo.OrdemVenda");
            DropColumn("dbo.OrdemVenda", "UltimaAtualizacao");
            DropColumn("dbo.OrdemVenda", "DataModificacao");
            DropColumn("dbo.OrdemVenda", "DataEmissao");
            DropColumn("dbo.OrdemVenda", "DataEfetivaFixa");
            DropColumn("dbo.OrdemVenda", "BloqueioRemessa");
            DropColumn("dbo.OrdemVenda", "Cultura");
            DropColumn("dbo.OrdemVenda", "TaxaCambio");
            DropColumn("dbo.OrdemVenda", "Moeda");
            DropColumn("dbo.OrdemVenda", "PedidoCliente");
            DropColumn("dbo.OrdemVenda", "CondFrete");
            DropColumn("dbo.OrdemVenda", "CondPagto");
            DropColumn("dbo.OrdemVenda", "Representante");
            DropColumn("dbo.OrdemVenda", "Pagador");
            DropColumn("dbo.OrdemVenda", "Emissor");
            DropColumn("dbo.OrdemVenda", "CodigoGc");
            DropColumn("dbo.OrdemVenda", "CodigoCtc");
            DropColumn("dbo.OrdemVenda", "OrgVendas");
            DropColumn("dbo.OrdemVenda", "Setor");
            DropColumn("dbo.OrdemVenda", "Canal");
            DropColumn("dbo.OrdemVenda", "Tipo");
            DropColumn("dbo.OrdemVenda", "Numero");
            DropTable("dbo.ItemOrdemVenda");
            DropTable("dbo.DivisaoRemessa");
            AddPrimaryKey("dbo.OrdemVenda", "ID");
        }
    }
}
