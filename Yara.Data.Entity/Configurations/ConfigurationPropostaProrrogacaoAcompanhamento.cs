using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaProrrogacaoAcompanhamento : EntityTypeConfiguration<PropostaProrrogacaoAcompanhamento>
    {
        public ConfigurationPropostaProrrogacaoAcompanhamento()
        {
            ToTable("PropostaProrrogacaoAcompanhamento");
            HasKey(c => new{c.PropostaProrrogacaoID, c.UsuarioID});
           
        }
    }
}
