namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Campos_Titulo_HistoricoV2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Titulo", "RazaoEspecial", c => c.String(maxLength: 120, unicode: false));
            AddColumn("dbo.Titulo", "DataDocumentoCompensacao", c => c.DateTime());
            AddColumn("dbo.Titulo", "AnoExercicioDocumentoCompensacao", c => c.String(maxLength: 120, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Titulo", "AnoExercicioDocumentoCompensacao");
            DropColumn("dbo.Titulo", "DataDocumentoCompensacao");
            DropColumn("dbo.Titulo", "RazaoEspecial");
        }
    }
}
