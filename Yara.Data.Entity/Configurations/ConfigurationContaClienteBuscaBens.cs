using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationContaClienteBuscaBens : EntityTypeConfiguration<ContaClienteBuscaBens>
    {
        public ConfigurationContaClienteBuscaBens()
        {
            ToTable("ContaClienteBuscaBens");
            HasKey(c => c.ID);

            Property(x => x.Patrimonio).HasColumnType("text").IsMaxLength();
            Property(c => c.EmpresasID).HasColumnType("char").HasMaxLength(1);
        }
    }
}
