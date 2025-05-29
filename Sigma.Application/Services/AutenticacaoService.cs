using Microsoft.IdentityModel.Tokens;
using Sigma.Application.Dtos;
using Sigma.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sigma.Application.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private const string SecretKey = "12345678901234567890123456789012"; 

        public async Task<string> Authenticate(LoginDto login)
        {
            if (login.Username != "admin" || login.Password != "admin")
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, login.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
