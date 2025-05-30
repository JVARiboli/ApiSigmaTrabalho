using Sigma.Domain.Enums;

namespace Sigma.Application.Dtos
{
    public class ProjetosDTo
    {
        public long Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime PrevisaoTermino { get; set; }
        public DateTime? DataTerminoReal { get; set; }
        public decimal OrcamentoTotal { get; set; }
        public ClassificacaoDeRisco ClassificacaoRisco { get; set; }
        public StatusDoProjeto Status { get; set; }
    }
}
