using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationTituloComentario : EntityTypeConfiguration<TituloComentario>
    {
        public ConfigurationTituloComentario()
        {
            ToTable("TituloComentario");
            HasKey(c => c.ID);

            Property(c => c.NumeroDocumento).HasMaxLength(10).HasColumnType("char").IsRequired();
            Property(c => c.Linha).HasMaxLength(3).HasColumnType("char").IsRequired();
            Property(c => c.AnoExercicio).HasMaxLength(4).HasColumnType("char").IsRequired();
            Property(c => c.Empresa).HasMaxLength(4).HasColumnType("char").IsRequired();

            Property(x => x.Texto).HasColumnType("text").IsMaxLength();
        }
    }
}
