namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CamposPropostaLCeComite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLC", "LCCliente", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLC", "VigenciaInicialCliente", c => c.DateTime());
            AddColumn("dbo.PropostaLC", "VigenciaFinalCliente", c => c.DateTime());
            AddColumn("dbo.PropostaLC", "AprovadoComite", c => c.Boolean(nullable: false));
            AddColumn("dbo.PropostaLC", "DataAprovacaoComite", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLC", "DataAprovacaoComite");
            DropColumn("dbo.PropostaLC", "AprovadoComite");
            DropColumn("dbo.PropostaLC", "VigenciaFinalCliente");
            DropColumn("dbo.PropostaLC", "VigenciaInicialCliente");
            DropColumn("dbo.PropostaLC", "LCCliente");
        }
    }
}
