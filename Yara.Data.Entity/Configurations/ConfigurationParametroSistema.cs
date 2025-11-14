using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationParametroSistema : EntityTypeConfiguration<ParametroSistema>
    {
        public ConfigurationParametroSistema()
        {
            ToTable("ParametroSistema");
            HasKey(x => x.ID);
            Property(c => c.Grupo).IsRequired();
            Property(c => c.Tipo).IsRequired();
            //Property(c => c.Chave).IsRequired();
            Property(c => c.Valor).IsRequired();
            Property(c => c.Ativo).IsRequired();

            Property(c => c.EmpresasID).HasColumnType("char").HasMaxLength(1);
        }
    }
}
