using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationContaClienteVisitas : EntityTypeConfiguration<ContaClienteVisita>
    {
        public ConfigurationContaClienteVisitas()
        {
            ToTable("ContaClienteVisita");
            HasKey(c => c.ID);

            Property(x => x.Parecer).HasColumnType("text").IsMaxLength();
            Property(c => c.EmpresasID).HasColumnType("char").HasMaxLength(1);
        }
    }
}
