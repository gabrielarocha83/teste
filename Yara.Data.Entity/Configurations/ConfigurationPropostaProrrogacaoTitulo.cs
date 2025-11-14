using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaProrrogacaoTitulo : EntityTypeConfiguration<PropostaProrrogacaoTitulo>
    {
        public ConfigurationPropostaProrrogacaoTitulo()
        {
            ToTable("PropostaProrrogacaoTitulo");
            HasKey(c => c.ID);
        }
    }
}
