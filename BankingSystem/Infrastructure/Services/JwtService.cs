using BankingSystem.Application.Services;
using BankingSystem.Application.Services.Models;
using BankingSystem.Infrastructure.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankingSystem.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IClock _clock;
        private readonly AuthOptions _authOptions;
        private readonly SigningCredentials _signingCredentials;

        public JwtService(IClock clock, AuthOptions authOptions)
        {
            if (authOptions.IssuerSigningKey is null)
            {
                throw new InvalidOperationException("Issuer signing key is not set.");
            }

            _clock = clock;
            _authOptions = authOptions;
            _signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.IssuerSigningKey)),
                SecurityAlgorithms.HmacSha256);
        }

        public JsonWebToken CreateToken(string userId, string email, string role)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("User ID claim (subject) cannot be empty.", nameof(userId));
            }

            var now = _clock.CurrentDate();

            var jwtClaims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId),
                new(JwtRegisteredClaimNames.UniqueName, userId),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, now.ToUnixTimeStamp().ToString())
            };

            if (!string.IsNullOrWhiteSpace(role))
            {
                jwtClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var expires = now.Add(_authOptions.Expiry);

            var jwt = new JwtSecurityToken(
                _authOptions.Issuer,
                claims: jwtClaims,
                notBefore: now,
                expires: expires,
                signingCredentials: _signingCredentials
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonWebToken
            {
                AccessToken = accessToken,
                Expires = expires.ToUnixTimeStamp(),
                Id = userId,
                Role = role ?? string.Empty,
                Email = email
            };
        }
    }
}
