using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCStatusComite : EntityTypeConfiguration<PropostaLCStatusComite>
    {
        public ConfigurationPropostaLCStatusComite()
        {
            ToTable("PropostaLCStatusComite");
            Property(c => c.ID).HasMaxLength(2).HasColumnType("varchar").IsRequired();
            HasKey(c => c.ID);
            
        }
    }
}
