using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCComite : EntityTypeConfiguration<PropostaLCComite>
    {
        public ConfigurationPropostaLCComite()
        {
            ToTable("PropostaLCComite");
            HasKey(c => new { c.ID, c.PropostaLCID });

            Property(c => c.StatusComiteID).HasMaxLength(2).HasColumnType("varchar").IsRequired();
            Property(c => c.Comentario).HasColumnType("text");
        }
    }
}
