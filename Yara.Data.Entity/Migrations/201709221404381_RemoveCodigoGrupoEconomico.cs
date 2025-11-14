namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCodigoGrupoEconomico : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GrupoEconomico", "ContaCliente_ID", "dbo.ContaCliente");
            DropIndex("dbo.GrupoEconomico", new[] { "ContaCliente_ID" });
            DropColumn("dbo.GrupoEconomico", "ContaCliente_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GrupoEconomico", "ContaCliente_ID", c => c.Guid());
            CreateIndex("dbo.GrupoEconomico", "ContaCliente_ID");
            AddForeignKey("dbo.GrupoEconomico", "ContaCliente_ID", "dbo.ContaCliente", "ID");
        }
    }
}
