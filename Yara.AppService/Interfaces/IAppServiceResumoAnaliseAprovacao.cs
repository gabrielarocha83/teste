using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceResumoAnaliseAprovacao
    {
        Task<IEnumerable<ResumoAnaliseAprovacaoDto>> BuscaResumoAnalise(Guid usuarioID, string empresaID, List<string> gcs);
        Task<IEnumerable<ResumoAnaliseAprovacaoDto>> BuscaResumoAprovacao(Guid usuarioID, string empresaID, List<string> gcs);
    }
}