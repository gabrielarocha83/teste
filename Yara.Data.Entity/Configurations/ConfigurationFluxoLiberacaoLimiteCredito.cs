using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationFluxoLiberacaoLimiteCredito : EntityTypeConfiguration<FluxoLiberacaoLimiteCredito>
    {
        public ConfigurationFluxoLiberacaoLimiteCredito()
        {
            ToTable("FluxoLiberacaoLimiteCredito");
            HasKey(c => c.ID);

            Property(c => c.Nivel).IsRequired();
            Property(c => c.ValorDe).IsRequired();
            Property(c => c.ValorAte).IsRequired();
            Property(c => c.PrimeiroPerfilID).IsRequired();
        }
    }
}
