using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPorcentagemQuebra : EntityTypeConfiguration<PorcentagemQuebra>
    {
        public ConfigurationPorcentagemQuebra()
        {
            ToTable("PorcentagemQuebra");
            HasKey(c => c.ID);
            Property(c => c.Porcentagem).IsRequired();
          
        }
    }
}
