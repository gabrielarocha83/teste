using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationStatusCobranca : EntityTypeConfiguration<StatusCobranca>
    {
        public ConfigurationStatusCobranca()
        {
            ToTable("StatusCobranca");
            HasKey(c => c.ID);
            Property(c => c.Descricao).HasMaxLength(60).HasColumnType("varchar").IsRequired();
        }
    }
}
