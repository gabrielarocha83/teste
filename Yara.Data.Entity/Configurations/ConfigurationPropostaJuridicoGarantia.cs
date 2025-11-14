using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaJuridicoGarantia : EntityTypeConfiguration<PropostaJuridicoGarantia>
    {
        public ConfigurationPropostaJuridicoGarantia()
        {
            ToTable("PropostaJuridicoGarantia");
            HasKey(c => c.ID);

            Property(c => c.Nome).IsRequired().HasMaxLength(120);
        }
    }
}
