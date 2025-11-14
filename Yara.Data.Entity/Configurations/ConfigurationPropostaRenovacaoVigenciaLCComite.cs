using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaRenovacaoVigenciaLCComite : EntityTypeConfiguration<PropostaRenovacaoVigenciaLCComite>
    {
        public ConfigurationPropostaRenovacaoVigenciaLCComite()
        {
            ToTable("PropostaRenovacaoVigenciaLCComite");
            // HasKey(c => new { c.ID, c.PropostaRenovacaoVigenciaLCID });
            HasKey(c => c.ID);
            Property(c => c.StatusComiteID).HasMaxLength(2).HasColumnType("varchar").IsRequired();
            Property(c => c.Comentario).HasColumnType("text").IsMaxLength();
        }
    }
}
