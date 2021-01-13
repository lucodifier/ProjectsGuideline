using Guideline.Application.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Guideline.Api.Midlewares
{
    public static class SecretsMidleware
    {
        public static void SetSecrets(IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("AppSettings");
            services.Configure<ConfigurationSecrets>(section);

            var secrets = new ConfigurationSecrets();
            secrets.JwtSecret = section.GetSection("JwtSecret").Value;
            secrets.ConnectionString = section.GetSection("ConnectionString").Value;
            secrets.PinblockKey = section.GetSection("PinblockKey").Value;

            services.AddSingleton(secrets);
        }
    }
}
