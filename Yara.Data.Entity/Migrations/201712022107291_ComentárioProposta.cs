namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComentárioProposta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaAlcadaComercial", "Comentario", c => c.String(unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaAlcadaComercial", "Comentario");
        }
    }
}
