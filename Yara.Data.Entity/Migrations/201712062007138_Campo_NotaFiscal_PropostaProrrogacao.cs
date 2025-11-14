namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Campo_NotaFiscal_PropostaProrrogacao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaProrrogacaoTitulo", "NotaFiscal", c => c.String(maxLength: 120, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaProrrogacaoTitulo", "NotaFiscal");
        }
    }
}
