namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Campo_EmpresaID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FluxoGrupoEconomico", "EmpresaID", c => c.String(maxLength: 1, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FluxoGrupoEconomico", "EmpresaID");
        }
    }
}
