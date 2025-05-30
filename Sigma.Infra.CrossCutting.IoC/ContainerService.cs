using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sigma.Application.Interfaces;
using Sigma.Application.Services;
using Sigma.Domain.Interfaces.Repositories;
using Sigma.Infra.Data.Context;
using Sigma.Infra.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Sigma.Infra.CrossCutting.IoC
{

    public static class ContainerService
    {
        public static IServiceCollection AddApplicationServicesCollentions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices();
            services.AddRepositories();
            services.AddApplicationAuthentication(configuration);
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProjetoService, ProjetoService>();
            services.AddScoped<IAutenticacaoService, AutenticacaoService>();
            
            return services;
        }

        public static IServiceCollection AddApplicationAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("4c6398f3-f5c6-4777-b39f-0e48a59784d7")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProjetoRepository, ProjetoRepository>();
            return services;
        }

        public static IServiceCollection AddApplicationContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SigmaContext>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("Sigma.Infra.Data")));
            return services;
        }        
    }
}
