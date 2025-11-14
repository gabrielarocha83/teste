namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTituloskeyprorrogacao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaProrrogacaoTitulo", "NumeroDocumento", c => c.String(maxLength: 120, unicode: false));
            AddColumn("dbo.PropostaProrrogacaoTitulo", "Linha", c => c.String(maxLength: 120, unicode: false));
            AddColumn("dbo.PropostaProrrogacaoTitulo", "AnoExercicio", c => c.String(maxLength: 120, unicode: false));
            AddColumn("dbo.PropostaProrrogacaoTitulo", "Empresa", c => c.String(maxLength: 120, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaProrrogacaoTitulo", "Empresa");
            DropColumn("dbo.PropostaProrrogacaoTitulo", "AnoExercicio");
            DropColumn("dbo.PropostaProrrogacaoTitulo", "Linha");
            DropColumn("dbo.PropostaProrrogacaoTitulo", "NumeroDocumento");
        }
    }
}
