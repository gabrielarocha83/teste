namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Blog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blog",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Area = c.Guid(nullable: false),
                        ParaID = c.Guid(),
                        Mensagem = c.String(nullable: false, unicode: false, storeType: "text"),
                        UsuarioCriacaoID = c.Guid(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        EmpresaID = c.String(maxLength: 1, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Empresa", t => t.EmpresaID)
                .ForeignKey("dbo.Usuario", t => t.ParaID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioCriacaoID)
                .Index(t => t.Area, name: "ix_area")
                .Index(t => t.ParaID)
                .Index(t => t.UsuarioCriacaoID)
                .Index(t => t.EmpresaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Blog", "UsuarioCriacaoID", "dbo.Usuario");
            DropForeignKey("dbo.Blog", "ParaID", "dbo.Usuario");
            DropForeignKey("dbo.Blog", "EmpresaID", "dbo.Empresa");
            DropIndex("dbo.Blog", new[] { "EmpresaID" });
            DropIndex("dbo.Blog", new[] { "UsuarioCriacaoID" });
            DropIndex("dbo.Blog", new[] { "ParaID" });
            DropIndex("dbo.Blog", "ix_area");
            DropTable("dbo.Blog");
        }
    }
}
