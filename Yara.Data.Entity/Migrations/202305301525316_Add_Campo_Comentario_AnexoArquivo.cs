namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Campo_Comentario_AnexoArquivo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnexoArquivo", "Complemento", c => c.String(maxLength: 400, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnexoArquivo", "Complemento");
        }
    }
}
