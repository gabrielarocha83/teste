using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCAdicionalComiteSolicitante : EntityTypeConfiguration<PropostaLCAdicionalComiteSolicitante>
    {
        public ConfigurationPropostaLCAdicionalComiteSolicitante()
        {
            ToTable("SolicitanteFluxoComiteAdicional");
            HasKey(x => x.ID);
        }
    }
}
