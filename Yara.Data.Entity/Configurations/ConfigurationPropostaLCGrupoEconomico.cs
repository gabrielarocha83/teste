using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCGrupoEconomico : EntityTypeConfiguration<PropostaLCGrupoEconomico>
    {
        public ConfigurationPropostaLCGrupoEconomico()
        {
            ToTable("PropostaLCGrupoEconomico");
            HasKey(c =>new {c.PropostaLCID, c.Documento});

            Property(x => x.Documento).HasColumnType("varchar").HasMaxLength(20);
        }
    }
}
