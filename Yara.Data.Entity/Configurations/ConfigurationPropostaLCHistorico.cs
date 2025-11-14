using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCHistorico : EntityTypeConfiguration<PropostaLCHistorico>
    {
        public ConfigurationPropostaLCHistorico()
        {
            ToTable("PropostaLCHistorico");
            HasKey(c => c.ID);
            Property(c => c.PropostaLCStatusID).HasMaxLength(2).HasColumnType("varchar").IsRequired();
        }
    }
}
