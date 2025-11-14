using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationFluxoGrupoEconomico: EntityTypeConfiguration<FluxoGrupoEconomico>
    {
        public ConfigurationFluxoGrupoEconomico()
        {
            ToTable("FluxoGrupoEconomico");
            HasKey(c => c.ID);
            Property(c => c.Nivel).IsRequired();
            Property(c => c.EmpresaID).HasMaxLength(1);
        }
    }
}
