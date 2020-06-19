using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProcessExplorerWeb.Core.Interfaces;
using ProcessExplorerWeb.Infrastructure.Interfaces;
using ProcessExplorerWeb.Infrastructure.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Infrastructure.Identity.Services
{
    public class TokenManager : ITokenManager
    {
        private readonly IDateTime _time;
        private readonly JwtTokenOptions _jwtOptions;

        public TokenManager(IDateTime time, IOptions<AuthenticationOptions> jwtOptions)
        {
            _time = time;
            _jwtOptions = jwtOptions.Value.JwtTokenOptions;
        }

        public (string jwtToken, long expireIn) GenerateToken(IEnumerable<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            DateTime expirationTIime = _time.Now.AddDays(_jwtOptions.Duration);
            long expireIn = new DateTimeOffset(expirationTIime).ToUnixTimeMilliseconds();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expirationTIime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtToken = tokenHandler.CreateToken(tokenDescriptor);
            return (tokenHandler.WriteToken(jwtToken), expireIn);
        }

        public bool TokenValid(string jwtToken)
        {
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            //Next line throws exception if token isnt valid
            try
            {
                IPrincipal principal = tokenHandler.ValidateToken(jwtToken, validationParameters, out SecurityToken validatedToken);
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }

        private TokenValidationParameters GetValidationParameters()
        {
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            return new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        }
    }
}
