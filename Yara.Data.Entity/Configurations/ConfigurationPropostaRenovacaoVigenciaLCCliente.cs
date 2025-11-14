using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaRenovacaoVigenciaLCCliente : EntityTypeConfiguration<PropostaRenovacaoVigenciaLCCliente>
    {
        public ConfigurationPropostaRenovacaoVigenciaLCCliente()
        {
            ToTable("PropostaRenovacaoVigenciaLCCliente");
            HasKey(c => c.ID);
            Property(c => c.NomeCliente).IsRequired();
            Property(c => c.CodigoCliente).IsRequired().HasMaxLength(10);
            //Property(c => c.Documento).IsRequired().HasMaxLength(24);

            Property(c => c.TipoCliente).HasMaxLength(50);
            //Property(c => c.NomeGrupo).IsOptional();
            //Property(c => c.ClassificacaoGrupo).IsOptional();
            Property(c => c.DataVigenciaLC).IsOptional();
            //Property(c => c.Top10).IsOptional();
            Property(c => c.DataConsultaSerasa).IsOptional();
            //Property(c => c.RestricaoSerasa).IsOptional();
            //Property(c => c.RestricaoYara).IsOptional();
            //Property(c => c.RestricaoSerasaGrupo).IsOptional();
            //Property(c => c.RestricaoYaraGrupo).IsOptional();
            Property(c => c.CodigoPropostaLC).HasMaxLength(12);
            Property(c => c.PropostaLCStatus).HasMaxLength(120);
            Property(c => c.CodigoPropostaAC).HasMaxLength(12);
            Property(c => c.PropostaACStatus).HasMaxLength(120);
            Property(c => c.DataUltimaCompra).IsOptional();
            Property(c => c.DataValidadeGarantia).IsOptional();

            Property(c => c.Apto).IsRequired();
        }
    }
}
