using Sigma.Application.Dtos;

namespace Sigma.Application.Interfaces
{
    public interface IAutenticacaoService
    {
        Task<string> Authenticate(LoginDto login);
    }
}
