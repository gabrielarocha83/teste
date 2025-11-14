using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCAcompanhamento : EntityTypeConfiguration<PropostaLCAcompanhamento>
    {
        public ConfigurationPropostaLCAcompanhamento()
        {
            ToTable("PropostaLCAcompanhamento");
            HasKey(c => new { c.PropostaLCID, c.UsuarioID });
        }
    }
}
