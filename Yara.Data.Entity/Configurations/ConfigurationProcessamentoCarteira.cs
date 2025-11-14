using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationProcessamentoCarteira : EntityTypeConfiguration<ProcessamentoCarteira>
    {
        public ConfigurationProcessamentoCarteira()
        {
            ToTable("ProcessamentoCarteira");
            HasKey(x => x.ID);
            Property(c => c.EmpresaID).HasMaxLength(1).IsRequired();
            Property(c => c.Cliente).HasMaxLength(10).IsRequired();
            Property(c => c.DataHora).IsRequired();
            Property(c => c.Status).IsRequired();
            Property(c => c.Motivo).HasColumnType("varchar").IsMaxLength();
            Property(c => c.Detalhes).HasColumnType("varchar").IsMaxLength();
            Property(c => c.OrdemVenda).HasColumnType("char").HasMaxLength(10);
        }
    }
}
