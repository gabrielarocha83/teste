using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationSolicitanteFluxo : EntityTypeConfiguration<SolicitanteFluxo>
    {
        public ConfigurationSolicitanteFluxo()
        {
            ToTable("SolicitanteFluxo");
            HasKey(x => x.ID);
            Property(c => c.Comentario).HasColumnType("text").IsMaxLength();
            Property(x => x.EmpresasId).HasColumnType("char").HasMaxLength(1);
            Property(c => c.AcompanharProposta).IsRequired();
        }
    }
}
