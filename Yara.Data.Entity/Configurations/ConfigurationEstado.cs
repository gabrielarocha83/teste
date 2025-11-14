using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    internal class ConfigurationEstado : EntityTypeConfiguration<Estado>
    {
        public ConfigurationEstado()
        {
            ToTable("Estado");
            HasKey(x => x.ID);
          
        }
    }
}
