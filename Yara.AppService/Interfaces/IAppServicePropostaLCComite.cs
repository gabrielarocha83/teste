using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServicePropostaLCComite : IAppServiceBase<PropostaLCComiteDto>
    {
        Task<bool> InsertAsync(PropostaLCComiteDto comite, string URL);
        Task<bool> InsertNivel(PropostaLCComiteDto comite);
        Task<KeyValuePair<bool, string>> UpdateValuePair(PropostaLCComiteDto obj, string URL);
    }
}
