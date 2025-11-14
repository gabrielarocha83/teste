using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationProdutividadeMedia : EntityTypeConfiguration<ProdutividadeMedia>
    {
        public ConfigurationProdutividadeMedia()
        {
            ToTable("Produtividade");
            HasKey(c => c.ID);
            Property(c => c.Nome).IsRequired().HasMaxLength(100);
            Property(c => c.RegiaoID).IsRequired();
        }
    }
}
