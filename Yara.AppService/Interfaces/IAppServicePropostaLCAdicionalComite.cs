using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServicePropostaLCAdicionalComite : IAppServiceBase<PropostaLCAdicionalComiteDto>
    {
        Task<bool> InsertAsync(PropostaLCAdicionalComiteDto comite, string URL);
        Task<bool> InsertNivel(PropostaLCAdicionalComiteDto comite);
        Task<KeyValuePair<bool, string>> UpdateValuePair(PropostaLCAdicionalComiteDto comite, string URL);
    }
}
