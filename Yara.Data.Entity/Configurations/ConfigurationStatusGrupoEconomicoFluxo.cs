using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationStatusGrupoEconomicoFluxo : EntityTypeConfiguration<StatusGrupoEconomicoFluxo>
    {
        public ConfigurationStatusGrupoEconomicoFluxo()
        {
            ToTable("StatusGrupoEconomicoFluxo");
            HasKey(c => c.ID);
            Property(c => c.ID).HasMaxLength(3).HasColumnType("varchar").IsRequired();
        }
    }
}
