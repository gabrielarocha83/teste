using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationPropostaLC : EntityTypeConfiguration<PropostaLC>
    {
        public ConfigurationPropostaLC()
        {
            ToTable("PropostaLC");
            HasKey(c => c.ID);

            Property(x => x.EmpresaID).HasColumnType("char").HasMaxLength(1).IsRequired();
            Property(x => x.EstadoCivil).HasColumnType("varchar").HasMaxLength(50);
            Property(x => x.CPFConjugue).HasColumnType("varchar").HasMaxLength(14);
            Property(x => x.NomeConjugue).HasColumnType("varchar").HasMaxLength(128);
            Property(x => x.RegimeCasamento).HasColumnType("varchar").HasMaxLength(50);
            Property(x => x.Trading).HasColumnType("varchar").HasMaxLength(256);
            Property(x => x.PrincipaisFornecedores).HasColumnType("varchar").HasMaxLength(256);
            Property(x => x.PrincipaisClientes).HasColumnType("varchar").HasMaxLength(256);
            Property(x => x.RestricaoSERASA).HasColumnType("varchar").HasMaxLength(20);
            Property(x => x.RestricaoTJ).HasColumnType("varchar").HasMaxLength(20);
            Property(x => x.RestricaoIBAMA).HasColumnType("varchar").HasMaxLength(20);
            Property(x => x.RestricaoTrabalhoEscravo).HasColumnType("varchar").HasMaxLength(20);
            Property(x => x.FonteRecursosCarteira).HasColumnType("varchar").HasMaxLength(256);
            Property(x => x.PropostaLCStatusID).HasColumnType("varchar").HasMaxLength(2);
            Property(c => c.CodigoSap).HasMaxLength(10);
            Property(c => c.Documento).HasColumnType("varchar").HasMaxLength(280);
            Property(c => c.DescricaoRestricao).HasColumnType("varchar").HasMaxLength(256);
            Property(x => x.ParecerRepresentante).HasColumnType("text").IsMaxLength();
            Property(x => x.ParecerCTC).HasColumnType("text").IsMaxLength();
            Property(x => x.ComentarioPatrimonio).HasColumnType("text").IsMaxLength();
            Property(x => x.ComentarioMercado).HasColumnType("text").IsMaxLength();
            Property(c => c.ParecerAnalista).HasColumnType("text").IsMaxLength();
        }
    }
}
