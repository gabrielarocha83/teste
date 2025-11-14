using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationCustoHaRegiao : EntityTypeConfiguration<CustoHaRegiao>
    {
        public ConfigurationCustoHaRegiao()
        {
            ToTable("CustoHaRegiao");
            HasKey(c => c.ID);
            Property(x => x.CidadeID).IsRequired();
        }
    }
}
