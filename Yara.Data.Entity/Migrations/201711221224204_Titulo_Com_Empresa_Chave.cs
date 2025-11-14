namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Titulo_Com_Empresa_Chave : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Titulo");
            AddPrimaryKey("dbo.Titulo", new[] { "NumeroDocumento", "Linha", "AnoExercicio", "Empresa" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Titulo");
            AddPrimaryKey("dbo.Titulo", new[] { "NumeroDocumento", "Linha", "AnoExercicio" });
        }
    }
}
