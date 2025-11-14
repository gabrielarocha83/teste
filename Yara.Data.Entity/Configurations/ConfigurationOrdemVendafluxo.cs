using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationOrdemVendafluxo : EntityTypeConfiguration<OrdemVendaFluxo>
    {
        public ConfigurationOrdemVendafluxo()
        {
            ToTable("OrdemVendaFluxo");
            HasKey(x => x.ID);

            Property(c => c.SolicitanteFluxoID).IsRequired();
            Property(c => c.Divisao).IsRequired();
            Property(c => c.ItemOrdemVenda).IsRequired();
            Property(c => c.OrdemVendaNumero).IsRequired();
           
            Property(x => x.EmpresasId).HasColumnType("char").HasMaxLength(1);
            

        }
    }
}
