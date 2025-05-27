using Sigma.Domain.Enums;

namespace Sigma.Domain.Entities
{
    public class Projetos
    {
        public long Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime PrevisaoTermino { get; set; }
        public DateTime? DataTerminoReal { get; set; }
        public decimal OrcamentoTotal { get; set; }
        public ClassificaçãoDeRisco ClassificacaoRisco { get; set; }
        public StatusDoProjeto Status { get; set; }
    }
}
