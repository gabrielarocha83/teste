namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstruturaTituloComentarioCampoTextov4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Titulo", "TextoComentario", c => c.String(maxLength: 120, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Titulo", "TextoComentario");
        }
    }
}
