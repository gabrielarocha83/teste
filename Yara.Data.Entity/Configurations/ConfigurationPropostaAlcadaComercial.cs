using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaAlcadaComercial : EntityTypeConfiguration<PropostaAlcadaComercial>
    {
        public ConfigurationPropostaAlcadaComercial()
        {
            ToTable("PropostaAlcadaComercial");
            HasKey(c => c.ID);
            Property(c => c.EmpresaID).HasColumnType("char").HasMaxLength(1);
            Property(c => c.CodigoSap).HasMaxLength(10);
            Property(x => x.ParecerCTC).HasColumnType("text").IsMaxLength();
            Property(x => x.ParecerCredito).HasColumnType("text").IsMaxLength();
            Property(x => x.ParecerRepresentante).HasColumnType("text").IsMaxLength();
            Property(x => x.Comentario).HasColumnType("text").IsMaxLength();
            Property(c => c.PropostaCobrancaStatusID).HasColumnType("char").HasMaxLength(2);

        }
    }
}
