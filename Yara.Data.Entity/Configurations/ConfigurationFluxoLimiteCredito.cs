using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationFluxoLimiteCredito : EntityTypeConfiguration<FluxoLiberacaoManual>
    {
        public ConfigurationFluxoLimiteCredito()
        {
            ToTable("FluxoLiberacaoManual");
            HasKey(c => c.ID);
            Property(c => c.Nivel).IsRequired();
            Property(c => c.ValorDe).IsRequired();
            Property(c => c.ValorAte).IsRequired();
        }
    }
}
