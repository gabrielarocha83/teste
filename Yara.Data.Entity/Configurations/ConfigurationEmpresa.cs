using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationEmpresa : EntityTypeConfiguration<Empresas>
    {
        public ConfigurationEmpresa()
        {
            ToTable("Empresa");
            HasKey(c => c.ID);
            Property(x => x.ID).HasColumnType("char").HasMaxLength(1);


        }
    }
}
