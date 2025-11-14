namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Campo_MotivoB7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BloqueioLiberacaoCarteira", "MotivoB7", c => c.String(maxLength: 120, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BloqueioLiberacaoCarteira", "MotivoB7");
        }
    }
}
