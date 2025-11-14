using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationContaClienteComentario : EntityTypeConfiguration<ContaClienteComentario>
    {
        public ConfigurationContaClienteComentario()
        {
            ToTable("ContaClienteComentario");
            HasKey(x => x.ID);

            Property(x => x.ContaClienteID).IsRequired();
            Property(x => x.UsuarioID).IsRequired();
            Property(x => x.Descricao).IsRequired().HasMaxLength(1000);
            Property(x => x.Ativo).IsRequired();

            //Campo ignorado não terá alterações de comentarios.
            Ignore(x => x.UsuarioIDAlteracao);
            Ignore(x => x.UsuarioIDCriacao);
        }
    }
}
