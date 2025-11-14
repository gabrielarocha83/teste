using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaJuridicoHistoricoPagamento : EntityTypeConfiguration<PropostaJuridicoHistoricoPagamento>
    {
        public ConfigurationPropostaJuridicoHistoricoPagamento()
        {
            ToTable("PropostaJuridicoHistoricoPagamento");
            HasKey(c => c.ID);
        }
    }
}
