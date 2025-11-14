using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaAlcadaComercialParceriaAgricola : EntityTypeConfiguration<PropostaAlcadaComercialParceriaAgricola>
    {
        public ConfigurationPropostaAlcadaComercialParceriaAgricola()
        {
            ToTable("PropostaAlcadaComercialParceriaAgricola");
            HasKey(c => c.ID);
          

        }
    }
}
