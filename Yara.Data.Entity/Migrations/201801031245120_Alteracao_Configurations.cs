namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alteracao_Configurations : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Blog", new[] { "EmpresaID" });
            AlterColumn("dbo.Blog", "EmpresaID", c => c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false));
            CreateIndex("dbo.Blog", "EmpresaID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Blog", new[] { "EmpresaID" });
            AlterColumn("dbo.Blog", "EmpresaID", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
            CreateIndex("dbo.Blog", "EmpresaID");
        }
    }
}
