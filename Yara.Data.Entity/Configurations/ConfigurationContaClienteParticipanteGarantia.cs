using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationContaClienteParticipanteGarantia : EntityTypeConfiguration<ContaClienteParticipanteGarantia>
    {
        public ConfigurationContaClienteParticipanteGarantia()
        {
            ToTable("ContaClienteParticipanteGarantia");
            HasKey(c => c.ID);
        }
    }
}
