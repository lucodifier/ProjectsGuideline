using Guideline.Domain.Configuration;

namespace Guideline.Application.Configuration
{
    public class ConfigurationSecrets : IConfigurationSecrets
    {
        public string JwtSecret { get; set; }
        public string ConnectionString { get; set; }
        public string PinblockKey { get; set; }
    }
}
