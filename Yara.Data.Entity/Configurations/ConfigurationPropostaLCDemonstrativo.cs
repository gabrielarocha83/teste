using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCDemonstrativo : EntityTypeConfiguration<PropostaLCDemonstrativo>
    {
        public ConfigurationPropostaLCDemonstrativo()
        {
            ToTable("PropostaLCDemonstrativo");
            HasKey(c => c.ID);
            Property(c => c.Html).HasColumnType("text").IsMaxLength();
            Property(c => c.HtmlResumo).HasColumnType("text").IsMaxLength();
            Property(c => c.HtmlRating).HasColumnType("text").IsMaxLength();
            Property(c => c.NomeArquivo).HasMaxLength(255);
            Property(c => c.Tipo).HasMaxLength(2);
        }
    }
}
