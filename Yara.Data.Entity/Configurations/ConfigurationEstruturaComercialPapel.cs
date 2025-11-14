using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationEstruturaComercialPapel : EntityTypeConfiguration<EstruturaComercialPapel>
    {
        public ConfigurationEstruturaComercialPapel()
        {
            ToTable("EstruturaComercialPapel");
            Property(x => x.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None).HasColumnType("char").HasMaxLength(1);
            Property(c => c.Papel).IsRequired();
            Property(c => c.Ativo).IsRequired();

        }
    }
}
