using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostAbonoComite : EntityTypeConfiguration<PropostaAbonoComite>
    {
        public ConfigurationPropostAbonoComite()
        {
            ToTable("PropostaAbonoComite");
            HasKey(c => c.ID);
            Property(c => c.Comentario).HasColumnType("text").IsMaxLength();
            

        }
    }
}
