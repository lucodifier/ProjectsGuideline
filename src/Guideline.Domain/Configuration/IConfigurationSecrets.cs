using System.Net;

namespace Guideline.Domain.Configuration
{
    public interface IConfigurationSecrets
    {
        string JwtSecret { get; set; }
        string ConnectionString { get; set; }
        string PinblockKey { get; set; }
    }
}
