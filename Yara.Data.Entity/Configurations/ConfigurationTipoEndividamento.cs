using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationTipoEndividamento : EntityTypeConfiguration<TipoEndividamento>
    {
        public ConfigurationTipoEndividamento()
        {
            ToTable("TipoEndividamento");
            HasKey(x => x.ID);
            Property(c => c.Tipo).IsRequired();
        }
    }
}
