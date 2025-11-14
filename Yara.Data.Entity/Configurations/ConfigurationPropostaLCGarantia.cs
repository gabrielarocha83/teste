using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCGarantia : EntityTypeConfiguration<PropostaLCGarantia>
    {
        public ConfigurationPropostaLCGarantia()
        {
            ToTable("PropostaLCGarantia");
            HasKey(c => c.ID);

            Property(x => x.Documento).HasColumnType("varchar").HasMaxLength(20);
            Property(x => x.Nome).HasColumnType("varchar").HasMaxLength(128);
    }
    }
}
