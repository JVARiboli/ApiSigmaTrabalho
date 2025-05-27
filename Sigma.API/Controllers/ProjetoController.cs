using Microsoft.AspNetCore.Mvc;
using Sigma.Application.Dtos;
using Sigma.Application.Interfaces;
using Sigma.Domain.Dtos;

namespace Sigma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetoController : ControllerBase
    {
        private readonly IProjetoService _projetoService;

        public ProjetoController(IProjetoService projetoService)
        {
            _projetoService = projetoService;
        }
        [HttpPost("inserir")]
        public async Task<IActionResult> Inserir([FromBody] ProjetoNovoDto model)
        {
            var result = await _projetoService.Inserir(model);
            return result ? Ok() : BadRequest();
        }

        [HttpPut("alterar")]
        public async Task<IActionResult> Alterar([FromBody] ProjetoAtualizarDto model)
        {
            try
            {
                var result = await _projetoService.Atualizar(model);
                return result ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("excluir/{id:long}")]
        public async Task<IActionResult> Excluir(long id)
        {
            try
            {
                var result = await _projetoService.Excluir(id);
                return result ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("buscar/{id:long}")]
        public async Task<IActionResult> Buscar(long id)
        {
            var projetos = await _projetoService.BuscarPorId(id);
            return Ok(projetos);
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarTodos()
        {
            var projetos = await _projetoService.BuscarTodos();
            return Ok(projetos);
        }
    }
}
