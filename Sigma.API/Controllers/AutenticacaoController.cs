using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sigma.Application.Dtos;
using Sigma.Application.Interfaces;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var token = await _authService.Authenticate(login);
            if (token == null)
                return Unauthorized();

            return Ok(new { Token = token });
        }
    }
}
