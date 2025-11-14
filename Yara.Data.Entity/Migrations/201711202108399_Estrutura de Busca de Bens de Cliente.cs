namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstruturadeBuscadeBensdeCliente : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContaClienteBuscaBens",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ContaClienteID = c.Guid(nullable: false),
                        DataSolicitacao = c.DateTime(nullable: false),
                        DataPatrimonio = c.DateTime(),
                        Patrimonio = c.String(nullable: false, unicode: false, storeType: "text"),
                        EmpresasID = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .ForeignKey("dbo.Empresa", t => t.EmpresasID)
                .Index(t => t.ContaClienteID)
                .Index(t => t.EmpresasID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContaClienteBuscaBens", "EmpresasID", "dbo.Empresa");
            DropForeignKey("dbo.ContaClienteBuscaBens", "ContaClienteID", "dbo.ContaCliente");
            DropIndex("dbo.ContaClienteBuscaBens", new[] { "EmpresasID" });
            DropIndex("dbo.ContaClienteBuscaBens", new[] { "ContaClienteID" });
            DropTable("dbo.ContaClienteBuscaBens");
        }
    }
}
