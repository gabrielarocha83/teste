using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    internal class ConfigurationPais : EntityTypeConfiguration<Pais>
    {
        public ConfigurationPais()
        {
            ToTable("Pais");
            HasKey(x => x.ID);
            Property(c => c.NomePtbr).IsRequired();
            Property(c => c.NomeEnus).IsRequired();
        }
    }
}
