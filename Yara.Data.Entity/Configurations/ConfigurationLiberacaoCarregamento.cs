using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationBloqueioLiberacaoCarregamento : EntityTypeConfiguration<BloqueioLiberacaoCarregamento>
    {
        public ConfigurationBloqueioLiberacaoCarregamento()
        {
            ToTable("BloqueioLiberacaoCarregamento");
            HasKey(c =>c.ID);

         
            
        }
    }
}
