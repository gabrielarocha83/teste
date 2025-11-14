using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationTribunalJustica : EntityTypeConfiguration<TribunalJustica>
    {
        public ConfigurationTribunalJustica()
        {
            ToTable("TribunalJustica");
            HasKey(c => c.Documento);
            Property(x => x.DataCriacao).IsRequired();
        }
    }
}
