using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationAreaIrrigada : EntityTypeConfiguration<AreaIrrigada>
    {
        public ConfigurationAreaIrrigada()
        {
            ToTable("AreaIrrigada");
            HasKey(c => c.ID);
            Property(x => x.Nome).IsRequired().HasMaxLength(50);
        }
    }
}
