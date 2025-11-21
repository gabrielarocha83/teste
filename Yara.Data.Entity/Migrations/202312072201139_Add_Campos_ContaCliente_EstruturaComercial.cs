namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Campos_ContaCliente_EstruturaComercial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaCliente_EstruturaComercial", "DataAlteracao", c => c.DateTime());
            AddColumn("dbo.ContaCliente_EstruturaComercial", "UsuarioIDAlteracao", c => c.Guid());
            AddColumn("dbo.ContaCliente_EstruturaComercial", "Ativo", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContaCliente_EstruturaComercial", "Ativo");
            DropColumn("dbo.ContaCliente_EstruturaComercial", "UsuarioIDAlteracao");
            DropColumn("dbo.ContaCliente_EstruturaComercial", "DataAlteracao");
        }
    }
}
