using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaAbonoAcompanhamento : EntityTypeConfiguration<PropostaAbonoAcompanhamento>
    {
        public ConfigurationPropostaAbonoAcompanhamento()
        {
            ToTable("PropostaAbonoAcompanhamento");
            HasKey(c => new{c.PropostaAbonoID, c.UsuarioID});
           
        }
    }
}
