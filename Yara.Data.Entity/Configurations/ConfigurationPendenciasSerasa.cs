using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    internal class ConfigurationPendenciasSerasa : EntityTypeConfiguration<PendenciasSerasa>
    {
        public ConfigurationPendenciasSerasa()
        {
            ToTable("PendenciaSerasa");
            HasKey(x => x.ID);

        }
    }
}
