namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Campos_Titulo_HistoricoV2_Correcao : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Titulo", "RazaoEspecial", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
            AlterColumn("dbo.Titulo", "AnoExercicioDocumentoCompensacao", c => c.String(maxLength: 4, fixedLength: true, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Titulo", "AnoExercicioDocumentoCompensacao", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.Titulo", "RazaoEspecial", c => c.String(maxLength: 120, unicode: false));
        }
    }
}
