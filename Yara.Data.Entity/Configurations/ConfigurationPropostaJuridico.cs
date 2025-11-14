using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaJuridico : EntityTypeConfiguration<PropostaJuridico>
    {
        public ConfigurationPropostaJuridico()
        {
            ToTable("PropostaJuridico");
            HasKey(c => c.ID);

            Property(x => x.ComentarioHistorico).HasColumnType("text").IsMaxLength();
            Property(x => x.ParecerVisita).HasColumnType("text").IsMaxLength();
            Property(x => x.ParecerCobranca).HasColumnType("text").IsMaxLength();
            Property(x => x.EmpresaID).HasColumnType("char").HasMaxLength(1);

            Property(x => x.PropostaJuridicoStatus).HasColumnType("varchar").HasMaxLength(2);
        }
    }
}
