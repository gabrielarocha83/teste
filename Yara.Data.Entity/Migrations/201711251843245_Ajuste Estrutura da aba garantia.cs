namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjusteEstruturadaabagarantia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaClienteGarantia", "ContaClienteID", c => c.Guid(nullable: false));
            AddColumn("dbo.ContaClienteParticipanteGarantia", "Ativo", c => c.Boolean(nullable: false));
            AddColumn("dbo.ContaClienteResponsavelGarantia", "Ativo", c => c.Boolean(nullable: false));
            CreateIndex("dbo.ContaClienteGarantia", "ContaClienteID");
            AddForeignKey("dbo.ContaClienteGarantia", "ContaClienteID", "dbo.ContaCliente", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContaClienteGarantia", "ContaClienteID", "dbo.ContaCliente");
            DropIndex("dbo.ContaClienteGarantia", new[] { "ContaClienteID" });
            DropColumn("dbo.ContaClienteResponsavelGarantia", "Ativo");
            DropColumn("dbo.ContaClienteParticipanteGarantia", "Ativo");
            DropColumn("dbo.ContaClienteGarantia", "ContaClienteID");
        }
    }
}
