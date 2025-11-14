namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Campos_Cliente_PropostaLCAdicional : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLCAdicional", "LCCliente", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLCAdicional", "VigenciaInicialCliente", c => c.DateTime());
            AddColumn("dbo.PropostaLCAdicional", "VigenciaFinalCliente", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLCAdicional", "VigenciaFinalCliente");
            DropColumn("dbo.PropostaLCAdicional", "VigenciaInicialCliente");
            DropColumn("dbo.PropostaLCAdicional", "LCCliente");
        }
    }
}
