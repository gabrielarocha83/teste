namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class All_Varchar_Max_To_IsMaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContaClienteFinanceiro", "DescricaoConceito", c => c.String(maxLength: 8000, unicode: false));
            AlterColumn("dbo.ProcessamentoCarteira", "Motivo", c => c.String(maxLength: 8000, unicode: false));
            AlterColumn("dbo.ProcessamentoCarteira", "Detalhes", c => c.String(maxLength: 8000, unicode: false));
            AlterColumn("dbo.LogDivisaoRemessaLiberacao", "Restricao", c => c.String(maxLength: 8000, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LogDivisaoRemessaLiberacao", "Restricao", c => c.String(unicode: false));
            AlterColumn("dbo.ProcessamentoCarteira", "Detalhes", c => c.String(unicode: false));
            AlterColumn("dbo.ProcessamentoCarteira", "Motivo", c => c.String(unicode: false));
            AlterColumn("dbo.ContaClienteFinanceiro", "DescricaoConceito", c => c.String(unicode: false));
        }
    }
}
