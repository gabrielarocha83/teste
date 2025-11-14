using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationHistoricoContaCliente : EntityTypeConfiguration<HistoricoContaCliente>
    {
        public ConfigurationHistoricoContaCliente()
        {
            ToTable("HistoricoContaCliente");
            HasKey(c => c.ID);
            Property(x => x.EmpresasID).HasColumnType("char").HasMaxLength(1);
        }
    }
}
