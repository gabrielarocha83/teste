using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServicePropostaLCDemonstrativo: IAppServiceBase<PropostaLCDemonstrativoDto>
    {

        bool Insert(ref PropostaLCDemonstrativoDto obj);
        Task<bool> InsertGrupoDemonstrativo(IEnumerable<PropostaLCGrupoEconomicoDto> grupos);
    }
}
