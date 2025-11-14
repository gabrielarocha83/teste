using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    internal class ConfigurationCambio : EntityTypeConfiguration<Cambio>
    {
        public ConfigurationCambio()
        {
            ToTable("Cambio");
            HasKey(x => new { x.InicioValidade, x.MoedaDe, x.MoedaPara });
            Property(c => c.MoedaDe).HasColumnType("varchar").HasMaxLength(5).IsRequired();
            Property(c => c.MoedaPara).HasColumnType("varchar").HasMaxLength(5).IsRequired();
            Property(c => c.Taxa).HasColumnType("decimal").HasPrecision(9, 5);
        }
    }
}
