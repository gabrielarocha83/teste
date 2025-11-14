using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaRenovacaoVigenciaLC : EntityTypeConfiguration<PropostaRenovacaoVigenciaLC>
    {
        public ConfigurationPropostaRenovacaoVigenciaLC()
        {
            ToTable("PropostaRenovacaoVigenciaLC");
            HasKey(c => c.ID);
            Property(c => c.PropostaLCStatusID).HasColumnType("varchar").HasMaxLength(2);
            Property(c => c.EmpresaID).HasColumnType("char").HasMaxLength(1);
        }
    }
}
