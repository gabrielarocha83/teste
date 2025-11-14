namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Correcao_PropostaLCAdicionalComite_Campos : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PropostaLCAdicionalComite", "EmpresaID");
            DropColumn("dbo.PropostaLCAdicionalComite", "CodigoSAP");
            DropColumn("dbo.PropostaLCAdicionalComite", "UsuarioIDCriacao");
            DropColumn("dbo.PropostaLCAdicionalComite", "UsuarioIDAlteracao");
            DropColumn("dbo.PropostaLCAdicionalComite", "DataAlteracao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PropostaLCAdicionalComite", "DataAlteracao", c => c.DateTime());
            AddColumn("dbo.PropostaLCAdicionalComite", "UsuarioIDAlteracao", c => c.Guid());
            AddColumn("dbo.PropostaLCAdicionalComite", "UsuarioIDCriacao", c => c.Guid(nullable: false));
            AddColumn("dbo.PropostaLCAdicionalComite", "CodigoSAP", c => c.String(maxLength: 120, unicode: false));
            AddColumn("dbo.PropostaLCAdicionalComite", "EmpresaID", c => c.String(maxLength: 120, unicode: false));
        }
    }
}
