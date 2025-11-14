namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstruturaTituloComentario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TituloComentario",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        NumeroDocumento = c.String(nullable: false, maxLength: 10, fixedLength: true, unicode: false),
                        Linha = c.String(nullable: false, maxLength: 3, fixedLength: true, unicode: false),
                        AnoExercicio = c.String(nullable: false, maxLength: 4, fixedLength: true, unicode: false),
                        Texto = c.String(nullable: false, unicode: false, storeType: "text"),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TituloComentario");
        }
    }
}
