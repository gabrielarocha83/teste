namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddcampoContaClienteIdnatabelaAnexoArquivo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnexoArquivo", "ContaClienteID", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnexoArquivo", "ContaClienteID");
        }
    }
}
