using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCMaquinaEquipamento : EntityTypeConfiguration<PropostaLCMaquinaEquipamento>
    {

        public ConfigurationPropostaLCMaquinaEquipamento()
        {

            ToTable("PropostaLCMaquinaEquipamento");
            HasKey(x => x.ID);

            Property(x => x.Descricao).HasColumnType("varchar").HasMaxLength(128);

        }

    }
}
