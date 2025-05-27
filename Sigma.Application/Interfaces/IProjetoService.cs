using Sigma.Application.Dtos;
using Sigma.Domain.Dtos;

namespace Sigma.Application.Interfaces
{
    public interface IProjetoService
    {
        Task<bool> Inserir(ProjetoNovoDto model);
        Task<bool> Atualizar(ProjetoAtualizarDto model);
        Task<bool> Excluir(long id);
        Task<List<ProjetosDto>> BuscarTodos();
        Task<ProjetosDto> BuscarPorId(long id);
    }
}
