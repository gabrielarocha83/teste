using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationContaCliente : EntityTypeConfiguration<ContaCliente>
    {
        public ConfigurationContaCliente()
        {
            HasKey(x => x.ID);

            Property(c => c.Nome).IsRequired();
            Property(c => c.Documento).IsRequired().HasMaxLength(24);
            Property(c => c.DataCriacao).IsRequired();
            Property(c => c.UsuarioIDCriacao).IsRequired();

            Property(c => c.CodigoPrincipal).HasMaxLength(10);
            Property(c => c.Numero).IsOptional().HasMaxLength(64);
            Property(c => c.Complemento).IsOptional().HasMaxLength(64);
            Property(c => c.Bairro).IsOptional();
            Property(c => c.BloqueioManual).IsOptional();
            Property(c => c.LiberacaoManual).IsOptional();
            Property(c => c.RestricaoSerasa).IsOptional();
            Property(c => c.ClientePremium).IsOptional();
            Property(c => c.AdiantamentoLC).IsOptional();
            Property(c => c.TOP10).IsOptional();

            Property(c => c.Segmentacao).IsOptional().HasMaxLength(255);
            Property(c => c.Categorizacao).IsOptional().HasMaxLength(255);
        }
    }
}
