namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Campos_HistoricoV2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HistoricoContaCliente", "DiasMaiorAtraso", c => c.Single(nullable: false));
            AddColumn("dbo.HistoricoContaCliente", "PesoMaiorAtraso", c => c.Single(nullable: false));
            AlterColumn("dbo.HistoricoContaCliente", "DiasAtraso", c => c.Single(nullable: false));
            DropColumn("dbo.HistoricoContaCliente", "PRPeso");
            DropColumn("dbo.HistoricoContaCliente", "REPRDias");
            DropColumn("dbo.HistoricoContaCliente", "REPRPeso");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HistoricoContaCliente", "REPRPeso", c => c.Single(nullable: false));
            AddColumn("dbo.HistoricoContaCliente", "REPRDias", c => c.Int(nullable: false));
            AddColumn("dbo.HistoricoContaCliente", "PRPeso", c => c.Single(nullable: false));
            AlterColumn("dbo.HistoricoContaCliente", "DiasAtraso", c => c.Int(nullable: false));
            DropColumn("dbo.HistoricoContaCliente", "PesoMaiorAtraso");
            DropColumn("dbo.HistoricoContaCliente", "DiasMaiorAtraso");
        }
    }
}
