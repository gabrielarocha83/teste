using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLCParceriaAgricola : EntityTypeConfiguration<PropostaLCParceriaAgricola>
    {

        public ConfigurationPropostaLCParceriaAgricola()
        {
            ToTable("PropostaLCParceriaAgricola");
            HasKey(c => c.ID);

            Property(x => x.Documento).HasColumnType("varchar").HasMaxLength(20);
            Property(x => x.InscricaoEstadual).HasColumnType("varchar").HasMaxLength(40);
            Property(x => x.Nome).HasColumnType("varchar").HasMaxLength(128);
        }


        



    }
}
