using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCBemUrbano : EntityTypeConfiguration<PropostaLCBemUrbano>
    {

        public ConfigurationPropostaLCBemUrbano()
        {

            ToTable("PropostaLCBemUrbano");
            HasKey(x => x.ID);

            Property(x => x.IR).HasColumnType("varchar").HasMaxLength(50);
            Property(x => x.Descricao).HasColumnType("varchar").HasMaxLength(128);

        }

    }
}
