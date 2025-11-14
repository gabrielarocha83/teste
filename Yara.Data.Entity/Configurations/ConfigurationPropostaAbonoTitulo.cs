using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaAbonoTitulo : EntityTypeConfiguration<PropostaAbonoTitulo>
    {
        public ConfigurationPropostaAbonoTitulo()
        {
            ToTable("PropostaAbonoTitulo");
            HasKey(c => c.ID);
        }
    }
}
