using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCReferencia : EntityTypeConfiguration<PropostaLCReferencia>
    {
        public ConfigurationPropostaLCReferencia()
        {
            ToTable("PropostaLCReferencia");
            HasKey(c => c.ID);

            Property(x => x.TipoReferencia).HasColumnType("varchar").HasMaxLength(20);
            Property(x => x.NomeEmpresa).HasColumnType("varchar").HasMaxLength(128);
            Property(x => x.NomeBanco).HasColumnType("varchar").HasMaxLength(128);
            Property(x => x.Municipio).HasColumnType("varchar").HasMaxLength(128);
            Property(x => x.Telefone).HasColumnType("varchar").HasMaxLength(64);
            Property(x => x.NomeContato).HasColumnType("varchar").HasMaxLength(128);
            Property(x => x.Desde).HasColumnType("varchar").HasMaxLength(20);
            Property(x => x.Garantias).HasColumnType("varchar").HasMaxLength(128);
            Property(x => x.Comentarios).HasColumnType("varchar").HasMaxLength(256);

        }
    }
}
