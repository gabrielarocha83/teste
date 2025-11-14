using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCNecessidadeProduto : EntityTypeConfiguration<PropostaLCNecessidadeProduto>
    {
        public ConfigurationPropostaLCNecessidadeProduto()
        {
            ToTable("PropostaLCNecessidadeProduto");
            HasKey(c => c.ID);
        }
    }
}
