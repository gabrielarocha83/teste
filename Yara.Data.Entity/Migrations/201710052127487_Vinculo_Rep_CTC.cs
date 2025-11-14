namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Vinculo_Rep_CTC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaCliente_Representante", "CodigoSapCTC", c => c.String(maxLength: 10, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContaCliente_Representante", "CodigoSapCTC");
        }
    }
}
