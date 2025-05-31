using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sigma.Application.Dtos;
using Sigma.Application.Interfaces;
using Sigma.Domain.Dtos;
using Sigma.Domain.Enums;

namespace Sigma.API.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/projetos")]
	public class ProjetoController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly IProjetoService _projetoService;

		public ProjetoController(IConfiguration configuration, IProjetoService projetoService)
		{
			_configuration = configuration;
			_projetoService = projetoService;
		}

		[Authorize]
		[HttpPost("inserir")]
		public async Task<IActionResult> CriarProjeto([FromBody] ProjetoNovoDto model)
		{
			var resultado = await _projetoService.Adicionar(model);
			return Ok(resultado);
		}

		[HttpGet("buscar-todos")]
		public async Task<IActionResult> ObterTodos()
		{
			var resultado = await _projetoService.ObterTodos();
			return Ok(resultado);
		}

		[Authorize]
		[HttpDelete("remover-projeto/{id:long}")]
		public async Task<IActionResult> RemoverProjeto(long id)
		{
			try
			{
				await _projetoService.Excluir(id);
				return NoContent();
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(new { mensagem = ex.Message });
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(new { mensagem = ex.Message });
			}
		}

		[Authorize]
		[HttpPut("atualizar-projeto/{id:long}")]
		public async Task<IActionResult> AtualizarProjeto(long id, [FromBody] ProjetoNovoDto dto)
		{
			await _projetoService.Alterar(id, dto);
			return NoContent();
		}

		[HttpGet("buscar-por-nome-status")]
		public async Task<IActionResult> ConsultarPorNomeStatus([FromQuery] string? nome, [FromQuery] StatusDoProjetoEnum? status)
		{
			var projetos = await _projetoService.ConsultarPorNomeStatus(nome, status);
			return Ok(projetos);
		}
	}
}
