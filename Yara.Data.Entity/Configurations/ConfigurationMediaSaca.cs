using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationMediaSaca : EntityTypeConfiguration<MediaSaca>
    {
        public ConfigurationMediaSaca()
        {
            ToTable("ValorSaca");
            HasKey(c => c.ID);
            Property(c => c.Nome).HasMaxLength(100).IsRequired();
            Property(c => c.Valor).HasPrecision(18, 2).IsRequired();
            Property(c => c.Peso).HasPrecision(18, 2).IsRequired();
        }
    }
}
