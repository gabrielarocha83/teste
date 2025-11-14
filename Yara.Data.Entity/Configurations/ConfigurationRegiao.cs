using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationRegiao : EntityTypeConfiguration<Regiao>
    {
        public ConfigurationRegiao()
        {
            ToTable("Regiao");
            HasKey(c => c.ID);
            Property(x => x.Nome).IsRequired().HasMaxLength(240);
        }
    }
}
