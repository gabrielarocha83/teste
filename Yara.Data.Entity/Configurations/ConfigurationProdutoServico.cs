using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationProdutoServico : EntityTypeConfiguration<ProdutoServico>
    {
        public ConfigurationProdutoServico()
        {
            ToTable("ProdutoServico");
            HasKey(c => c.ID);
            Property(c => c.Nome).IsRequired().HasMaxLength(120);
        }
    }
}
