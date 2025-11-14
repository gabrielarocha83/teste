namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdemdeVendaAlteracaodeestrutura : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemOrdemVenda", "MotivoRecusa", c => c.String(maxLength: 2, unicode: false));
            AlterColumn("dbo.DivisaoRemessa", "UnidadeMedida", c => c.String(maxLength: 3, unicode: false));
            AlterColumn("dbo.DivisaoRemessa", "Motivo", c => c.String(maxLength: 3, unicode: false));
            AlterColumn("dbo.DivisaoRemessa", "Bloqueio", c => c.String(maxLength: 2, unicode: false));
            AlterColumn("dbo.ItemOrdemVenda", "Centro", c => c.String(maxLength: 4, unicode: false));
            AlterColumn("dbo.ItemOrdemVenda", "CondPagto", c => c.String(maxLength: 4, fixedLength: true, unicode: false));
            AlterColumn("dbo.ItemOrdemVenda", "CondFrete", c => c.String(maxLength: 3, fixedLength: true, unicode: false));
            AlterColumn("dbo.ItemOrdemVenda", "Moeda", c => c.String(maxLength: 4, unicode: false));
            AlterColumn("dbo.ItemOrdemVenda", "UnidadeMedida", c => c.String(maxLength: 3, unicode: false));
            AlterColumn("dbo.OrdemVenda", "CodigoCtc", c => c.String(maxLength: 3, fixedLength: true, unicode: false));
            AlterColumn("dbo.OrdemVenda", "CodigoGc", c => c.String(maxLength: 4, fixedLength: true, unicode: false));
            AlterColumn("dbo.OrdemVenda", "Pagador", c => c.String(maxLength: 10, unicode: false));
            AlterColumn("dbo.OrdemVenda", "Representante", c => c.String(maxLength: 10, unicode: false));
            AlterColumn("dbo.OrdemVenda", "CondPagto", c => c.String(maxLength: 4, fixedLength: true, unicode: false));
            AlterColumn("dbo.OrdemVenda", "CondFrete", c => c.String(maxLength: 3, fixedLength: true, unicode: false));
            AlterColumn("dbo.OrdemVenda", "Moeda", c => c.String(maxLength: 5, unicode: false));
            AlterColumn("dbo.OrdemVenda", "Cultura", c => c.String(maxLength: 3, unicode: false));
            AlterColumn("dbo.OrdemVenda", "BloqueioRemessa", c => c.String(maxLength: 2, unicode: false));
            AlterColumn("dbo.OrdemVenda", "BloqueioFaturamento", c => c.String(maxLength: 2, unicode: false));
            DropColumn("dbo.ItemOrdemVenda", "MotivoRecus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemOrdemVenda", "MotivoRecus", c => c.String(nullable: false, maxLength: 2, unicode: false));
            AlterColumn("dbo.OrdemVenda", "BloqueioFaturamento", c => c.String(nullable: false, maxLength: 2, unicode: false));
            AlterColumn("dbo.OrdemVenda", "BloqueioRemessa", c => c.String(nullable: false, maxLength: 2, unicode: false));
            AlterColumn("dbo.OrdemVenda", "Cultura", c => c.String(nullable: false, maxLength: 3, unicode: false));
            AlterColumn("dbo.OrdemVenda", "Moeda", c => c.String(nullable: false, maxLength: 5, unicode: false));
            AlterColumn("dbo.OrdemVenda", "CondFrete", c => c.String(nullable: false, maxLength: 3, fixedLength: true, unicode: false));
            AlterColumn("dbo.OrdemVenda", "CondPagto", c => c.String(nullable: false, maxLength: 4, fixedLength: true, unicode: false));
            AlterColumn("dbo.OrdemVenda", "Representante", c => c.String(nullable: false, maxLength: 10, unicode: false));
            AlterColumn("dbo.OrdemVenda", "Pagador", c => c.String(nullable: false, maxLength: 10, unicode: false));
            AlterColumn("dbo.OrdemVenda", "CodigoGc", c => c.String(nullable: false, maxLength: 4, fixedLength: true, unicode: false));
            AlterColumn("dbo.OrdemVenda", "CodigoCtc", c => c.String(nullable: false, maxLength: 3, fixedLength: true, unicode: false));
            AlterColumn("dbo.ItemOrdemVenda", "UnidadeMedida", c => c.String(nullable: false, maxLength: 3, unicode: false));
            AlterColumn("dbo.ItemOrdemVenda", "Moeda", c => c.String(nullable: false, maxLength: 4, unicode: false));
            AlterColumn("dbo.ItemOrdemVenda", "CondFrete", c => c.String(nullable: false, maxLength: 3, fixedLength: true, unicode: false));
            AlterColumn("dbo.ItemOrdemVenda", "CondPagto", c => c.String(nullable: false, maxLength: 4, fixedLength: true, unicode: false));
            AlterColumn("dbo.ItemOrdemVenda", "Centro", c => c.String(nullable: false, maxLength: 4, unicode: false));
            AlterColumn("dbo.DivisaoRemessa", "Bloqueio", c => c.String(nullable: false, maxLength: 2, unicode: false));
            AlterColumn("dbo.DivisaoRemessa", "Motivo", c => c.String(nullable: false, maxLength: 3, unicode: false));
            AlterColumn("dbo.DivisaoRemessa", "UnidadeMedida", c => c.String(nullable: false, maxLength: 3, unicode: false));
            DropColumn("dbo.ItemOrdemVenda", "MotivoRecusa");
        }
    }
}
