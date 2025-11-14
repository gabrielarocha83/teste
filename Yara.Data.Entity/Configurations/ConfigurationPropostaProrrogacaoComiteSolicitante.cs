using System.Data.Entity.ModelConfiguration;
using Yara.Domain;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaProrrogacaoComiteSolicitante : EntityTypeConfiguration<PropostaProrrogacaoComiteSolicitante>
    {
        public ConfigurationPropostaProrrogacaoComiteSolicitante()
        {
            ToTable("PropostaProrrogacaoComiteSolicitante");
            HasKey(c => c.ID);
            

        }
    }
}
