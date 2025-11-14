using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaJuridicoTitulo : EntityTypeConfiguration<PropostaJuridicoTitulo>
    {
        public ConfigurationPropostaJuridicoTitulo()
        {
            ToTable("PropostaJuridicoTitulo");
            HasKey(c => c.ID);
        }
    }
}
