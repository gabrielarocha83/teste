namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MudancaTabelaLiberacaoBloqueioCarteira : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.LiberacaoCarteira", newName: "BloqueioLiberacaoCarteira");
            AddColumn("dbo.BloqueioLiberacaoCarteira", "Bloqueada", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BloqueioLiberacaoCarteira", "Bloqueada");
            RenameTable(name: "dbo.BloqueioLiberacaoCarteira", newName: "LiberacaoCarteira");
        }
    }
}
