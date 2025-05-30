using Sigma.Domain.Entities;
using Sigma.Domain.Enums;

namespace Sigma.Domain.Interfaces.Repositories
{
    public interface IProjetoRepository
    {
        Task<bool> Inserir(Projetos entidade);
        Task<bool> Atualizar(Projetos entidade);
        Task<bool> Excluir(long id);
        Task<List<Projetos>> BuscarTodos();
        Task<Projetos> BuscarPorId(long id);
		Task<List<Projetos>> BuscarPorNomeStatus(string nome, StatusDoProjeto? status);
	}
}
