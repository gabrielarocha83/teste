using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceSerasa
    {
        Task<dynamic> ConsultarSerasa(SolicitanteSerasaDto solicitante, string EmpresaID, string urlSerasa, string usuarioSerasa, string senhaSerasa);
        Task<dynamic> ConsultarSerasaPropostaLC(SolicitanteSerasaDto solicitante, Guid PropostaLcID, string EmpresaID, string urlSerasa, string usuarioSerasa, string senhaSerasa);
        Task<dynamic> ConsultarSerasaAlcadaComercial(SolicitanteSerasaDto solicitante, Guid PropostaLcID, string EmpresaID, string urlSerasa, string usuarioSerasa, string senhaSerasa);
        Task<dynamic> HitoricoDetalhe(Guid ID);
        Task<SolicitanteSerasaDto> ExistSerasa(Guid ContaClienteID, string EmpresaID);
        Task<IEnumerable<SolicitanteSerasaDto>> Historico(Guid ContaClienteID);
        Task<bool> AlterarStatusSerasa(Guid contaClienteID, string empresaID, int tipoRestricao);
    }
}
