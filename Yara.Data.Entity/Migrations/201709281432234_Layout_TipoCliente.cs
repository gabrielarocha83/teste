namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Layout_TipoCliente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TipoCliente", "LayoutProposta", c => c.Int(nullable: false));
            AlterColumn("dbo.TipoCliente", "Nome", c => c.String(nullable: false, maxLength: 50, unicode: false));
            DropColumn("dbo.TipoCliente", "Descricao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TipoCliente", "Descricao", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.TipoCliente", "Nome", c => c.String(nullable: false, maxLength: 120, unicode: false));
            DropColumn("dbo.TipoCliente", "LayoutProposta");
        }
    }
}
