namespace Yara.Data.Entity.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddHtmlResumoeRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLCDemonstrativo", "HtmlResumo", c => c.String(maxLength: 120, unicode: false));
            AddColumn("dbo.PropostaLCDemonstrativo", "HtmlRating", c => c.String(maxLength: 120, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLCDemonstrativo", "HtmlRating");
            DropColumn("dbo.PropostaLCDemonstrativo", "HtmlResumo");
        }
    }
}
