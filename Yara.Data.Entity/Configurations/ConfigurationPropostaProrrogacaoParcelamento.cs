using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaProrrogacaoParcelamento : EntityTypeConfiguration<PropostaProrrogacaoParcelamento>
    {
        public ConfigurationPropostaProrrogacaoParcelamento()
        {
            ToTable("PropostaProrrogacaoParcelamento");
            HasKey(c => c.ID);

        }
    }
}
