using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationStatusOrdemVenda : EntityTypeConfiguration<StatusOrdemVendas>
    {
        public ConfigurationStatusOrdemVenda()
        {
            ToTable("StatusOrdemVenda");
            HasKey(c => c.ID);
            Property(c => c.Status).HasMaxLength(2).HasColumnType("varchar").IsRequired();
            Property(c => c.Descricao).HasMaxLength(50).HasColumnType("varchar").IsRequired();
        }
    }
}
