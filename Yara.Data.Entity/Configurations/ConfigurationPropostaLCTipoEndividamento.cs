using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCTipoEndividamento : EntityTypeConfiguration<PropostaLCTipoEndividamento>
    {
        public ConfigurationPropostaLCTipoEndividamento()
        {
            ToTable("PropostaLCTipoEndividamento");
            HasKey(c => c.ID);

            Property(c => c.Documento).HasMaxLength(24);
        }
    }
}
