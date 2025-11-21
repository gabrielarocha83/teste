using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationTitulo : EntityTypeConfiguration<Titulo>
    {
        public ConfigurationTitulo()
        {
            ToTable("Titulo");
            HasKey(t => new { t.NumeroDocumento, t.Linha, t.AnoExercicio, t.Empresa });

            Property(c => c.NumeroDocumento).HasMaxLength(10).HasColumnType("char").IsRequired();
            Property(c => c.Linha).HasMaxLength(3).HasColumnType("char").IsRequired();
            Property(c => c.AnoExercicio).HasMaxLength(4).HasColumnType("char").IsRequired();
            Property(c => c.Empresa).HasMaxLength(4).HasColumnType("char").IsRequired();
            Property(c => c.TipoDocumento).HasMaxLength(2).HasColumnType("varchar");
            Property(c => c.CodigoRazao).HasMaxLength(1).HasColumnType("char");
            Property(c => c.CodigoCliente).HasMaxLength(10).HasColumnType("char").IsRequired();
            Property(c => c.OrdemVendaNumero).HasMaxLength(10).HasColumnType("varchar");
            Property(c => c.NotaFiscal).HasMaxLength(20).HasColumnType("varchar");
            Property(c => c.TextoDocumento).HasMaxLength(50).HasColumnType("varchar");
            Property(c => c.InstrumentoPagamento).HasMaxLength(1).HasColumnType("char");
            Property(c => c.NumeroDocumentoCompensacao).HasMaxLength(10).HasColumnType("char");
            Property(c => c.MoedaInterna).HasMaxLength(5).HasColumnType("varchar");
            Property(c => c.MoedaDocumento).HasMaxLength(5).HasColumnType("varchar");
            Property(c => c.CreditoDebito).HasMaxLength(1).HasColumnType("char").IsRequired();
            Property(c => c.CobrancaAutomatica).HasMaxLength(5).HasColumnType("varchar").IsRequired();
            Property(c => c.CondPagto).HasMaxLength(4).HasColumnType("varchar");
            Property(c => c.ValorInterno).HasPrecision(13, 2);
            Property(c => c.ValorDocumento).HasPrecision(13, 2);
            Property(c => c.TaxaJuros).HasPrecision(13,5);
            Property(c => c.PropostaStatus).HasMaxLength(1).HasColumnType("char");
            Property(c => c.RazaoEspecial).HasMaxLength(1).HasColumnType("char");
            Property(c => c.AnoExercicioDocumentoCompensacao).HasMaxLength(4).HasColumnType("char");
            Property(c => c.VariacaoCambial).HasPrecision(13, 2);
            Property(c => c.BloqueioPagamento).HasMaxLength(1).HasColumnType("char");

        }
    }
}
