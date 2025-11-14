using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationCultura : EntityTypeConfiguration<Cultura>
    {
        public ConfigurationCultura()
        {
            ToTable("Cultura");
            HasKey(c => c.ID);
            Property(x => x.Descricao).IsRequired().HasMaxLength(50);
            Property(x => x.UnidadeMedida).IsRequired().HasMaxLength(30);
        }
    }
}
