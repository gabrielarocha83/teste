using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationUsuario : EntityTypeConfiguration<Usuario>
    {
        public ConfigurationUsuario()
        {
            ToTable("Usuario").HasMany<Grupo>(x => x.Grupos)
            .WithMany(x => x.Usuarios)
            .Map(x =>
            {
                x.ToTable("Usuario_Grupo");
                x.MapLeftKey("UsuarioID");
                x.MapRightKey("GrupoID");
            });

            ToTable("Usuario").HasMany<Representante>(x => x.Representantes)
            .WithMany(x => x.Usuarios)
            .Map(x =>
            {
                x.ToTable("Usuario_Representante");
                x.MapLeftKey("UsuarioID");
                x.MapRightKey("RepresentanteID");
            });

            ToTable("Usuario").HasMany<EstruturaComercial>(x => x.EstruturasComerciais)
            .WithMany(x => x.Usuarios)
            .Map(x =>
            {
                x.ToTable("Usuario_EstruturaComercial");
                x.MapLeftKey("UsuarioID");
                x.MapRightKey("CodigoSap");
            });
            HasKey(x => x.ID);

            Property(x => x.Nome).IsRequired();
            Property(x => x.Login).IsRequired();
            Property(x => x.TipoAcesso).IsRequired();
            Property(x => x.Email).IsRequired();
            Property(x => x.EmpresaLogada).HasColumnType("char").HasMaxLength(1);
            // Property(x => x.TokenID).IsRequired();
            Property(x => x.EmpresasID).HasColumnType("char").HasMaxLength(1);
            Ignore(x => x.UsuarioIDAlteracao);
            Ignore(x => x.UsuarioIDCriacao);
        }
    }
}
