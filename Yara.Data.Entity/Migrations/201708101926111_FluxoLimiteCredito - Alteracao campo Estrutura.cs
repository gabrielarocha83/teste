namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FluxoLimiteCreditoAlteracaocampoEstrutura : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FluxoLimiteCredito", "Estrutura", c => c.String(maxLength: 120, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FluxoLimiteCredito", "Estrutura", c => c.Guid());
        }
    }
}
