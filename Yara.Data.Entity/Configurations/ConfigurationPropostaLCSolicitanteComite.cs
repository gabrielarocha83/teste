using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCSolicitanteComite : EntityTypeConfiguration<PropostaLCComiteSolicitante>
    {
        public ConfigurationPropostaLCSolicitanteComite()
        {
            ToTable("SolicitanteFluxoComite");
            HasKey(x => x.ID);
        }
    }
}
