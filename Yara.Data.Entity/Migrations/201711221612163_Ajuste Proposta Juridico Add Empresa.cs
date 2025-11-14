namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjustePropostaJuridicoAddEmpresa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaJuridico", "EmpresaID", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
            CreateIndex("dbo.PropostaJuridico", "EmpresaID");
            AddForeignKey("dbo.PropostaJuridico", "EmpresaID", "dbo.Empresa", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaJuridico", "EmpresaID", "dbo.Empresa");
            DropIndex("dbo.PropostaJuridico", new[] { "EmpresaID" });
            DropColumn("dbo.PropostaJuridico", "EmpresaID");
        }
    }
}
