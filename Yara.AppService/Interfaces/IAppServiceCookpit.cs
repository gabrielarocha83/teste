using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceCookpit 
    {
        Task<IEnumerable<BuscaGrupoEconomicoDto>> BuscaGrupoEconomico(Guid usuarioID, string EmpresaID);
        Task<IEnumerable<BuscaCockpitPropostaLCDto>> BuscaPropostaLC(Guid usuarioID, string EmpresaID);
        Task<IEnumerable<BuscaPropostaAbonoDto>> BuscaPropostaAbono(Guid usuarioID, string EmpresaID, bool Acompanhar);
        Task<IEnumerable<BuscaCockpitPropostaLCDto>> BuscaPropostaLCAcompanhamento(Guid usuarioID, string EmpresaID);
        Task<IEnumerable<DivisaoRemessaCockPitDto>> BuscaOrdemVenda(Guid usuarioID, string EmpresaID);
        Task<IEnumerable<BuscaPropostaProrrogacaoDto>> BuscaPropostaProrrogacao(Guid usuarioID, string EmpresaID,bool Acompanhar);
        Task<IEnumerable<BuscaCockpitPropostaAlcadaDto>> BuscaPropostaCockpitAlcada(Guid usuarioId, string empresaId, bool acompanhar);
        Task<IEnumerable<DivisaoRemessaCockPitDto>> GetAllPendencyByUser(Guid UserID, string EmpresaID);
        Task<IEnumerable<BuscaCockpitPropostaRenovacaoVigenciaLCDto>> BuscaPropostaRenovacaoVigenciaLC(Guid usuarioID, string EmpresaID);
        Task<IEnumerable<BuscaPropostaLCAdicionalDto>> BuscaPropostaLCAdicional(Guid usuarioID, string EmpresaID, bool Acompanhar);
    }
}