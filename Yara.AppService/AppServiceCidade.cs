using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Repository;

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.AppService
{
    public class AppServiceCidade : IAppServiceCidade
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceCidade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CidadeDto>> GetAllStatesByState(Guid StateID)
        {
            var cidades = await _unitOfWork.CidadeRepository.GetAllFilterAsync(c => c.EstadoID.Equals(StateID));
            return cidades.MapTo<IEnumerable<CidadeDto>>();
        }

    }
}
