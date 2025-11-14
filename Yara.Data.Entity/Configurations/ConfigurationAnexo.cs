using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationAnexo : EntityTypeConfiguration<Anexo>
    {
        public ConfigurationAnexo()
        {
            ToTable("Anexo");
            HasKey(c => c.ID);
            Property(x => x.Descricao).IsRequired().HasMaxLength(512);
            Property(x => x.LayoutsProposta).HasMaxLength(255);
        }
    }
}
