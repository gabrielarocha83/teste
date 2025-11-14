using System.Data.Entity.ModelConfiguration;
using Yara.Domain.Entities;

namespace Yara.Data.Entity.Configurations
{
    internal class ConfigurationLogLevel : EntityTypeConfiguration<LogLevel>
    {
        public ConfigurationLogLevel()
        {
            ToTable("LogLevel");
            HasKey(x => x.ID);
        }
    }
}