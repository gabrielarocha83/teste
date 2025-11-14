using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.Domain.Repository;

namespace Yara.AppService
{
    public class AppServicePropostaJuridicoGarantia : IAppServicePropostaJuridicoGarantia
    {

        private readonly IUnitOfWork _unitOfWork;

        public AppServicePropostaJuridicoGarantia(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<PropostaJuridicoGarantiaDto> GetAsync(Expression<Func<PropostaJuridicoGarantiaDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PropostaJuridicoGarantiaDto>> GetAllFilterAsync(Expression<Func<PropostaJuridicoGarantiaDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PropostaJuridicoGarantiaDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public bool Insert(PropostaJuridicoGarantiaDto obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(PropostaJuridicoGarantiaDto obj)
        {
            throw new NotImplementedException();
        }

    }
}
