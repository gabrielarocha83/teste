using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCAdicionalAcompanhamento : EntityTypeConfiguration<PropostaLCAdicionalAcompanhamento>
    {
        public ConfigurationPropostaLCAdicionalAcompanhamento()
        {
            ToTable("PropostaLCAdicionalAcompanhamento");
            HasKey(c => new { c.PropostaLCAdicionalID, c.UsuarioID });
        }
    }
}
