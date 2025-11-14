namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FluxoLimiteCreditoAlteracaocampoGuid : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FluxoLimiteCredito", "Usuario", c => c.Guid());
            AlterColumn("dbo.FluxoLimiteCredito", "Grupo", c => c.Guid());
            AlterColumn("dbo.FluxoLimiteCredito", "Estrutura", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FluxoLimiteCredito", "Estrutura", c => c.Guid(nullable: false));
            AlterColumn("dbo.FluxoLimiteCredito", "Grupo", c => c.Guid(nullable: false));
            AlterColumn("dbo.FluxoLimiteCredito", "Usuario", c => c.Guid(nullable: false));
        }
    }
}
