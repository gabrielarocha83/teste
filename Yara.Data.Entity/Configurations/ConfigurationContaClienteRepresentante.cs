using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationContaClienteRepresentante : EntityTypeConfiguration<ContaClienteRepresentante>
    {
        public ConfigurationContaClienteRepresentante()
        {
            ToTable("ContaCliente_Representante");

            HasKey(c => new { c.ContaClienteID, c.RepresentanteID, c.EmpresasID });

            Property(x => x.EmpresasID).HasColumnType("char").HasMaxLength(1);
            Property(x => x.CodigoSapCTC).HasColumnType("varchar").HasMaxLength(10);

            ToTable("ContaCliente_Representante")
                .HasRequired(c => c.ContaCliente)
                .WithMany(c => c.ContaClienteRepresentante)
                .HasForeignKey(c => c.ContaClienteID);

            ToTable("ContaCliente_Representante")
                .HasRequired(c => c.Representante)
                .WithMany(c => c.ContaClienteRepresentante)
                .HasForeignKey(c => c.RepresentanteID);
        }
    }
}
