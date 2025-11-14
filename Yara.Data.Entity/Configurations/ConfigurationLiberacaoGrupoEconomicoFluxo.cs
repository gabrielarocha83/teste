using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationLiberacaoGrupoEconomicoFluxo : EntityTypeConfiguration<LiberacaoGrupoEconomicoFluxo>
    {
        public ConfigurationLiberacaoGrupoEconomicoFluxo()
        {
            ToTable("LiberacaoGrupoEconomicoFluxo");
            HasKey(x => x.ID);
            Property(c => c.CodigoSap).HasMaxLength(10).IsRequired();
            Property(c => c.StatusGrupoEconomicoFluxoID).HasMaxLength(3).HasColumnType("varchar").IsRequired();


        }
    }
}
