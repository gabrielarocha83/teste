using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationLog : EntityTypeConfiguration<Log>
    {
        public ConfigurationLog()
        {
            ToTable("Log");
            Property(c => c.Descricao).HasMaxLength(255);
            Property(c => c.Navegador).HasMaxLength(255);
            HasKey(x => x.ID);
            Ignore(x => x.UsuarioIDAlteracao);
            Ignore(x => x.UsuarioIDCriacao);
        }
    }
}