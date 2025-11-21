using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationGrupoEconomicoMembro : EntityTypeConfiguration<GrupoEconomicoMembros>
    {
        public ConfigurationGrupoEconomicoMembro()
        {
            ToTable("GrupoEconomicoMembro");
            HasKey(c => new {c.ContaClienteID, c.GrupoEconomicoID});
            Property(c => c.StatusGrupoEconomicoFluxoID).HasMaxLength(3).HasColumnType("varchar").IsRequired();
            Property(c => c.ExplodeGrupo)
                .HasColumnType("bit")
                .IsRequired();
        }
    }
}
