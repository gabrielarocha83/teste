using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationEstruturaComercial : EntityTypeConfiguration<EstruturaComercial>
    {
        public ConfigurationEstruturaComercial()
        {
            ToTable("EstruturaComercial");
            HasKey(x => x.CodigoSap);

            Property(c => c.EstruturaComercialPapelID).IsRequired().HasColumnType("char").HasMaxLength(1);
            Property(c => c.CodigoSap).HasMaxLength(10);
            
            Property(x => x.Nome).IsRequired();
            Property(x => x.Ativo).IsRequired();
        }
    }
}
