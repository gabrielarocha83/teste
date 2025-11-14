namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstruturaTituloComentarioCampoTextov3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TituloComentario", "Texto", c => c.String(unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TituloComentario", "Texto", c => c.String(unicode: false));
        }
    }
}
