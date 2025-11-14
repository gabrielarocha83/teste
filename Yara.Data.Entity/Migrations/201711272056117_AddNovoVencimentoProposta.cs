namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNovoVencimentoProposta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaProrrogacaoTitulo", "NovoVencimento", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaProrrogacaoTitulo", "NovoVencimento");
        }
    }
}
