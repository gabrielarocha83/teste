using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationGrupoEconomico : EntityTypeConfiguration<GrupoEconomico>
    {
        public ConfigurationGrupoEconomico()
        {
            ToTable("GrupoEconomico");
            HasKey(x => x.ID);
            Property(c => c.CodigoGrupo).HasMaxLength(10).IsRequired();
            Property(c => c.Nome).IsRequired();
            Property(c => c.Descricao).IsRequired();
            Property(c => c.StatusGrupoEconomicoFluxoID).HasMaxLength(3).HasColumnType("varchar").IsRequired();
            Property(c => c.EmpresasID).HasColumnType("char").HasMaxLength(1);
        }
    }
}
