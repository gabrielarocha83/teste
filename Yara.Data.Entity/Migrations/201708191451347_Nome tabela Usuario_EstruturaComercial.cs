namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NometabelaUsuario_EstruturaComercial : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UsuarioEstruturaComercial", newName: "Usuario_EstruturaComercial");
            RenameColumn(table: "dbo.Usuario_EstruturaComercial", name: "Usuario_ID", newName: "UsuarioID");
            RenameColumn(table: "dbo.Usuario_EstruturaComercial", name: "EstruturaComercial_CodigoSap", newName: "CodigoSap");
            RenameIndex(table: "dbo.Usuario_EstruturaComercial", name: "IX_Usuario_ID", newName: "IX_UsuarioID");
            RenameIndex(table: "dbo.Usuario_EstruturaComercial", name: "IX_EstruturaComercial_CodigoSap", newName: "IX_CodigoSap");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Usuario_EstruturaComercial", name: "IX_CodigoSap", newName: "IX_EstruturaComercial_CodigoSap");
            RenameIndex(table: "dbo.Usuario_EstruturaComercial", name: "IX_UsuarioID", newName: "IX_Usuario_ID");
            RenameColumn(table: "dbo.Usuario_EstruturaComercial", name: "CodigoSap", newName: "EstruturaComercial_CodigoSap");
            RenameColumn(table: "dbo.Usuario_EstruturaComercial", name: "UsuarioID", newName: "Usuario_ID");
            RenameTable(name: "dbo.Usuario_EstruturaComercial", newName: "UsuarioEstruturaComercial");
        }
    }
}
