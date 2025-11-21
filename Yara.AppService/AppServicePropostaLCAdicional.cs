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
    public class AppServicePropostaLCAdicional : IAppServicePropostaLCAdicional
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServicePropostaLCAdicional(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PropostaLCAdicionalDto>> GetAllAsync()
        {
            var propostas = await _unitOfWork.PropostaLCAdicionalRepository.GetAllAsync();
            var propostasDto = Mapper.Map<IEnumerable<PropostaLCAdicionalDto>>(propostas);

            foreach (var propostaDto in propostasDto)
            {
                var acompanhamentos = await _unitOfWork.PropostaLCAdicionalAcompanhamentoRepository.GetAllFilterAsync(c => c.PropostaLCAdicionalID.Equals(propostaDto.ID) && c.Ativo);
                propostaDto.UsuarioIdAcompanhamento = acompanhamentos.Select(c => c.UsuarioID).ToList();

                var usuario = await _unitOfWork.UsuarioRepository.GetAsync(c => c.ID.Equals(propostaDto.UsuarioIDCriacao));
                propostaDto.UsuarioCriacaoNome = Mapper.Map<UsuarioDto>(usuario).Nome;
            }

            return propostasDto;
        }

        public async Task<IEnumerable<PropostaLCAdicionalDto>> GetAllFilterAsync(Expression<Func<PropostaLCAdicionalDto, bool>> expression)
        {
            var propostas = await _unitOfWork.PropostaLCAdicionalRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<PropostaLCAdicional, bool>>>(expression));
            var propostasDto = Mapper.Map<IEnumerable<PropostaLCAdicionalDto>>(propostas);

            foreach (var propostaDto in propostasDto)
            {
                var acompanhamentos = await _unitOfWork.PropostaLCAdicionalAcompanhamentoRepository.GetAllFilterAsync(c => c.PropostaLCAdicionalID.Equals(propostaDto.ID) && c.Ativo);
                propostaDto.UsuarioIdAcompanhamento = acompanhamentos.Select(c => c.UsuarioID).ToList();

                var usuario = await _unitOfWork.UsuarioRepository.GetAsync(c => c.ID.Equals(propostaDto.UsuarioIDCriacao));
                propostaDto.UsuarioCriacaoNome = Mapper.Map<UsuarioDto>(usuario).Nome;
            }

            return propostasDto;
        }

        public async Task<PropostaLCAdicionalDto> GetAsync(Expression<Func<PropostaLCAdicionalDto, bool>> expression)
        {
            var proposta = await _unitOfWork.PropostaLCAdicionalRepository.GetAsync(Mapper.Map<Expression<Func<PropostaLCAdicional, bool>>>(expression));
            var propostaDto = Mapper.Map<PropostaLCAdicionalDto>(proposta);

            var acompanhamentos = await _unitOfWork.PropostaLCAdicionalAcompanhamentoRepository.GetAllFilterAsync(c => c.PropostaLCAdicionalID.Equals(propostaDto.ID) && c.Ativo);
            propostaDto.UsuarioIdAcompanhamento = acompanhamentos.Select(c => c.UsuarioID).ToList();

            var usuario = await _unitOfWork.UsuarioRepository.GetAsync(c => c.ID.Equals(propostaDto.UsuarioIDCriacao));
            propostaDto.UsuarioCriacaoNome = Mapper.Map<UsuarioDto>(usuario).Nome;

            return propostaDto;
        }

        public bool Insert(PropostaLCAdicionalDto obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(PropostaLCAdicionalDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertPropostalAsync(PropostaLCAdicionalDto obj)
        {
            try
            {
                var membroAtual = await _unitOfWork.GrupoEconomicoMembroReporitory.GetAsync(c => c.ContaClienteID == obj.ContaClienteID && c.GrupoEconomico.EmpresasID.Equals(obj.EmpresaID) && c.Ativo && c.GrupoEconomico.Ativo && c.GrupoEconomico.TipoRelacaoGrupoEconomico.ClassificacaoGrupoEconomicoID == 1);
                if (membroAtual != null && !membroAtual.MembroPrincipal)
                {
                    var membroPrincipal = await _unitOfWork.GrupoEconomicoMembroReporitory.GetAsync(c => c.GrupoEconomicoID == membroAtual.GrupoEconomicoID && c.MembroPrincipal && c.GrupoEconomico.EmpresasID.Equals(obj.EmpresaID) && c.Ativo && c.GrupoEconomico.Ativo && c.GrupoEconomico.TipoRelacaoGrupoEconomico.ClassificacaoGrupoEconomicoID == 1);
                    throw new ArgumentException($"Cliente faz parte de Grupo Econômico Compartilhado. Favor criar proposta no Integrante Principal {membroPrincipal.ContaCliente.Nome}.");
                }

                var cliente = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(obj.ContaClienteID));
                if (cliente == null)
                {
                    throw new ArgumentException("Cliente inexistente.");
                }

                var gruposEconomicosMembro = await _unitOfWork.GrupoEconomicoMembroReporitory.GetAllFilterAsync(c => c.ContaClienteID.Equals(obj.ContaCliente) && c.Ativo);
                var idsGrupos = gruposEconomicosMembro.Select(s => s.GrupoEconomicoID).ToList();
                var gruposEconomicos = await _unitOfWork.GrupoEconomicoReporitory.GetAllFilterAsync(c => idsGrupos.Contains(c.ID));

                var grupoCompartilhado = gruposEconomicos.FirstOrDefault(c => c.ClassificacaoGrupoEconomicoID == 1);
                if (grupoCompartilhado != null)
                {
                    var principal = gruposEconomicosMembro.FirstOrDefault(c => c.GrupoEconomicoID.Equals(grupoCompartilhado.ID) && c.MembroPrincipal && c.Ativo);
                    if (principal != null && principal.ContaClienteID != obj.ContaClienteID)
                    {
                        throw new ArgumentException($"Cliente faz parte de Grupo Econômico Compartilhado. Favor criar proposta no Integrante Principal {principal.ContaCliente.Nome}.");
                    }

                    //obj.GrupoEconomicoID = grupoCompartilhado.ID;
                }

                var grupoIndividual = gruposEconomicos.FirstOrDefault(c => c.ClassificacaoGrupoEconomicoID == 2);
                if (grupoIndividual != null)
                {
                    var principal = gruposEconomicosMembro.FirstOrDefault(c => c.GrupoEconomicoID.Equals(grupoCompartilhado.ID) && c.MembroPrincipal && c.Ativo);
                    if (principal != null && principal.ContaClienteID != obj.ContaClienteID)
                    {
                        throw new ArgumentException($"Cliente faz parte de Grupo Econômico Compartilhado. Favor criar proposta no Integrante Principal {principal.ContaCliente.Nome}.");
                    }

                    //obj.GrupoEconomicoID = grupoIndividual.ID;
                }

                var novaProposta = new PropostaLCAdicional
                {
                    ID = obj.ID,
                    DataCriacao = obj.DataCriacao,
                    UsuarioIDCriacao = obj.UsuarioIDCriacao,

                    NumeroInternoProposta = _unitOfWork.PropostaLCAdicionalRepository.GetMaxNumeroInterno(),
                    PropostaLCStatusID = "XC",
                    EmpresaID = obj.EmpresaID,

                    CodigoSap = obj.CodigoSap,
                    LCAdicional = obj.LCAdicional,
                    VigenciaAdicional = obj.VigenciaAdicional,
                    Parecer = obj.Parecer,

                    ContaClienteID = obj.ContaClienteID,

                    ResponsavelID = obj.ResponsavelID

                    //GrupoEconomicoID = obj.GrupoEconomicoID,
                    //TipoClienteID = obj.TipoClienteID,
                };

                // Estes campos são para recuperar o LC e Vigencia caso a proposta seja encerrada por algum motivo.
                var financeiro = await _unitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.ContaClienteID.Equals(novaProposta.ContaClienteID) && c.EmpresasID == novaProposta.EmpresaID);

                if (financeiro != null)
                {
                    novaProposta.LCCliente = financeiro.LC;
                    novaProposta.VigenciaInicialCliente = financeiro.Vigencia;
                    novaProposta.VigenciaFinalCliente = financeiro.VigenciaFim;
                }

                _unitOfWork.PropostaLCAdicionalRepository.Insert(novaProposta);

                if (obj.AcompanharProposta)
                {
                    await SaveFollow(novaProposta);
                }

                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> UpdatePropostalAsync(PropostaLCAdicionalDto propostaLCAdicional)
        {
            try
            {
                var proposta = await _unitOfWork.PropostaLCAdicionalRepository.GetAsync(c => c.ID.Equals(propostaLCAdicional.ID));

                proposta.CodigoSap = propostaLCAdicional.CodigoSap;
                proposta.ResponsavelID = propostaLCAdicional.ResponsavelID;

                _unitOfWork.PropostaLCAdicionalRepository.Update(proposta);

                proposta.AcompanharProposta = propostaLCAdicional.AcompanharProposta;

                await SaveFollow(proposta);

                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> CancelProposalAsync(Guid propostaLCAdicionalID, Guid usuarioIDAlteracao, string URL)
        {
            try
            {
                var proposta = await _unitOfWork.PropostaLCAdicionalRepository.GetAsync(c => c.ID.Equals(propostaLCAdicionalID));

                proposta.PropostaLCStatusID = "XE";
                proposta.UsuarioIDAlteracao = usuarioIDAlteracao;
                proposta.DataAlteracao = DateTime.Now;
                proposta.ResponsavelID = null;

                _unitOfWork.PropostaLCAdicionalRepository.Update(proposta);

                // Estes campos são para recuperar o LC e Vigencia caso a proposta seja encerrada por algum motivo.
                var financeiro = await _unitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.ContaClienteID.Equals(proposta.ContaClienteID) && c.EmpresasID == proposta.EmpresaID);

                if (financeiro != null)
                {
                    proposta.LCCliente = financeiro.LC;
                    proposta.VigenciaInicialCliente = financeiro.Vigencia;
                    proposta.VigenciaFinalCliente = financeiro.VigenciaFim;
                }

                _unitOfWork.PropostaLCAdicionalRepository.Update(proposta);

                try
                {
                    var user = await _unitOfWork.UsuarioRepository.GetAsync(c => c.ID.Equals(usuarioIDAlteracao));

                    var email = new AppServiceEnvioEmail(_unitOfWork);
                    await email.SendMailFeedBackPropostas(user.MapTo<UsuarioDto>(), propostaLCAdicionalID, "Proposta de limite de crédito adicional encerrada.", proposta.ContaClienteID, URL);
                }
                catch
                {

                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return _unitOfWork.Commit();
        }

        public async Task<bool> FixLimitProposalAsync(ContaClienteFinanceiroDto clienteFinanceiroDto, string URL)
        {
            var proposta = await _unitOfWork.PropostaLCAdicionalRepository.GetAsync(c => c.ID.Equals(clienteFinanceiroDto.PropostaLCAdicionalId));

            var contaClienteFinanceiro = await _unitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.ContaClienteID.Equals(clienteFinanceiroDto.ContaClienteID) && c.EmpresasID.Equals(clienteFinanceiroDto.EmpresasID));
            if (contaClienteFinanceiro != null)
            {
                contaClienteFinanceiro.LCAnterior = contaClienteFinanceiro.LC;
                contaClienteFinanceiro.VigenciaAnterior = contaClienteFinanceiro.Vigencia;
                contaClienteFinanceiro.VigenciaFimAnterior = contaClienteFinanceiro.VigenciaFim;

                contaClienteFinanceiro.LCAdicional = clienteFinanceiroDto.LC;
                contaClienteFinanceiro.VigenciaAdicional = DateTime.Now;
                contaClienteFinanceiro.VigenciaAdicionalFim = clienteFinanceiroDto.VigenciaFim;

                contaClienteFinanceiro.DataAlteracao = DateTime.Now;
                contaClienteFinanceiro.UsuarioIDAlteracao = clienteFinanceiroDto.UsuarioIDCriacao;

                _unitOfWork.ContaClienteFinanceiroRepository.Update(contaClienteFinanceiro);
            }
            else
            {
                contaClienteFinanceiro.ContaClienteID = clienteFinanceiroDto.ContaClienteID;
                contaClienteFinanceiro.EmpresasID = clienteFinanceiroDto.EmpresasID;

                contaClienteFinanceiro.LCAnterior = Convert.ToDecimal(0);
                contaClienteFinanceiro.VigenciaAnterior = DateTime.Now;
                contaClienteFinanceiro.VigenciaFimAnterior = DateTime.Now;

                contaClienteFinanceiro.LCAdicional = clienteFinanceiroDto.LC;
                contaClienteFinanceiro.VigenciaAdicional = DateTime.Now;
                contaClienteFinanceiro.VigenciaAdicionalFim = clienteFinanceiroDto.VigenciaFim;

                contaClienteFinanceiro.DataCriacao = DateTime.Now;
                contaClienteFinanceiro.UsuarioIDCriacao = clienteFinanceiroDto.UsuarioIDCriacao;

                _unitOfWork.ContaClienteFinanceiroRepository.Insert(contaClienteFinanceiro);
            }

            var contaCliente = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID == clienteFinanceiroDto.ContaClienteID);
            if (contaCliente != null)
            {
                var carteira = new ProcessamentoCarteira
                {
                    ID = Guid.NewGuid(),
                    Cliente = contaCliente.CodigoPrincipal,
                    DataHora = DateTime.Now,
                    Status = 2,
                    Motivo = "LC - Fixou Limite de Crédito Adicional para o Cliente: " + contaCliente.Nome,
                    Detalhes = "LC - O Limite de Crédito adicional fixado foi no valor de: " + clienteFinanceiroDto.LC,
                    EmpresaID = clienteFinanceiroDto.EmpresasID
                };

                _unitOfWork.ProcessamentoCarteiraRepository.Insert(carteira);
            }

            if (proposta != null)
            {
                proposta.PropostaLCStatusID = "AA";

                _unitOfWork.PropostaLCAdicionalRepository.Update(proposta);
            }

            bool commit = _unitOfWork.Commit();

            if (commit)
            {
                //try
                //{
                //    var rfc = new AppServiceRFCSap(_unitOfWork);
                //    await rfc.EnviarFixacaoLimite(proposta.ID);
                //}
                //catch (Exception e)
                //{
                //    throw e;
                //}

                if (!proposta.ResponsavelID.Equals(proposta.UsuarioIDCriacao))
                {
                    try
                    {
                        var user = await _unitOfWork.UsuarioRepository.GetAsync(c => c.ID.Equals(proposta.UsuarioIDCriacao));

                        var email = new AppServiceEnvioEmail(_unitOfWork);
                        await email.SendMailFeedBackPropostas(user.MapTo<UsuarioDto>(), proposta.ID, "Proposta de limite de crédito adicional aprovada.", proposta.ContaClienteID, URL);
                    }
                    catch
                    {

                    }
                }
            }

            return commit;
        }

        private async Task SaveFollow(PropostaLCAdicional obj)
        {
            try
            {
                var acompanha = await _unitOfWork.PropostaLCAdicionalAcompanhamentoRepository.GetAsync(c => c.PropostaLCAdicionalID.Equals(obj.ID) && c.UsuarioID.Equals(obj.UsuarioIDCriacao));
                if (acompanha != null)
                {
                    acompanha.Ativo = obj.AcompanharProposta;
                    _unitOfWork.PropostaLCAdicionalAcompanhamentoRepository.Update(acompanha);
                }
                else
                {
                    var acompanhar = new PropostaLCAdicionalAcompanhamento()
                    {
                        UsuarioID = obj.UsuarioIDCriacao,
                        DataCriacao = DateTime.Now,
                        Ativo = true,
                        PropostaLCAdicionalID = obj.ID
                    };

                    _unitOfWork.PropostaLCAdicionalAcompanhamentoRepository.Insert(acompanhar);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //private async Task AddProcessamentoCarteira(ContaClienteFinanceiroDto clienteFinanceiroDto)
        //{
        //    var contaCliente = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID == clienteFinanceiroDto.ContaClienteID);
        //    var carteiraDto = new ProcessamentoCarteiraDto();
        //    carteiraDto.ID = Guid.NewGuid();
        //    carteiraDto.Cliente = contaCliente.CodigoPrincipal;
        //    carteiraDto.DataHora = DateTime.Now;
        //    carteiraDto.Status = 2;
        //    carteiraDto.Motivo = "LC - Fixou Limite de Crédito Adicional para o Cliente: " + contaCliente.Nome;
        //    carteiraDto.Detalhes = "LC - O Limite de Crédito adicional fixado foi no valor de: " + clienteFinanceiroDto.LC;
        //    carteiraDto.EmpresaID = clienteFinanceiroDto.EmpresasID;
        //    _unitOfWork.ProcessamentoCarteiraRepository.Insert(carteiraDto.MapTo<ProcessamentoCarteira>());
        //}
    }
}
