using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    internal class ConfigurationPermissao : EntityTypeConfiguration<Permissao>
    {
        public ConfigurationPermissao()
        {

            ToTable("Permissao");
            HasKey(x => x.Nome);
            Property(x => x.Nome).HasMaxLength(50).IsRequired();
            Property(x => x.Descricao).HasMaxLength(100);
            Property(x => x.Processo).HasMaxLength(100);

        }
    }
}