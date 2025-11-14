using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    internal class ConfigurationGrupo : EntityTypeConfiguration<Grupo>
    {
        public ConfigurationGrupo()
        {
            ToTable("Grupo").HasMany<Permissao>(x => x.Permissoes)
           .WithMany(x => x.Grupos)
           .Map(x =>
           {
               x.ToTable("Grupo_Permissao");
               x.MapLeftKey("GrupoID");
               x.MapRightKey("PermissaoID");
           });
            HasKey(x => x.ID);
            Property(c => c.Nome).IsRequired();
        }
    }
}