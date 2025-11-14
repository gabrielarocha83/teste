using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationRepresentante : EntityTypeConfiguration<Representante>
    {
        public ConfigurationRepresentante()
        {
            ToTable("Representante");
            HasKey(x => x.ID);

            Property(c => c.CodigoSap).HasMaxLength(10).IsRequired();
            Property(c => c.Nome).IsRequired();
        }
    }
}
