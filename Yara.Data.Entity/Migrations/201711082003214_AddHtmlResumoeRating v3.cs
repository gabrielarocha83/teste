namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHtmlResumoeRatingv3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PropostaLCDemonstrativo", "HtmlResumo", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("dbo.PropostaLCDemonstrativo", "HtmlRating", c => c.String(unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PropostaLCDemonstrativo", "HtmlRating", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.PropostaLCDemonstrativo", "HtmlResumo", c => c.String(maxLength: 120, unicode: false));
        }
    }
}
