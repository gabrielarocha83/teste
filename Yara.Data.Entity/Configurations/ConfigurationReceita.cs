using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationReceita : EntityTypeConfiguration<Receita>
    {
        public ConfigurationReceita()
        {
            ToTable("Receita");
            HasKey(c => c.ID);
            Property(x => x.Descricao).IsRequired().HasMaxLength(50);
        }
    }
}
