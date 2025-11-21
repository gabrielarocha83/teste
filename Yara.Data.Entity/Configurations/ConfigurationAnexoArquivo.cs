using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationAnexoArquivo : EntityTypeConfiguration<AnexoArquivo>
    {
        public ConfigurationAnexoArquivo()
        {
            ToTable("AnexoArquivo");
            HasKey(c => c.ID);

            Property(c => c.AnexoID).IsRequired();
            Property(c => c.Arquivo).IsRequired();
            Property(c => c.NomeArquivo).IsRequired().HasMaxLength(512);
            Property(c => c.ExtensaoArquivo).IsRequired().HasMaxLength(6);
            Property(c => c.Comentario).HasMaxLength(400);
            Property(c => c.Complemento).HasMaxLength(400);
        }
    }
}
