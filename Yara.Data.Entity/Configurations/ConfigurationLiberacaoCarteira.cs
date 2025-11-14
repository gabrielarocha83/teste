using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationBloqueioLiberacaoCarteira : EntityTypeConfiguration<BloqueioLiberacaoCarteira>
    {
        public ConfigurationBloqueioLiberacaoCarteira()
        {
            ToTable("BloqueioLiberacaoCarteira");
            HasKey(c =>c.ID);
            Property(c => c.Numero).HasColumnType("varchar").HasMaxLength(10).IsRequired();
            Property(c => c.MotivoB7).HasMaxLength(10).HasColumnType("varchar");
        }
    }
}
