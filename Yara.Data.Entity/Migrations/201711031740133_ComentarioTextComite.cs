namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComentarioTextComite : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PropostaLCComite", "Comentario", c => c.String(unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PropostaLCComite", "Comentario", c => c.String(maxLength: 120, unicode: false));
        }
    }
}
