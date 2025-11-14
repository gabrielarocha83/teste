using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPerfil : EntityTypeConfiguration<Perfil>
    {
        public ConfigurationPerfil()
        {
            ToTable("Perfil");
            HasKey(c => c.ID);
            Property(x => x.Descricao).IsRequired().HasMaxLength(50);
        }
    }
}
