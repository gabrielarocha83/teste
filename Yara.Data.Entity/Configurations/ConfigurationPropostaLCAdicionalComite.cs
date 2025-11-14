using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCAdicionalComite : EntityTypeConfiguration<PropostaLCAdicionalComite>
    {
        public ConfigurationPropostaLCAdicionalComite()
        {
            ToTable("PropostaLCAdicionalComite");
            HasKey(c => new { c.ID, c.PropostaLCAdicionalID });

            Property(c => c.PropostaLCAdicionalStatusComiteID).HasMaxLength(2).HasColumnType("varchar").IsRequired();
            Property(c => c.Comentario).HasColumnType("text");
            //Property(c => c.CodigoSAP).HasMaxLength(10);
        }
    }
}
