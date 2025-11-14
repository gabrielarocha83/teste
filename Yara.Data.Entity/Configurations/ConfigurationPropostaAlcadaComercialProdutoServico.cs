using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaAlcadaComercialProdutoServico : EntityTypeConfiguration<PropostaAlcadaComercialProdutoServico>
    {
        public ConfigurationPropostaAlcadaComercialProdutoServico()
        {
            ToTable("PropostaAlcadaComercialProdutoServico");
            HasKey(c => c.ID);
          

        }
    }
}
