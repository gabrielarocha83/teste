using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Repository;

namespace Yara.AppService
{
    public class AppServiceHistoricoContaCliente : IAppServiceHistoricoContaCliente
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceHistoricoContaCliente(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<HistoricoContaClienteDto>> GetAllHistoryAccountClient(Guid clientID, string company)
        {
            var historicos = await _unitOfWork.HistoricoContaClienteRepository.GetAllFilterAsync(c => c.ContaClienteID.Equals(clientID) && c.EmpresasID.Equals(company));
            return historicos.OrderByDescending(c => c.Ano).MapTo<IEnumerable<HistoricoContaClienteDto>>();
        }
    }
}
