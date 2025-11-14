namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddcampoContaClienteIdnatabelaAnexoArquivov2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.AnexoArquivo", "ContaClienteID");
            AddForeignKey("dbo.AnexoArquivo", "ContaClienteID", "dbo.ContaCliente", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnexoArquivo", "ContaClienteID", "dbo.ContaCliente");
            DropIndex("dbo.AnexoArquivo", new[] { "ContaClienteID" });
        }
    }
}
