using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaCobrancaStatus : EntityTypeConfiguration<PropostaCobrancaStatus>
    {
        public ConfigurationPropostaCobrancaStatus()
        {
            ToTable("PropostaCobrancaStatus");
            Property(x => x.ID).HasColumnType("char").HasMaxLength(2);
            HasKey(c => c.ID);
           
           

        }
    }
}
