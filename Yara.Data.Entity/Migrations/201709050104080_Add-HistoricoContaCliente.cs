namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHistoricoContaCliente : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HistoricoContaCliente",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ContaClienteID = c.Guid(nullable: false),
                        EmpresaID = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        Ano = c.Int(nullable: false),
                        Montante = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MontantePrazo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MontanteAVista = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiasAtraso = c.Int(nullable: false),
                        PesoAtraso = c.Single(nullable: false),
                        PRDias = c.Int(nullable: false),
                        PRPeso = c.Single(nullable: false),
                        REPRDias = c.Int(nullable: false),
                        REPRPeso = c.Single(nullable: false),
                        Pefin = c.Boolean(nullable: false),
                        OpFinanciamento = c.Boolean(nullable: false),
                        Empresas_ID = c.String(maxLength: 1, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .ForeignKey("dbo.Empresa", t => t.Empresas_ID)
                .Index(t => t.ContaClienteID)
                .Index(t => t.Empresas_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HistoricoContaCliente", "Empresas_ID", "dbo.Empresa");
            DropForeignKey("dbo.HistoricoContaCliente", "ContaClienteID", "dbo.ContaCliente");
            DropIndex("dbo.HistoricoContaCliente", new[] { "Empresas_ID" });
            DropIndex("dbo.HistoricoContaCliente", new[] { "ContaClienteID" });
            DropTable("dbo.HistoricoContaCliente");
        }
    }
}
