using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    internal class ConfigurationMotivoProrrogacao : EntityTypeConfiguration<MotivoProrrogacao>
    {
        public ConfigurationMotivoProrrogacao()
        {
            ToTable("MotivoProrrogacao");
            HasKey(c => c.ID);
            Property(x => x.Nome)
                .IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("Index") { IsUnique = true } }));
        }
    }
}
