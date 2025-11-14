using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationContaClienteTelefone : EntityTypeConfiguration<ContaClienteTelefone>
    {
        public ConfigurationContaClienteTelefone()
        {
            ToTable("ContaClienteTelefone");
            HasKey(x => x.ID);
            Property(c => c.ContaClienteID).IsRequired();
            Property(c => c.Telefone).IsRequired().HasMaxLength(24);
            Property(c => c.Tipo).IsRequired();
          
        }
    }
}
