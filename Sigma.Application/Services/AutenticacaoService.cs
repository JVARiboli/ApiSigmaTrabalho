using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sigma.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AutenticacaoService : IAutenticacaoService
{
	private readonly string _secret;

	public AutenticacaoService(IConfiguration configuration)
	{
		_secret = configuration["Jwt:Key"] ?? throw new Exception("Jwt:Key não configurada");
	}

	public string GerarToken(string username)
	{
		var tokenHandler = new JwtSecurityTokenHandler();
		var key = Encoding.ASCII.GetBytes(_secret);

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }),
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
