using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationExperiencia : EntityTypeConfiguration<Experiencia>
    {
        public ConfigurationExperiencia()
        {
            ToTable("Experiencia");
            HasKey(x => x.ID);
            Property(c => c.Descricao).HasMaxLength(50).IsRequired();
        }
    }
}
