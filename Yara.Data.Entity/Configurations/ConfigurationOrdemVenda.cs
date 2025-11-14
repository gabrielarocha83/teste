using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationOrdemVenda : EntityTypeConfiguration<OrdemVenda>
    {
        public ConfigurationOrdemVenda()
        {
            ToTable("OrdemVenda");
            HasKey(c => c.Numero);

            Property(c => c.Numero).HasMaxLength(10).HasColumnType("varchar").IsRequired();
            Property(c => c.Tipo).HasMaxLength(4).HasColumnType("char").IsRequired();
            Property(c => c.Canal).HasMaxLength(2).HasColumnType("char").IsRequired();
            Property(c => c.Setor).HasMaxLength(2).HasColumnType("char").IsRequired();
            Property(c => c.OrgVendas).HasMaxLength(4).HasColumnType("char").IsRequired();
            Property(c => c.CodigoCtc).HasMaxLength(3).HasColumnType("char");
            Property(c => c.CodigoGc).HasMaxLength(4).HasColumnType("char");
            Property(c => c.Emissor).HasMaxLength(10).HasColumnType("varchar").IsRequired();
            Property(c => c.Pagador).HasMaxLength(10).HasColumnType("varchar");
            Property(c => c.Representante).HasMaxLength(10).HasColumnType("varchar");
            Property(c => c.CondPagto).HasMaxLength(4).HasColumnType("char");
            Property(c => c.CondFrete).HasMaxLength(3).HasColumnType("char");
            Property(c => c.PedidoCliente).HasMaxLength(20).HasColumnType("varchar");
            Property(c => c.Moeda).HasMaxLength(5).HasColumnType("varchar");
            Property(c => c.TaxaCambio).HasPrecision(9,5);
            Property(c => c.Cultura).HasMaxLength(3).HasColumnType("varchar");
            Property(c => c.BloqueioRemessa).HasMaxLength(2).HasColumnType("varchar");
            Property(c => c.BloqueioFaturamento).HasMaxLength(2).HasColumnType("varchar");
            //Property(c => c.DataEfetivaFixa).IsRequired();
            Property(c => c.DataEmissao).IsRequired();
            //Property(c => c.DataModificacao).IsRequired();
            Property(c => c.UltimaAtualizacao).IsRequired();
            Property(c => c.UsuarioIdCriacao).IsRequired();
            Property(c => c.DataCriacao).IsRequired();

            
        }
    }
}
