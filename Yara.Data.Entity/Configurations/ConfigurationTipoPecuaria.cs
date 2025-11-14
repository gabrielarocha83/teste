using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    internal class ConfigurationTipoPecuaria : EntityTypeConfiguration<TipoPecuaria>
    {
        public ConfigurationTipoPecuaria()
        {
            ToTable("TipoPecuaria");
            HasKey(x => x.ID);
            Property(c => c.Tipo).IsRequired();
            Property(c => c.UnidadeMedida).IsRequired();
        }
    }
}