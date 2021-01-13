using Microsoft.Extensions.Configuration;

namespace Guideline.Application.Tests
{
    public class Setting
    {
        public static IConfiguration GetConfiguration()
        {
            var configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", optional: true)
              .Build();

            return configuration;
        }
    }
}
