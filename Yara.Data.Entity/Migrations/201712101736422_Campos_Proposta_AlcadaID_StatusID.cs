namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Campos_Proposta_AlcadaID_StatusID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaCliente", "PropostaAlcadaID", c => c.Guid());
            AddColumn("dbo.ContaCliente", "PropostaAlcadaStatusID", c => c.String(maxLength: 120, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContaCliente", "PropostaAlcadaStatusID");
            DropColumn("dbo.ContaCliente", "PropostaAlcadaID");
        }
    }
}
