using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCMercado : EntityTypeConfiguration<PropostaLCMercado>
    {

        public ConfigurationPropostaLCMercado()
        {
            ToTable("PropostaLCMercado");
            HasKey(c => c.ID);
        }

    }
}
