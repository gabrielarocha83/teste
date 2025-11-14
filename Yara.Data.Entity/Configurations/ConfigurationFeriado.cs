using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationFeriado : EntityTypeConfiguration<Feriado>
    {
        public ConfigurationFeriado()
        {
            ToTable("Feriado");
            HasKey(x => x.ID);
            Property(c => c.DataFeriado).IsRequired();
            Property(c => c.Descricao).HasMaxLength(50).IsRequired();
        }
    }
}
