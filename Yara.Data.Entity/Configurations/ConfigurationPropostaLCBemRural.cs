using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCBemRural : EntityTypeConfiguration<PropostaLCBemRural>
    {

        public ConfigurationPropostaLCBemRural()
        {

            ToTable("PropostaLCBemRural");
            HasKey(x => x.ID);

            Property(x => x.IR).HasColumnType("varchar").HasMaxLength(50);

        }

    }
}
