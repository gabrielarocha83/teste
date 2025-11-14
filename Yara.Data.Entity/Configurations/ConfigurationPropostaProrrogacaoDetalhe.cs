using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaProrrogacaoDetalhe : EntityTypeConfiguration<PropostaProrrogacaoDetalhe>
    {
        public ConfigurationPropostaProrrogacaoDetalhe()
        {
            ToTable("PropostaProrrogacaoDetalhe");
            HasKey(c => c.ID);

        }
    }
}
