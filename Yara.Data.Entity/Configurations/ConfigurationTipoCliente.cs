using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    internal class ConfigurationTipoCliente : EntityTypeConfiguration<TipoCliente>
    {
        public ConfigurationTipoCliente()
        {
            ToTable("TipoCliente");
            HasKey(x => x.ID);
            Property(c => c.Nome).IsRequired().HasMaxLength(50);
        }
    }
}