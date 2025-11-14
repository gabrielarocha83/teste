using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationIdadeMediaCanavial : EntityTypeConfiguration<IdadeMediaCanavial>
    {
        public ConfigurationIdadeMediaCanavial()
        {
            ToTable("IdadeMediaCanavial");
            HasKey(c => c.ID);
            Property(x => x.Nome).IsRequired().HasMaxLength(120);
        }
    }
}
