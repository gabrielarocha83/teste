using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.Domain.Entities;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceDivisaoRemessa : IAppServiceBase<DivisaoRemessaDto>
    {
        Task<IEnumerable<DivisaoRemessaCockPitDto>> GetAllPendencyByUser(Guid UserID, string EmpresaID);
        Task<DivisaoRemessaDto> GetDadosAsync(int Divisao, int Item, string Numero);
        Task<IEnumerable<DivisaoRemessaLogFluxoDto>> GetAllLogByDivisao(int Divisao, int Item, string Numero, Guid SolicitanteID);
    }
}
