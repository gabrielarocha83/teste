namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Campos_ContaCliente_Representante : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaCliente_Representante", "DataAlteracao", c => c.DateTime());
            AddColumn("dbo.ContaCliente_Representante", "UsuarioIDAlteracao", c => c.Guid());
            AddColumn("dbo.ContaCliente_Representante", "Ativo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContaCliente_Representante", "Ativo");
            DropColumn("dbo.ContaCliente_Representante", "UsuarioIDAlteracao");
            DropColumn("dbo.ContaCliente_Representante", "DataAlteracao");
        }
    }
}
