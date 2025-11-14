namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Campos_FixarComite_PropostaLCAdicional : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLCAdicional", "AprovadoComite", c => c.Boolean(nullable: false));
            AddColumn("dbo.PropostaLCAdicional", "DataAprovacaoComite", c => c.DateTime());
            AddColumn("dbo.PropostaLCAdicional", "FixarLimiteCredito", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLCAdicional", "FixarLimiteCredito");
            DropColumn("dbo.PropostaLCAdicional", "DataAprovacaoComite");
            DropColumn("dbo.PropostaLCAdicional", "AprovadoComite");
        }
    }
}
