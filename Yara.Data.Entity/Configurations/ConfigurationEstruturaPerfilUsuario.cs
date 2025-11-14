using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationEstruturaPerfilUsuario : EntityTypeConfiguration<EstruturaPerfilUsuario>
    {
        public ConfigurationEstruturaPerfilUsuario()
        {
            ToTable("EstruturaPerfilUsuario");
            HasKey(c => c.ID);

            Property(c => c.CodigoSap).HasMaxLength(10).IsRequired();
            Property(c => c.PerfilId).IsRequired();
        }
    }
}
