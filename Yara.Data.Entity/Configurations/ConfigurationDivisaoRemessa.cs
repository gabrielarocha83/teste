using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationDivisaoRemessa : EntityTypeConfiguration<DivisaoRemessa>
    {
        public ConfigurationDivisaoRemessa()
        {
            ToTable("DivisaoRemessa");
            Property(c => c.Divisao).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            HasKey(c => new { c.Divisao, c.ItemOrdemVendaItem, c.OrdemVendaNumero });

            Property(c => c.OrdemVendaNumero).HasMaxLength(10).HasColumnType("varchar").IsRequired();
            Property(c => c.QtdPedida).HasPrecision(15, 3).IsRequired();
            Property(c => c.QtdEntregue).HasPrecision(15, 3).IsRequired();
            Property(c => c.UnidadeMedida).HasMaxLength(3).HasColumnType("varchar");
            Property(c => c.Status).HasMaxLength(2).HasColumnType("varchar");
            Property(c => c.Motivo).HasMaxLength(3).HasColumnType("varchar");
            Property(c => c.Bloqueio).HasMaxLength(2).HasColumnType("varchar");
            Property(c => c.BloqueioPortal).HasMaxLength(2).HasColumnType("varchar");
            Property(c => c.MotivoB7).HasMaxLength(10).HasColumnType("varchar");
            HasRequired(c => c.ItemOrdemVenda).WithMany(c => c.DivisaoRemessas).HasForeignKey(c => new {c.ItemOrdemVendaItem, c.OrdemVendaNumero});
            
        }
    }
}
