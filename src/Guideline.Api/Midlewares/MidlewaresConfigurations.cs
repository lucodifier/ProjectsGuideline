using FluentValidation.AspNetCore;
using Guideline.Application.ViewModels;
using Guideline.Infra.CrossCutting.Ioc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Guideline.Api.Midlewares
{
    public static class MidlewaresConfigurations
    {
        public static void ConfigureFluent(this IServiceCollection services)
        {
            services.AddMvc()
           .AddFluentValidation(options =>
           {
               options.RegisterValidatorsFromAssemblyContaining<Startup>();
               options.RegisterValidatorsFromAssemblyContaining<CreateUserViewModel>();

           });
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1",
            //        new OpenApiInfo
            //        {
            //            Title = "Guideline.Api",
            //            Version = "v1",
            //            Description = "Web API Guideline para demais projetos",
            //            Contact = new OpenApiContact
            //            {
            //                Name = "Guideline Project"
            //            }
            //        });

            //    string applicationPath = AppContext.BaseDirectory;
            //    string applicationName = Assembly.GetEntryAssembly().GetName().Name;
            //    string xmlDocPath = Path.Combine(applicationPath, $"{applicationName}.xml");
            //    c.IncludeXmlComments(xmlDocPath);
            //});

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "Guideline.Api", Version = "v1" });
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                       new string[] { }
                    }
                });

                string applicationPath = AppContext.BaseDirectory;
                string applicationName = Assembly.GetEntryAssembly().GetName().Name;
                string xmlDocPath = Path.Combine(applicationPath, $"{applicationName}.xml");
                s.IncludeXmlComments(xmlDocPath);
            });
        }

        public static void ConfigureIocDI(this IServiceCollection services)
        {
                Bootstrapper.SetupIoC(services);
        }

        public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var _secret = configuration.GetSection("JwtSettings").GetSection("secret").Value;
            var _key = Encoding.ASCII.GetBytes(_secret);
            var _audience = configuration.GetSection("JwtSettings").GetSection("audience").Value;
            var _issuer = configuration.GetSection("JwtSettings").GetSection("issuer").Value;

            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _issuer,
                        ValidAudience = _audience,
                        IssuerSigningKey = new SymmetricSecurityKey(_key)
                    };
                });
        }
    }
}
