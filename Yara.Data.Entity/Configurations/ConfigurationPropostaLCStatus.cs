using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCStatus : EntityTypeConfiguration<PropostaLCStatus>
    {
        public ConfigurationPropostaLCStatus()
        {
            ToTable("PropostaLCStatus");
            HasKey(c => c.ID);
            Property(c => c.ID).HasMaxLength(2).HasColumnType("varchar").IsRequired();
            Property(c => c.Nome).HasMaxLength(120).HasColumnType("varchar").IsRequired();
        }
    }
}
