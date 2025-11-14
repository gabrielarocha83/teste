using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCAdicional : EntityTypeConfiguration<PropostaLCAdicional>
    {
        public ConfigurationPropostaLCAdicional()
        {
            ToTable("PropostaLCAdicional");
            HasKey(c => c.ID);

            //Property(c => c.NumeroInternoProposta).IsRequired();
            Property(c => c.EmpresaID).HasColumnType("char").HasMaxLength(1).IsRequired();
            //Property(c => c.LCAdicional).IsRequired();
            //Property(c => c.VigenciaAdicional).IsRequired();
            Property(c => c.Parecer).HasColumnType("text").IsMaxLength().IsRequired();
            //Property(c => c.AcompanharProposta).IsRequired();
            Property(x => x.PropostaLCStatusID).HasColumnType("varchar").HasMaxLength(2);
            Property(c => c.CodigoSap).HasMaxLength(10).IsRequired();
        }
    }
}
