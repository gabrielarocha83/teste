using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaAlcadaComercialDocumento : EntityTypeConfiguration<PropostaAlcadaComercialDocumentos>
    {
        public ConfigurationPropostaAlcadaComercialDocumento()
        {
            ToTable("PropostaAlcadaComercialDocumento");
            HasKey(c => c.ID);
          

        }
    }
}
