using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationUnidadeMedidaCultura : EntityTypeConfiguration<UnidadeMedidaCultura>
    {
        public ConfigurationUnidadeMedidaCultura()
        {
            ToTable("UnidadeMedidaCultura");
            HasKey(c => c.ID);
            Property(c => c.Nome).IsRequired().HasMaxLength(120);
            Property(c => c.Sigla).HasMaxLength(10);
        }
    }
}
