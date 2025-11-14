using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCPecuaria : EntityTypeConfiguration<PropostaLCPecuaria>
    {
        public ConfigurationPropostaLCPecuaria()
        {
            ToTable("PropostaLCPecuaria");
            HasKey(c => c.ID);

            Property(c => c.Documento).HasMaxLength(24);
        }
    }
}
