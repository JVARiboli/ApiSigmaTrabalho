using Sigma.Application.Dtos;
using Sigma.Domain.Dtos;
using Sigma.Domain.Enums;

namespace Sigma.Application.Interfaces
{
    public interface IProjetoService
    {
        Task<bool> Inserir(ProjetoNovoDTo model);
        Task<bool> Atualizar(ProjetoAtualizarDTo model);
        Task<bool> Excluir(long id);
        Task<List<ProjetosDTo>> BuscarTodos();
        Task<ProjetosDTo> BuscarPorId(long id);
		Task<List<ProjetosDTo>> BuscarPorNomeStatus(string nome, StatusDoProjeto? status);
	}
}
