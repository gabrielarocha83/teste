namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TamanhoDescricaoConceito : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContaClienteFinanceiro", "DescricaoConceito", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContaClienteFinanceiro", "DescricaoConceito", c => c.String(maxLength: 120, unicode: false));
        }
    }
}
