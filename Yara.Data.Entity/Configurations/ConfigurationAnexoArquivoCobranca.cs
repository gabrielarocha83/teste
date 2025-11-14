using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationAnexoArquivoCobranca : EntityTypeConfiguration<AnexoArquivoCobranca>
    {
        public ConfigurationAnexoArquivoCobranca()
        {
            ToTable("AnexoArquivoCobranca");
            HasKey(c => c.ID);
            Property(c => c.Arquivo).IsRequired();
            Property(c => c.NomeArquivo).IsRequired().HasMaxLength(50);
            Property(c => c.Descricao).IsRequired().HasMaxLength(50);
            Property(c => c.ExtensaoArquivo).IsRequired().HasMaxLength(6);
        }
    }
}
