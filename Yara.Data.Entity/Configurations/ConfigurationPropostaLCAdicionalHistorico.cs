using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCAdicionalHistorico : EntityTypeConfiguration<PropostaLCAdicionalHistorico>
    {
        public ConfigurationPropostaLCAdicionalHistorico()
        {
            ToTable("PropostaLCAdicionalHistorico");
            HasKey(c => c.ID);

            Property(c => c.PropostaLCStatusID).HasMaxLength(2).HasColumnType("varchar").IsRequired();
        }
    }
}
