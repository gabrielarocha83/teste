using System.Data.Entity.ModelConfiguration;
using Yara.Domain;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostAbonoComiteSolicitante : EntityTypeConfiguration<PropostaAbonoComiteSolicitante>
    {
        public ConfigurationPropostAbonoComiteSolicitante()
        {
            ToTable("PropostaAbonoComiteSolicitante");
            HasKey(c => c.ID);
            

        }
    }
}
