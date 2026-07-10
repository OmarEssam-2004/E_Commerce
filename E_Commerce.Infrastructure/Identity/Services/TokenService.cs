using E_Commerce.Application.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace E_Commerce.Infrastructure.Identity.Services
{
    public class TokenService : ITokenService
    {
        public async Task<string> CreateTokenAsync(string userId, string email, string userNmae, IReadOnlyList<string> roles)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.GivenName, userNmae),
            };

            foreach (var role in roles) 
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var secretKey = "MySecretKeyForMyApplicationMySecretKeyForMyApplicationMySecretKeyForMyApplicationMySecretKeyForMyApplication";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));


            var jwtToken = new JwtSecurityToken(
                issuer: "https://localhost:7149",
                audience: "MyOnlineStore",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(2),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)


                );


            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
