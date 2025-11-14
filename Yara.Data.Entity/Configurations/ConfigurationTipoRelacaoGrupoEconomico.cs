using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationTipoRelacaoGrupoEconomico : EntityTypeConfiguration<TipoRelacaoGrupoEconomico>
    {
        public ConfigurationTipoRelacaoGrupoEconomico()
        {
            ToTable("TipoRelacaoGrupoEconomico");
            HasKey(x => x.ID);
            Property(c => c.Nome).IsRequired();
        }
    }
}
