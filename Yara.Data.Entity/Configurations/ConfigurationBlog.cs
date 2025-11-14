using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    public class ConfigurationBlog : EntityTypeConfiguration<Blog>
    {
        public ConfigurationBlog()
        {
            ToTable("Blog");
            HasKey(x => x.ID);

            Property(c => c.Area).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("ix_area")));
            Property(c => c.DataCriacao).IsRequired();
            Property(c => c.EmpresaID).HasColumnType("char").HasMaxLength(1).IsRequired();
            Property(c => c.Mensagem).HasColumnType("text").IsMaxLength().IsRequired();
        }
    }
}
