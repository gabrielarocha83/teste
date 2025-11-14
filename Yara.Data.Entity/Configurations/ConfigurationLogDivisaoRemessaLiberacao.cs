using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationLogDivisaoRemessaLiberacao : EntityTypeConfiguration<LogDivisaoRemessaLiberacao>
    {
        public ConfigurationLogDivisaoRemessaLiberacao()
        {
            ToTable("LogDivisaoRemessaLiberacao");
            HasKey(x => x.ID);
            Property(c => c.OrdemVendaNumero).HasColumnType("varchar").HasMaxLength(10).IsRequired();
            Property(p => p.Restricao).HasColumnType("varchar").IsMaxLength();
        }
    }
}
