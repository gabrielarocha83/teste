using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCPrecoPorRegiao : EntityTypeConfiguration<PropostaLCPrecoPorRegiao>
    {

        public ConfigurationPropostaLCPrecoPorRegiao()
        {

            ToTable("PropostaLCPrecoPorRegiao");
            HasKey(x => x.ID);
            Property(c => c.Documento).HasMaxLength(120).HasColumnType("varchar");


        }

    }
}
