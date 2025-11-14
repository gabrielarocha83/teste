using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCOutraReceita : EntityTypeConfiguration<PropostaLCOutraReceita>
    {
        public ConfigurationPropostaLCOutraReceita()
        {
            ToTable("PropostaLCOutraReceita");
            HasKey(c => c.ID);

            Property(c => c.Documento).HasMaxLength(24);
        }
    }
}
