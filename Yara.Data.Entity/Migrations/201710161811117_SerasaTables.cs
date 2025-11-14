namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SerasaTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PendenciaSerasa", "Empresa", c => c.String(maxLength: 120, unicode: false));
            AddColumn("dbo.PendenciaSerasa", "CNPJ", c => c.String(maxLength: 120, unicode: false));
            AddColumn("dbo.PendenciaSerasa", "Falencia", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PendenciaSerasa", "Data", c => c.DateTime());
            AlterColumn("dbo.PendenciaSerasa", "Quantidade", c => c.Int());
            AlterColumn("dbo.PendenciaSerasa", "Valor", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PendenciaSerasa", "Valor", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PendenciaSerasa", "Quantidade", c => c.Int(nullable: false));
            AlterColumn("dbo.PendenciaSerasa", "Data", c => c.DateTime(nullable: false));
            DropColumn("dbo.PendenciaSerasa", "Falencia");
            DropColumn("dbo.PendenciaSerasa", "CNPJ");
            DropColumn("dbo.PendenciaSerasa", "Empresa");
        }
    }
}
