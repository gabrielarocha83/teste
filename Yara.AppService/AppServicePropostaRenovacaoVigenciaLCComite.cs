using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.AppService
{
    public class AppServicePropostaRenovacaoVigenciaLCComite : IAppServicePropostaRenovacaoVigenciaLCComite
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServicePropostaRenovacaoVigenciaLCComite(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PropostaRenovacaoVigenciaLCComiteDto> GetAsync(Expression<Func<PropostaRenovacaoVigenciaLCComiteDto, bool>> expression)
        {
            var comite = await _unitOfWork.PropostaRenovacaoVigenciaLCComiteRepository.GetAsync(Mapper.Map<Expression<Func<PropostaRenovacaoVigenciaLCComite, bool>>>(expression));
            return Mapper.Map<PropostaRenovacaoVigenciaLCComiteDto>(comite);
        }

        public async Task<IEnumerable<PropostaRenovacaoVigenciaLCComiteDto>> GetAllFilterAsync(Expression<Func<PropostaRenovacaoVigenciaLCComiteDto, bool>> expression)
        {
            var comite = await _unitOfWork.PropostaRenovacaoVigenciaLCComiteRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<PropostaRenovacaoVigenciaLCComite, bool>>>(expression));
            return Mapper.Map<IEnumerable<PropostaRenovacaoVigenciaLCComiteDto>>(comite);
        }

        public async Task<IEnumerable<PropostaRenovacaoVigenciaLCComiteDto>> GetAllAsync()
        {
            var comite = await _unitOfWork.PropostaRenovacaoVigenciaLCComiteRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<PropostaRenovacaoVigenciaLCComiteDto>>(comite);
        }

        public bool Insert(PropostaRenovacaoVigenciaLCComiteDto obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(PropostaRenovacaoVigenciaLCComiteDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SendCommittee(Guid propostaRenovacaoVigenciaLCID, Guid usuarioIDAlteracao, string URL)
        {
            try
            {
                var proposta = await _unitOfWork.PropostaRenovacaoVigenciaLCRepository.GetAsync(c => c.ID.Equals(propostaRenovacaoVigenciaLCID));
                if (proposta != null)
                {
                    var fluxos = await _unitOfWork.FluxoRenovacaoVigenciaLCRepository.GetAllFilterAsyncOrdered(c => c.EmpresaID == proposta.EmpresaID && c.Ativo, d => d.Nivel);
                    if (fluxos == null || fluxos.Count() <= 0)
                        throw new ArgumentException("Envio para o comite não permitido, pois, não possuí um responsável para o primeiro nivel.");
                    else
                    {
                        PropostaRenovacaoVigenciaLCComiteDto primeiroComiteDto = null;

                        foreach(var fluxo in fluxos)
                        {
                            var comite = new PropostaRenovacaoVigenciaLCComite
                            {
                                ID = Guid.NewGuid(),
                                PropostaRenovacaoVigenciaLCID = proposta.ID,
                                DataCriacao = DateTime.Now,
                                Nivel = fluxo.Nivel,
                                UsuarioID = fluxo.UsuarioId,
                                StatusComiteID = (fluxo.Nivel == 1 ? "AA" : "PE"),
                                FluxoRenovacaoVigenciaLCID = fluxo.ID
                            };

                            _unitOfWork.PropostaRenovacaoVigenciaLCComiteRepository.Insert(comite);

                            if (primeiroComiteDto == null)
                            {
                                primeiroComiteDto = comite.MapTo<PropostaRenovacaoVigenciaLCComiteDto>();
                                primeiroComiteDto.EmpresaID = proposta.EmpresaID;
                            }
                        }

                        proposta.PropostaLCStatusID = "FC";
                        proposta.UsuarioIDAlteracao = usuarioIDAlteracao;
                        proposta.DataAlteracao = DateTime.Now;
                        proposta.ResponsavelID = fluxos.FirstOrDefault(c => c.Nivel == 1)?.UsuarioId ?? proposta.ResponsavelID;

                        _unitOfWork.PropostaRenovacaoVigenciaLCRepository.Update(proposta);

                        await SendEmail(primeiroComiteDto, URL);
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }

            return _unitOfWork.Commit();
        }

        public async Task<IEnumerable<PropostaRenovacaoVigenciaLCComiteDto>> GetCommitteeByProposalID(Guid propostaRenovacaoVigenciaLCID)
        {
            try
            {
                var comite = await _unitOfWork.PropostaRenovacaoVigenciaLCComiteRepository.GetAllFilterAsync(c => c.PropostaRenovacaoVigenciaLCID == propostaRenovacaoVigenciaLCID);
                comite = comite.OrderBy(c => c.Nivel);
                return comite.MapTo<IEnumerable<PropostaRenovacaoVigenciaLCComiteDto>>();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> SendApproval(DecisaoComitePropostaRenovacaoVigenciaLCDto decisaoComitePropostaRenovacaoVigenciaLC, string URL)
        {
            try
            {
                var comite = await _unitOfWork.PropostaRenovacaoVigenciaLCComiteRepository.GetAsync(c => c.ID == decisaoComitePropostaRenovacaoVigenciaLC.PropostaRenovacaoVigenciaLCComiteID);

                comite.DataAcao = DateTime.Now;
                comite.Comentario = decisaoComitePropostaRenovacaoVigenciaLC.Comentario;
                comite.StatusComiteID = decisaoComitePropostaRenovacaoVigenciaLC.StatusComiteID;

                _unitOfWork.PropostaRenovacaoVigenciaLCComiteRepository.Update(comite);

                if (decisaoComitePropostaRenovacaoVigenciaLC.StatusComiteID == "RF")
                {
                    var proposta = await _unitOfWork.PropostaRenovacaoVigenciaLCRepository.GetAsync(c => c.ID == comite.PropostaRenovacaoVigenciaLCID);

                    proposta.PropostaLCStatusID = "XR";
                    proposta.UsuarioIDAlteracao = comite.UsuarioID;
                    proposta.DataAlteracao = DateTime.Now;

                    _unitOfWork.PropostaRenovacaoVigenciaLCRepository.Update(proposta);
                }
                else
                {
                    var comites = await _unitOfWork.PropostaRenovacaoVigenciaLCComiteRepository.GetAllFilterAsync(c => c.PropostaRenovacaoVigenciaLCID == comite.PropostaRenovacaoVigenciaLCID);
                    var ultimoNivelComiteID = comites.OrderByDescending(c => c.Nivel).Select(c => c.ID).FirstOrDefault();

                    var proposta = await _unitOfWork.PropostaRenovacaoVigenciaLCRepository.GetAsync(c => c.ID == comite.PropostaRenovacaoVigenciaLCID);

                    if (ultimoNivelComiteID == comite.ID)
                    {
                        proposta.PropostaLCStatusID = "AA";
                        proposta.UsuarioIDAlteracao = comite.UsuarioID;
                        proposta.DataAlteracao = DateTime.Now;

                        var propostaDto = proposta.MapTo<PropostaRenovacaoVigenciaLCDto>();

                        await RenovaVigenciaLCClientes(propostaDto, comite.UsuarioID);
                    }
                    else
                    {
                        var proximoComite = await _unitOfWork.PropostaRenovacaoVigenciaLCComiteRepository.GetAsync(c => c.PropostaRenovacaoVigenciaLCID == comite.PropostaRenovacaoVigenciaLCID && c.Nivel == (comite.Nivel + 1));

                        proximoComite.StatusComiteID = "AA";

                        proposta.ResponsavelID = proximoComite.UsuarioID;
                        proposta.UsuarioIDAlteracao = comite.UsuarioID;
                        proposta.DataAlteracao = DateTime.Now;                     

                        _unitOfWork.PropostaRenovacaoVigenciaLCComiteRepository.Update(proximoComite);

                        var proximoComiteDto = proximoComite.MapTo<PropostaRenovacaoVigenciaLCComiteDto>();
                        proximoComiteDto.EmpresaID = decisaoComitePropostaRenovacaoVigenciaLC.EmpresasID;

                        await SendEmail(proximoComiteDto, URL);
                    }

                    _unitOfWork.PropostaRenovacaoVigenciaLCRepository.Update(proposta);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            
            return _unitOfWork.Commit();
        }

        private async Task SendEmail(PropostaRenovacaoVigenciaLCComiteDto comite, string URL)
        {
            try
            {
                var email = new AppServiceEnvioEmail(_unitOfWork);
                await email.SendMailComiteRenovacaoVigenciaLC(comite, URL);
            }
            catch
            {

            }
        }

        private async Task RenovaVigenciaLCClientes(PropostaRenovacaoVigenciaLCDto propostaDto, Guid usuarioIDAlteracao)
        {
            try
            {
                foreach (var clienteApto in propostaDto.ClientesAptos)
                {
                    var contaClienteFinanceiro = await _unitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.ContaClienteID == clienteApto.ContaClienteID && c.EmpresasID == propostaDto.EmpresaID);

                    if (contaClienteFinanceiro != null)
                    {
                        contaClienteFinanceiro.VigenciaFim = propostaDto.DataNovaVigencia;
                        contaClienteFinanceiro.DataAlteracao = DateTime.Now;
                        contaClienteFinanceiro.UsuarioIDAlteracao = usuarioIDAlteracao;

                        _unitOfWork.ContaClienteFinanceiroRepository.Update(contaClienteFinanceiro);
                    }

                    var propostaLC = await _unitOfWork.PropostaLCRepository.GetLatest(c => c.ContaClienteID == clienteApto.ContaClienteID && (c.PropostaLCStatusID != "AA" && c.PropostaLCStatusID != "XE" && c.PropostaLCStatusID != "XR"));

                    if (propostaLC != null)
                    {
                        propostaLC.VigenciaFinalCliente = propostaDto.DataNovaVigencia;
                        propostaLC.DataAlteracao = contaClienteFinanceiro.DataAlteracao;
                        propostaLC.UsuarioIDAlteracao = usuarioIDAlteracao;

                        _unitOfWork.PropostaLCRepository.Update(propostaLC);
                    }

                    var carteira = new ProcessamentoCarteira
                    {
                        ID = Guid.NewGuid(),
                        Cliente = clienteApto.CodigoCliente,
                        DataHora = DateTime.Now,
                        Status = 2,
                        Motivo = "Renovou a vigência do LC para " + propostaDto.DataNovaVigencia,
                        Detalhes = $"A proposta {propostaDto.NumeroProposta} renovou a vigência do LC do cliente " + clienteApto.NomeCliente,
                        EmpresaID = propostaDto.EmpresaID
                    };

                    _unitOfWork.ProcessamentoCarteiraRepository.Insert(carteira);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
