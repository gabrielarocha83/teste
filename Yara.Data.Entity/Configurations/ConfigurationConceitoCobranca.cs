using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    internal class ConfigurationConceitoCobranca : EntityTypeConfiguration<ConceitoCobranca>
    {
        public ConfigurationConceitoCobranca()
        {
            ToTable("ConceitoCobranca");
            HasKey(c => c.ID);
            Property(x => x.Nome).IsRequired()
                .HasMaxLength(2)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] {
        new IndexAttribute("Index") { IsUnique = true } }));

        }
    }
}
