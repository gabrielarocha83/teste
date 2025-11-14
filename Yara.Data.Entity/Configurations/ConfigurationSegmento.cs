using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationSegmento : EntityTypeConfiguration<Segmento>
    {
        public ConfigurationSegmento()
        {
            ToTable("Segmento");
            HasKey(x => x.ID);
            Property(x => x.Descricao).IsRequired();
            Property(x => x.Ativo).IsRequired();
            Property(x => x.DataCriacao).IsRequired();
            Property(x => x.UsuarioIDCriacao).IsRequired();
        }
    }
}
