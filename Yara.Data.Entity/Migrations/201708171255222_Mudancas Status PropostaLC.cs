namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MudancasStatusPropostaLC : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PropostaLC", "PropostaLCStatusID", "dbo.PropostaLCStatus");
            DropIndex("dbo.PropostaLC", new[] { "PropostaLCStatusID" });
            DropPrimaryKey("dbo.PropostaLCStatus");
            AlterColumn("dbo.PropostaLC", "PropostaLCStatusID", c => c.String(maxLength: 2, unicode: false));
            AlterColumn("dbo.PropostaLCStatus", "ID", c => c.String(nullable: false, maxLength: 2, unicode: false));
            AlterColumn("dbo.PropostaLCStatus", "Nome", c => c.String(nullable: false, maxLength: 120, unicode: false));
            AddPrimaryKey("dbo.PropostaLCStatus", "ID");
            CreateIndex("dbo.PropostaLC", "PropostaLCStatusID");
            AddForeignKey("dbo.PropostaLC", "PropostaLCStatusID", "dbo.PropostaLCStatus", "ID");
            DropColumn("dbo.PropostaLCStatus", "UsuarioIDCriacao");
            DropColumn("dbo.PropostaLCStatus", "UsuarioIDAlteracao");
            DropColumn("dbo.PropostaLCStatus", "DataCriacao");
            DropColumn("dbo.PropostaLCStatus", "DataAlteracao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PropostaLCStatus", "DataAlteracao", c => c.DateTime());
            AddColumn("dbo.PropostaLCStatus", "DataCriacao", c => c.DateTime(nullable: false));
            AddColumn("dbo.PropostaLCStatus", "UsuarioIDAlteracao", c => c.Guid());
            AddColumn("dbo.PropostaLCStatus", "UsuarioIDCriacao", c => c.Guid(nullable: false));
            DropForeignKey("dbo.PropostaLC", "PropostaLCStatusID", "dbo.PropostaLCStatus");
            DropIndex("dbo.PropostaLC", new[] { "PropostaLCStatusID" });
            DropPrimaryKey("dbo.PropostaLCStatus");
            AlterColumn("dbo.PropostaLCStatus", "Nome", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.PropostaLCStatus", "ID", c => c.Guid(nullable: false));
            AlterColumn("dbo.PropostaLC", "PropostaLCStatusID", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.PropostaLCStatus", "ID");
            CreateIndex("dbo.PropostaLC", "PropostaLCStatusID");
            AddForeignKey("dbo.PropostaLC", "PropostaLCStatusID", "dbo.PropostaLCStatus", "ID");
        }
    }
}
