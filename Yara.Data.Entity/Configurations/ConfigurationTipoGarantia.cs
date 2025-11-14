using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationTipoGarantia : EntityTypeConfiguration<TipoGarantia>
    {
        public ConfigurationTipoGarantia()
        {
            ToTable("TipoGarantia");
            HasKey(c => c.ID);
            Property(c => c.Nome).IsRequired().HasMaxLength(120);
        }
    }
}
