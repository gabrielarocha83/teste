using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationCulturaEstado : EntityTypeConfiguration<CulturaEstado>
    {
        public ConfigurationCulturaEstado()
        {
            ToTable("CulturaEstado");
            HasKey(c => c.ID);
        }
    }
}
