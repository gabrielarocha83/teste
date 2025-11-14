using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationContaClienteResponsavelGarantia : EntityTypeConfiguration<ContaClienteResponsavelGarantia>
    {
        public ConfigurationContaClienteResponsavelGarantia()
        {
            ToTable("ContaClienteResponsavelGarantia");
            HasKey(c => c.ID);

            Property(x => x.TipoResponsabilidade).HasColumnType("varchar").HasMaxLength(3);
        }
    }
}
