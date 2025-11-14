using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationItemOrdemVenda : EntityTypeConfiguration<ItemOrdemVenda>
    {
        public ConfigurationItemOrdemVenda()
        {
            ToTable("ItemOrdemVenda");
            Property(c => c.Item).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            HasKey(c => new {c.Item, c.OrdemVendaNumero});
           
            Property(c => c.OrdemVendaNumero).HasMaxLength(10).HasColumnType("varchar").IsRequired();
            Property(c => c.Material).HasMaxLength(18).HasColumnType("varchar").IsRequired();
            Property(c => c.Centro).HasMaxLength(4).HasColumnType("varchar");
            Property(c => c.Deposito).HasMaxLength(4).HasColumnType("varchar");
            Property(c => c.CondPagto).HasMaxLength(4).HasColumnType("char");
            Property(c => c.CondFrete).HasMaxLength(3).HasColumnType("char");
            Property(c => c.Moeda).HasMaxLength(4).HasColumnType("varchar");
            Property(c => c.TaxaCambio).HasPrecision(9, 5);
            Property(c => c.MotivoRecusa).HasMaxLength(2).HasColumnType("varchar");
            Property(c => c.QtdPedida).HasPrecision(15, 3).IsRequired();
            Property(c => c.QtdEntregue).HasPrecision(15, 3).IsRequired();
            Property(c => c.QtdDelivery).HasPrecision(15, 3).IsRequired();
            Property(c => c.UnidadeMedida).HasMaxLength(3).HasColumnType("varchar");
            Property(c => c.ValorUnitario).HasPrecision(13, 2).IsRequired();
            Property(c => c.DescricaoMaterial).HasMaxLength(100).HasColumnType("varchar");
            Property(c => c.OutrosBloqueios).HasMaxLength(255).HasColumnType("varchar");
            Property(c => c.CotacaoMoeda).HasColumnType("decimal").HasPrecision(9, 5);
            Property(c => c.CodigoCulturaSAP).HasMaxLength(3).HasColumnType("varchar");
            Property(c => c.DesricaoCultura).HasMaxLength(20).HasColumnType("varchar");

    }
}
}
