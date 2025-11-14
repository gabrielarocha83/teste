using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationLiberacaoOrdemVendaFluxo : EntityTypeConfiguration<LiberacaoOrdemVendaFluxo>
    {
        public ConfigurationLiberacaoOrdemVendaFluxo()
        {
            ToTable("LiberacaoOrdemVendaFluxo");
            HasKey(x => x.ID);
            Property(c => c.EmpresasId).HasColumnType("char").HasMaxLength(1);
            Property(c => c.CodigoSap).HasMaxLength(10);
            Property(c => c.Comentario).HasMaxLength(500);
        }
    }
}
