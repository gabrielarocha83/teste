namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_Campo_DescricaoConceitoAnterior : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContaClienteFinanceiro", "DescricaoConceitoAnterior", c => c.String(maxLength: 8000, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContaClienteFinanceiro", "DescricaoConceitoAnterior", c => c.String(maxLength: 120, unicode: false));
        }
    }
}
