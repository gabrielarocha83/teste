using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaAlcadaComercialCultura : EntityTypeConfiguration<PropostaAlcadaComercialCultura>
    {
        public ConfigurationPropostaAlcadaComercialCultura()
        {
            ToTable("PropostaAlcadaComercialCultura");
            HasKey(c => c.ID);
          

        }
    }
}
