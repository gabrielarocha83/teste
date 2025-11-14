using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceRelatorios 
    {

        Task<IEnumerable<BuscaPropostasDto>> GetConsultaProposta(BuscaPropostasSearchDto filter);
        Task<byte[]> GetConsultaPropostaExcel(BuscaPropostasSearchDto filter);
        Task<IEnumerable<string>> GetStatusProposal();
    }
}
