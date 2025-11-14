using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaProrrogacaoComite : EntityTypeConfiguration<PropostaProrrogacaoComite>
    {
        public ConfigurationPropostaProrrogacaoComite()
        {
            ToTable("PropostaProrrogacaoComite");
            HasKey(c => c.ID);
            Property(c => c.Comentario).HasColumnType("text").IsMaxLength();
        }
    }
}
