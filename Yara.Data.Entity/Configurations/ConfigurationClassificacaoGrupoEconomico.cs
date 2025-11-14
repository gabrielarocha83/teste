using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationClassificacaoGrupoEconomico : EntityTypeConfiguration<ClassificacaoGrupoEconomico>
    {
        public ConfigurationClassificacaoGrupoEconomico()
        {
            ToTable("ClassificacaoGrupoEconomico");
            HasKey(x => x.ID);
            Property(c => c.Nome).IsRequired();
        }
    }
}
