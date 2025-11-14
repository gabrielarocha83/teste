using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    internal class ConfigurationContaClienteFinanceiro : EntityTypeConfiguration<ContaClienteFinanceiro>
    {
        public ConfigurationContaClienteFinanceiro()
        {
            ToTable("ContaClienteFinanceiro").HasRequired(s => s.ContaCliente).WithMany(c => c.ContaClienteFinanceiro).HasForeignKey(c => c.ContaClienteID);
            HasKey(c => new { c.ContaClienteID, c.EmpresasID });
            Property(c => c.DescricaoConceito).HasColumnType("varchar").IsMaxLength();
            Property(c => c.DescricaoConceitoAnterior).HasColumnType("varchar").IsMaxLength();
            Property(c => c.EmpresasID).HasColumnType("char").HasMaxLength(1);
        }
    }
}
