using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    internal class ConfigurationSolicitanteSerasa : EntityTypeConfiguration<SolicitanteSerasa>
    {
        public ConfigurationSolicitanteSerasa()
        {
            ToTable("SolicitanteSerasa");
            HasKey(x => x.ID);
            Property(c => c.Json).HasColumnType("text").IsMaxLength();
            Property(c => c.MotivoConsulta).HasColumnType("varchar").HasMaxLength(100);
        }
    }
}
