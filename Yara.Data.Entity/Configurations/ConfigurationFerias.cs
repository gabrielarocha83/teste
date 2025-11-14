using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationFerias : EntityTypeConfiguration<Ferias>
    {
        public ConfigurationFerias()
        {
            ToTable("Ferias");
            HasKey(x => x.ID);
            Property(c => c.FeriasInicio).IsRequired();
            Property(c => c.FeriasFim).IsRequired();
            Property(c => c.UsuarioID).IsRequired();
            Property(c => c.SubstitutoID).IsRequired();
        }
    }
}
