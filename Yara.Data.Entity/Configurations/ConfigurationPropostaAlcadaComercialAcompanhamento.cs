using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaAlcadaComercialAcompanhamento : EntityTypeConfiguration<PropostaAlcadaComercialAcompanhamento>
    {
        public ConfigurationPropostaAlcadaComercialAcompanhamento()
        {
            ToTable("PropostaAlcadaComercialAcompanhamento");
            HasKey(c => new { c.PropostaAlcadaComercialID, c.UsuarioID });
        }
    }
}
