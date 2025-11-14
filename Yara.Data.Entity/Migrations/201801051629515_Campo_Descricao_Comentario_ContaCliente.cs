namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Campo_Descricao_Comentario_ContaCliente : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContaClienteComentario", "Descricao", c => c.String(nullable: false, maxLength: 1000, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContaClienteComentario", "Descricao", c => c.String(nullable: false, maxLength: 255, unicode: false));
        }
    }
}
