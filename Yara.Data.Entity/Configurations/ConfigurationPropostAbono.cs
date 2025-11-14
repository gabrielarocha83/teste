using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostAbono : EntityTypeConfiguration<PropostaAbono>
    {
        public ConfigurationPropostAbono()
        {
            ToTable("PropostaAbono");
            HasKey(c => c.ID);
            Property(c => c.PropostaCobrancaStatusID).HasColumnType("char").HasMaxLength(2);
            Property(c => c.EmpresaID).HasColumnType("char").HasMaxLength(1);
            Property(c => c.CodigoSap).HasMaxLength(10);
            Property(x => x.ParecerComercial).HasColumnType("text").IsMaxLength();
            Property(x => x.ParecerCobranca).HasColumnType("text").IsMaxLength();
            Property(x => x.Motivo).HasColumnType("text").IsMaxLength();

        }
    }
}
