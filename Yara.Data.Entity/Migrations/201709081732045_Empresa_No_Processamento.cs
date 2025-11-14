namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Empresa_No_Processamento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProcessamentoCarteira", "EmpresaID", c => c.String(nullable: false, maxLength: 1, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProcessamentoCarteira", "EmpresaID");
        }
    }
}
