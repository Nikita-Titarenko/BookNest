using System.Text;
using BookNest.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BookNest.Infrastructure
{
    public static class InfrastructureServicesExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("DefaultConnection not found");
            services.AddDbContextFactory<ApplicationDbContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            });

            services.Configure<JwtTokenOptions>(configuration.GetSection("Jwt"));

            services.AddSingleton<IExecuteSafe, ExecuteSafe>();

            var token = configuration.GetSection("Jwt:Key").Value ?? throw new InvalidOperationException("Jwt key not fond");
            var issuer = configuration.GetSection("Jwt:Issuer").Value ?? throw new InvalidOperationException("Issuer not found");
            var audience = configuration.GetSection("Jwt:Audience").Value ?? throw new InvalidOperationException("Audience not found");
            services.AddAuthentication()
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token)),
                        ValidIssuer = issuer,
                        ValidAudience = audience
                    };
                });

            return services;
        }
    }
}
