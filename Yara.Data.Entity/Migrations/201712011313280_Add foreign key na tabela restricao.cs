namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addforeignkeynatabelarestricao : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.PropostaAlcadaComercialRestricao", "ContaClienteID");
            AddForeignKey("dbo.PropostaAlcadaComercialRestricao", "ContaClienteID", "dbo.ContaCliente", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaAlcadaComercialRestricao", "ContaClienteID", "dbo.ContaCliente");
            DropIndex("dbo.PropostaAlcadaComercialRestricao", new[] { "ContaClienteID" });
        }
    }
}
