using Sigma.Application.Dtos;

namespace Sigma.Application.Interfaces
{
    public interface IAutenticacaoService
    {
        public string GerarToken(string username);
    }
}
