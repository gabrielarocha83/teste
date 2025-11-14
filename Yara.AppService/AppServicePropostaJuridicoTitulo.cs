using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.Domain.Repository;

namespace Yara.AppService
{
    public class AppServicePropostaJuridicoTitulo : IAppServicePropostaJuridicoTitulo
    {

        private readonly IUnitOfWork _unitOfWork;

        public AppServicePropostaJuridicoTitulo(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<PropostaJuridicoTituloDto> GetAsync(Expression<Func<PropostaJuridicoTituloDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PropostaJuridicoTituloDto>> GetAllFilterAsync(Expression<Func<PropostaJuridicoTituloDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PropostaJuridicoTituloDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public bool Insert(PropostaJuridicoTituloDto obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(PropostaJuridicoTituloDto obj)
        {
            throw new NotImplementedException();
        }

    }
}
