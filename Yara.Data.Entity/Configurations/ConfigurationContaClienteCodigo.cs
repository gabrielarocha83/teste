using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationContaClienteCodigo : EntityTypeConfiguration<ContaClienteCodigo>
    {
        public ConfigurationContaClienteCodigo()
        {
            ToTable("ContaClienteCodigo");
            HasKey(x => x.ID);
            Property(c => c.ContaClienteID).IsRequired();
            Property(c => c.CodigoPrincipal).IsRequired();
            Property(c => c.Codigo).IsRequired().HasMaxLength(10);
            Property(c => c.Documento).IsRequired().HasMaxLength(24);
        }
    }
}
