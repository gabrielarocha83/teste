using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationTipoEmpresa : EntityTypeConfiguration<TipoEmpresa>
    {
        public ConfigurationTipoEmpresa()
        {
            ToTable("TipoEmpresa");
            HasKey(c => c.ID);
            Property(c => c.Tipo).IsRequired().HasMaxLength(50);
        }
    }
}
