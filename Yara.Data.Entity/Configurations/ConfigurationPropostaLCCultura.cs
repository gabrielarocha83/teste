using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCCultura : EntityTypeConfiguration<PropostaLCCultura>
    {
        public ConfigurationPropostaLCCultura()
        {
            ToTable("PropostaLCCultura");
            HasKey(c => c.ID);

            Property(c => c.Documento).HasMaxLength(24);
        }
    }
}
