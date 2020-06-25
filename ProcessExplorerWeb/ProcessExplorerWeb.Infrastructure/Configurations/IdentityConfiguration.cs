using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Common.Options;
using ProcessExplorerWeb.Infrastructure.Identity;
using ProcessExplorerWeb.Infrastructure.Persistence;
using System;
using System.Text;

namespace ProcessExplorerWeb.Infrastructure.Configurations
{
    /// <summary>
    /// Microsoft identity configuraiton
    /// </summary>
    public class IdentityConfiguration : IInstallation
    {
        public IServiceCollection Configure(IServiceCollection services, IConfiguration config)
        {
            //Define use of identity
            services.AddIdentity<IdentityAppUser, IdentityAppRole>()
                .AddEntityFrameworkStores<ProcessExplorerDbContext>()
                .AddDefaultTokenProviders();

            //Get configuration sections for authentication
            var pwConfig = config.GetSection($"{nameof(AuthenticationOptions)}:{nameof(IdentityPasswordOptions)}")
                .Get<IdentityPasswordOptions>();
            var userConfig = config.GetSection($"{nameof(AuthenticationOptions)}:{nameof(IdentityUserOptions)}")
                .Get<IdentityUserOptions>();

            //Configure Identity password and user
            services.Configure<IdentityOptions>(options =>
            {
                //Password section configuration
                options.Password.RequireDigit = pwConfig.RequireDigit;
                options.Password.RequireLowercase = pwConfig.RequireLowercase;
                options.Password.RequireNonAlphanumeric = pwConfig.RequireNonAlphanumeric;
                options.Password.RequireUppercase = pwConfig.RequireUppercase;
                options.Password.RequiredLength = pwConfig.RequiredLength;
                options.Password.RequiredUniqueChars = pwConfig.RequiredUniqueChars;

                //User section configuration
                options.User.RequireUniqueEmail = userConfig.RequireUniqueEmail;
            });

            //Get configuration section for token
            var jwtConfig = config.GetSection($"{nameof(AuthenticationOptions)}:{nameof(JwtTokenOptions)}")
                .Get<JwtTokenOptions>();
            var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);

            //Define authentication scheme and add JWT token
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
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                };
            });

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromDays(30);
            });

            return services;
        }
    }
}
