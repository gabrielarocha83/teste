using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCAdicionalStatusComite : EntityTypeConfiguration<PropostaLCAdicionalStatusComite>
    {
        public ConfigurationPropostaLCAdicionalStatusComite()
        {
            ToTable("PropostaLCAdicionalStatusComite");
            Property(c => c.ID).HasMaxLength(2).HasColumnType("varchar").IsRequired();
            HasKey(c => c.ID);
        }
    }
}
