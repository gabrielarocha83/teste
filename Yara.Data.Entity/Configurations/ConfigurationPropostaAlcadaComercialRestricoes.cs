using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaAlcadaComercialRestricoes : EntityTypeConfiguration<PropostaAlcadaComercialRestricoes>
    {
        public ConfigurationPropostaAlcadaComercialRestricoes()
        {
            ToTable("PropostaAlcadaComercialRestricao");
            HasKey(c => c.ID);
            Property(c => c.EmpresasID).HasColumnType("char").HasMaxLength(1);
        }
    }
}
