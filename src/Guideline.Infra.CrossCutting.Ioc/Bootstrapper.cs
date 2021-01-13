using AutoMapper;
using Guideline.Application.AutoMapper;
using Guideline.Application.Interfaces;
using Guideline.Application.Services;
using Guideline.Domain.Interfaces;
using Guideline.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Guideline.Infra.CrossCutting.Ioc
{

    public static class Bootstrapper
    {
        public static void SetupIoC(this IServiceCollection services)
        {
            services.ConfigureAutoMapper();
            services.RegisterRepositories();
            services.RegisterServices();
        }

        private static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));
        }

        private static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
        }

        private static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();            
        }
    }
}
