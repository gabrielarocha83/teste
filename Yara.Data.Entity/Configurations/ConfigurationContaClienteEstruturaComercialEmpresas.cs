using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationContaClienteEstruturaComercialEmpresas : EntityTypeConfiguration<ContaClienteEstruturaComercial>
    {
        public ConfigurationContaClienteEstruturaComercialEmpresas()
        {
            ToTable("ContaCliente_EstruturaComercial");

            HasKey(c => new {c.ContaClienteId, c.EstruturaComercialId, c.EmpresasId});

            Property(x => x.EmpresasId).HasColumnType("char").HasMaxLength(1);
            Property(c => c.EstruturaComercialId).HasMaxLength(10);

            ToTable("ContaCliente_EstruturaComercial")
                .HasRequired(c => c.ContaCliente)
                .WithMany(c => c.ContaClienteEstruturaComercial)
                .HasForeignKey(c => c.ContaClienteId);

            ToTable("ContaCliente_EstruturaComercial")
                .HasRequired(c => c.EstruturaComercial)
                .WithMany(c => c.ContaClienteEstruturaComercial)
                .HasForeignKey(c => c.EstruturaComercialId);
        }
    }
}
