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
    public class AppServiceLiberacaoGrupoEconomicoFluxo : IAppServiceLiberacaoGrupoEconomicoFluxo
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppServiceEnvioEmail _email;

        public AppServiceLiberacaoGrupoEconomicoFluxo(IUnitOfWork unitOfWork, IAppServiceEnvioEmail email)
        {
            _unitOfWork = unitOfWork;
            _email = email;
        }

        // Deprecated
        public async Task<FluxoGrupoEconomicoDto> AprovaReprovaLiberacaoGrupoEconomico(bool aprovar, Guid GrupoEconomicoID, Guid UsuarioID, int ClassificacaoID, string EmpresaID, string URL)
        {
            var fluxoDto = await _unitOfWork.LiberacaoGrupoEconomicoFluxoRepository.AprovaReprovaLiberacaoGrupoEconomico(aprovar, GrupoEconomicoID, UsuarioID, ClassificacaoID, EmpresaID);
            if (fluxoDto != null)
            {
                // Caso não seja nulo, existe um fluxo para envio de email
                var perfil = await _unitOfWork.PerfilRepository.GetAsync(c => c.ID.Equals(fluxoDto.PerfilId));

                var liberacao = await _unitOfWork.LiberacaoGrupoEconomicoFluxoRepository.GetAsync(c => c.FluxoGrupoEconomicoID.Equals(fluxoDto.ID) && c.GrupoEconomicoID.Equals(GrupoEconomicoID));

                var responsavel = await _unitOfWork.EstruturaPerfilUsuarioRepository.GetAsync(c => c.CodigoSap.Equals(liberacao.CodigoSap) && c.PerfilId.Equals(fluxoDto.PerfilId));
                if (responsavel == null)
                    throw new ArgumentException($"O Perfil {perfil.Descricao} não possui configuração para aprovação.");

                var usuarioDto = responsavel.Usuario.MapTo<UsuarioDto>();

                // Busca nome do Grupo para envio de email
                var grupo = await _unitOfWork.GrupoEconomicoReporitory.GetAsync(c => c.ID.Equals(GrupoEconomicoID));

                try
                {
                    var retorno = await _email.SendMailGrupoEconomico(grupo.ID,usuarioDto, grupo.Nome, URL);
                }
                catch
                {

                }
            }
            else
            {
                // Busca Solicitante para envio.
                var solicitante = await _unitOfWork.LiberacaoGrupoEconomicoFluxoRepository.GetAllFilterAsync(c => c.GrupoEconomicoID.Equals(GrupoEconomicoID));

                var metodo = solicitante.OrderByDescending(c => c.DataCriacao).FirstOrDefault();
                if (metodo != null)
                {
                    var user = await _unitOfWork.SolicitanteGrupoEconomicoRepository.GetAsync(c => c.ID.Equals(metodo.SolicitanteGrupoEconomicoID));
                    var grupo = await _unitOfWork.GrupoEconomicoReporitory.GetAsync(c => c.ID.Equals(GrupoEconomicoID));
                    
                    // Usuario de Criação
                    var usuario = await _unitOfWork.UsuarioRepository.GetAsync(c => c.ID.Equals(user.UsuarioIDCriacao));

                    var usuarioDto = usuario.MapTo<UsuarioDto>();

                    try
                    {
                        var retorno = await _email.SendMailSolicitanteGrupoEconomico(usuarioDto, aprovar, grupo.Nome, URL);
                    }
                    catch
                    {

                    }
                }

            }

            return fluxoDto.MapTo<FluxoGrupoEconomicoDto>();
        }

        public async Task<KeyValuePair<bool, string>> AprovaReprovaLiberacaoGrupoEconomicoValue(bool aprovar, Guid grupoEconomicoId, Guid usuarioId, int classificacaoId, string empresaId, string URL)
        {
            var retorno = new KeyValuePair<bool, string>();

            var fluxoDto = await _unitOfWork.LiberacaoGrupoEconomicoFluxoRepository.AprovaReprovaLiberacaoGrupoEconomico(aprovar, grupoEconomicoId, usuarioId, classificacaoId, empresaId);

            var grupo = await _unitOfWork.GrupoEconomicoReporitory.GetAsync(c => c.ID.Equals(grupoEconomicoId));

            if (fluxoDto != null)
            {
                // Caso não seja nulo, existe um fluxo para envio de email
                var perfil = await _unitOfWork.PerfilRepository.GetAsync(c => c.ID.Equals(fluxoDto.PerfilId));

                var liberacao = await _unitOfWork.LiberacaoGrupoEconomicoFluxoRepository.GetAsync(c => c.FluxoGrupoEconomicoID.Equals(fluxoDto.ID) && c.GrupoEconomicoID.Equals(grupoEconomicoId) && c.StatusGrupoEconomicoFluxoID.Equals("PE") || c.StatusGrupoEconomicoFluxoID.Equals("PI"));

                var responsavel = await _unitOfWork.EstruturaPerfilUsuarioRepository.GetAsync(c => c.CodigoSap.Equals(liberacao.CodigoSap) && c.PerfilId.Equals(fluxoDto.PerfilId));
                if (responsavel == null)
                    throw new ArgumentException($"O Perfil {perfil.Descricao} não possui configuração para aprovação.");

                try
                {
                    retorno = await _email.SendMailGrupoEconomico(grupo.ID, responsavel.Usuario.MapTo<UsuarioDto>(), grupo.Nome, URL);
                }
                catch
                {

                }
            }
            else
            {
                // Busca Solicitante para envio
                var solicitante = await _unitOfWork.LiberacaoGrupoEconomicoFluxoRepository.GetAllFilterAsync(c => c.GrupoEconomicoID.Equals(grupoEconomicoId));
                var metodo = solicitante.OrderByDescending(c => c.DataCriacao).FirstOrDefault();

                if (metodo != null)
                {
                    // Devolve o LC original do membro principal caso ele não possua nenhuma proposta de LC aprovada após a criação do grupo

                    // Membro Principal
                    var membroPrincipal = await _unitOfWork.GrupoEconomicoMembroReporitory.GetAsync(c => c.GrupoEconomicoID.Equals(grupo.ID) && c.MembroPrincipal);

                    // Data de aprovação do comite da última proposta de LC aprovada
                    var ultimaPropostaLCAprovada = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ContaClienteID.Equals(membroPrincipal.ContaClienteID) && c.PropostaLCStatusID.Equals("AA") && c.DataAprovacaoComite.HasValue && c.DataCriacao >= grupo.DataCriacao);

                    if (!grupo.Ativo && grupo.StatusGrupoEconomicoFluxoID.Equals("AP") && classificacaoId.Equals(1) && ultimaPropostaLCAprovada == null)
                    {
                        var contaClienteFinanceiro = await _unitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.ContaClienteID.Equals(membroPrincipal.ContaClienteID) && c.EmpresasID.Equals(grupo.EmpresasID));
                        contaClienteFinanceiro.LC = membroPrincipal.LCAntesGrupo;
                        _unitOfWork.ContaClienteFinanceiroRepository.Update(contaClienteFinanceiro);
                        _unitOfWork.Commit();
                    }

                    var user = await _unitOfWork.SolicitanteGrupoEconomicoRepository.GetAsync(c => c.ID.Equals(metodo.SolicitanteGrupoEconomicoID));
                    var usuario = await _unitOfWork.UsuarioRepository.GetAsync(c => c.ID.Equals(user.UsuarioIDCriacao));

                    try
                    {
                        retorno = await _email.SendMailSolicitanteGrupoEconomico(usuario.MapTo<UsuarioDto>(), aprovar, grupo.Nome, URL);
                    }
                    catch
                    {

                    }
                }
            }

            return new KeyValuePair<bool, string>(retorno.Key, retorno.Value);
        }
    }
}
