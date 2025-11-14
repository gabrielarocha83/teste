using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Repository;

namespace Yara.AppService
{
    public class AppServicePropostaJuridicoHistoricoPagamento : IAppServicePropostaJuridicoHistoricoPagamento
    {

        private readonly IUnitOfWork _unitOfWork;

        public AppServicePropostaJuridicoHistoricoPagamento(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<PropostaJuridicoHistoricoPagamentoDto> GetAsync(Expression<Func<PropostaJuridicoHistoricoPagamentoDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PropostaJuridicoHistoricoPagamentoDto>> GetAllFilterAsync(Expression<Func<PropostaJuridicoHistoricoPagamentoDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PropostaJuridicoHistoricoPagamentoDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public bool Insert(PropostaJuridicoHistoricoPagamentoDto obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(PropostaJuridicoHistoricoPagamentoDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PropostaJuridicoHistoricoPagamentoDto>> BuscaHistoricoPagamento(Guid propostaJuridicoId, Guid contaClienteId)
        {
            var historico = await _unitOfWork.PropostaJuridicoHistoricoPagamentoRepository.BuscaHistoricoPagamento(propostaJuridicoId, contaClienteId);
            return historico.MapTo<List<PropostaJuridicoHistoricoPagamentoDto>>();
        }

    }
}
