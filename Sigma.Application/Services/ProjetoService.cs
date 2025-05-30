using AutoMapper;
using Sigma.Application.Dtos;
using Sigma.Application.Interfaces;
using Sigma.Domain.Dtos;
using Sigma.Domain.Entities;
using Sigma.Domain.Enums;
using Sigma.Domain.Interfaces.Repositories;

namespace Sigma.Application.Services
{
    public class ProjetoService : IProjetoService
    {
        private readonly IMapper _mapper;
        private readonly IProjetoRepository _projetoRepository;

        private readonly StatusDoProjeto[] StatusRestritos = new StatusDoProjeto[]
        {
            StatusDoProjeto.Iniciado,
            StatusDoProjeto.Planejado,
            StatusDoProjeto.EmAndamento,
            StatusDoProjeto.Encerrado
        };

        public ProjetoService(IMapper mapper, IProjetoRepository projetoRepository)
        {
            _mapper = mapper;
            _projetoRepository = projetoRepository;
        }

        public async Task<bool> Inserir(ProjetoNovoDTo model)
        {
            var projeto = _mapper.Map<Projetos>(model);
            projeto.Status = StatusDoProjeto.EmAnalise;
            return await _projetoRepository.Inserir(projeto);
        }

        public async Task<bool> Atualizar(ProjetoAtualizarDTo model)
        {
            var projetoExistente = await _projetoRepository.BuscarPorId(model.Id);
            if (projetoExistente == null)
                throw new Exception("O Projeto não foi encontrado");

            var projetoAtualizado = _mapper.Map<ProjetoAtualizarDTo, Projetos>(model, projetoExistente);

            return await _projetoRepository.Atualizar(projetoAtualizado);
        }

        public async Task<bool> Excluir(long id)
        {
            var projeto = await _projetoRepository.BuscarPorId(id);
            if (projeto == null)
                throw new Exception("O Projeto não foi encontrado");

            if (Array.Exists(StatusRestritos, s => s == projeto.Status))
                throw new Exception($"A Exclusão do projeto não permitida para projetos com o status '{projeto.Status}'");

            return await _projetoRepository.Excluir(id);
        }

        public async Task<List<ProjetosDTo>> BuscarTodos()
        {
            var projetos = await _projetoRepository.BuscarTodos();
            return _mapper.Map<List<ProjetosDTo>>(projetos);
        }

        public async Task<ProjetosDTo> BuscarPorId(long id)
        {
            var projetos = await _projetoRepository.BuscarPorId(id);
            return _mapper.Map<ProjetosDTo>(projetos);
        }

		public async Task<List<ProjetosDTo>> BuscarPorNomeStatus(string nome, StatusDoProjeto? status)
		{
			var projetos = await _projetoRepository.BuscarPorNomeStatus(nome, status);
			return _mapper.Map<List<ProjetosDTo>>(projetos);
		}
	}
}
