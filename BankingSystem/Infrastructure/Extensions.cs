using BankingSystem.Application.Services;
using BankingSystem.Core.Repositories;
using BankingSystem.Infrastructure.Contexts;
using BankingSystem.Infrastructure.Options;
using BankingSystem.Infrastructure.Repositories;
using BankingSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BankingSystem.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBankingAccountRepository, BankingAccountRepository>();

            services.AddSingleton<IClock, Clock>();
            services.AddSingleton<IPasswordService, PasswordService>();
            services.AddSingleton<IJwtService, JwtService>();
            services.AddSingleton<IPasswordHasher<PasswordService>, PasswordHasher<PasswordService>>();

            services.AddDbContext<BankingSystemDbContext>(opt => 
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));



            var options = services.GetOptions<AuthOptions>("auth");
            services.AddSingleton(options);

            var tokenValidationParameters = BuildTokenValidationParameters(options);

            services
                .AddAuthentication(authOptions =>
                {
                    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(authOptions =>
                {
                    if (!string.IsNullOrWhiteSpace(options.Challenge))
                    {
                        authOptions.Challenge = options.Challenge;
                    };
                    authOptions.TokenValidationParameters = tokenValidationParameters;
                    authOptions.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(authOptions =>
            {
                authOptions.AddPolicy("is-admin", x => x.RequireRole("admin"));
                authOptions.AddPolicy("is-user", x => x.RequireRole("user"));
            });

            return services;
        }

        private static TokenValidationParameters BuildTokenValidationParameters(AuthOptions options)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = options.ValidIssuer,
                ValidateAudience = options.ValidateAudience,
                ValidateIssuer = options.ValidateIssuer,
                ValidateLifetime = options.ValidateLifetime,
                ClockSkew = TimeSpan.Zero
            };

            if (string.IsNullOrWhiteSpace(options.IssuerSigningKey))
            {
                throw new ArgumentException("Missin issuer signing key.", nameof(options.IssuerSigningKey));
            }

            var rawKey = Encoding.UTF8.GetBytes(options.IssuerSigningKey);
            tokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(rawKey);

            return tokenValidationParameters;
        }

        public static long ToUnixTimeStamp(this DateTime dateTime)
        {
            return new DateTimeOffset(dateTime).ToUnixTimeSeconds();
        }

        private static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            return configuration.GetOptions<T>(sectionName);
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
        {
            var options = new T();
            configuration.GetSection(sectionName).Bind(options);
            return options;
        }
    }
}
