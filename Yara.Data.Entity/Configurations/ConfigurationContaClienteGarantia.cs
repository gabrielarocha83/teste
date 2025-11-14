using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationContaClienteGarantia : EntityTypeConfiguration<ContaClienteGarantia>
    {
        public ConfigurationContaClienteGarantia()
        {
            ToTable("ContaClienteGarantia");
            HasKey(c => c.ID);

            Property(c => c.Motivo).HasColumnType("text").IsMaxLength();
            Property(c => c.Observacao).HasColumnType("text").IsMaxLength();
            Property(c => c.Registro).HasColumnType("text").IsMaxLength();
            Property(c => c.Laudo).HasColumnType("text").IsMaxLength();

            Property(x => x.TipoImovel).HasColumnType("char").HasMaxLength(1);
            Property(x => x.TipoGarantia).HasColumnType("varchar").HasMaxLength(3);

            Property(c => c.OutrasGarantias).HasColumnType("text").IsMaxLength();
            Property(c => c.DescricaoOutros).HasColumnType("text").IsMaxLength();

            Property(c => c.EmpresasID).HasColumnType("char").HasMaxLength(1);
        }
    }
}
