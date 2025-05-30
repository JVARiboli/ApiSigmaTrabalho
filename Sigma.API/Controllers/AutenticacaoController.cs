using Microsoft.AspNetCore.Mvc;
using Sigma.Application.Dtos;
using Sigma.Application.Interfaces;

namespace Sigma.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AutenticacaoController : ControllerBase
	{
		private readonly IAutenticacaoService _authService;

		public AutenticacaoController(IAutenticacaoService authService)
		{
			_authService = authService;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] UserDTo login)
		{
			if (login.Username == "admin" && login.Password == "123")
			{
				var token = _authService.GerarToken(login.Username);
				return Ok(new { Token = token });
			}

			return Unauthorized();
		}
	}
}
