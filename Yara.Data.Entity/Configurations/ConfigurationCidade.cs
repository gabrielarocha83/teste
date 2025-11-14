using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    internal class ConfigurationCidade : EntityTypeConfiguration<Cidade>
    {
        public ConfigurationCidade()
        {
            ToTable("Cidade");
            HasKey(x => x.ID);
          
        }
    }
}
