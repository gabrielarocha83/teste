using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceEnvioEmail
    {
        Task<KeyValuePair<bool, string>> SendMailLiberacaoManual(Guid solicitacaoId, UsuarioDto usuarioDto, string comentario, string EmpresaID, string URL);
        Task<KeyValuePair<bool, string>> SendMailGrupoEconomico(Guid grupoId, UsuarioDto usuarioDto, string grupoNome, string URL);
        Task<KeyValuePair<bool, string>> SendMailSolicitanteGrupoEconomico(UsuarioDto usuarioDto, bool status, string grupoNome, string URL);
        Task<KeyValuePair<bool, string>> SendMailFeedBackPropostas(UsuarioDto usuarioDto, Guid PropostaID, string comentario, Guid contaClienteID, string URL);
        Task<KeyValuePair<bool, string>> SendMailBlogProposta(BlogDto blogDto, Guid contaClienteID, string URL);
        // Task<KeyValuePair<bool, string>> SendMailPropostaResponsavel(List<UsuarioDto> usuarioDto, bool status, string EmpresaID, string propostaNumero, string URL);
        Task<KeyValuePair<bool, string>> SendMailComiteLC(PropostaLCComiteDto comite, string URL);
        Task<KeyValuePair<bool, string>> SendMailComiteLCAdicional(PropostaLCAdicionalComiteDto comite, string URL);
        Task<KeyValuePair<bool, string>> SendMailComiteAbono(PropostaAbonoComiteDto comite, string URL);
        Task<KeyValuePair<bool, string>> SendMailComiteProrrogacao(PropostaProrrogacaoComiteDto comite, string URL);
        Task<KeyValuePair<bool, string>> SendMailComiteRenovacaoVigenciaLC(PropostaRenovacaoVigenciaLCComiteDto comite, string URL);
        Task<string> SendMailPropostaJuridica(PropostaJuridicoDto juridicoDto, ContaClienteDto clienteDto, string URL);
        Task<KeyValuePair<bool, string>> SendMailCockpitNotificacaoUsuario(IEnumerable<NotificacaoUsuarioDto> notificacaoUsuarioDto, UsuarioDto usuarioDto, string empresa, string URLcockpit, string URLcadastroCliente);
    }
}
