using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using WebGrease.Css.Extensions;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.AppService
{
    public class AppServicePropostaLC : IAppServicePropostaLC
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServicePropostaLC(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PropostaLCValidacaoDto> ValidaProposta(Guid contaClienteId, Guid usuario, string empresaId)
        {
            var proposta = _unitOfWork.PropostaLCRepository.GetAllFilterOrderBy(c => c.ContaClienteID.Equals(contaClienteId) && c.EmpresaID.Equals(empresaId), c => c.DataCriacao);
            var retorno = new PropostaLCValidacaoDto();

            if (proposta != null)
            {
                if (proposta.PropostaLCStatusID != "AA" && proposta.PropostaLCStatusID != "XE" && proposta.PropostaLCStatusID != "XR")
                {
                    var proposto = proposta.LCProposto ?? 0;
                    retorno.UsuarioID = null;
                    retorno.PropostaLCID = proposta.ID;

                    // var user = await _unitOfWork.UsuarioRepository.GetAsync(c => c.ID.Equals(proposta.UsuarioIDCriacao));
                    retorno.LcProposto = proposta.LCProposto;
                    retorno.Mensagem = $"Proposta de LC em andamento " + proposto.ToString("C");
                }
                else
                {
                    retorno.UsuarioID = null;
                    retorno.PropostaLCID = null;
                    retorno.Mensagem = "Solicitar Proposta de LC";
                }
            }
            else
            {
                retorno.UsuarioID = null;
                retorno.PropostaLCID = null;
                retorno.Mensagem = "Solicitar Proposta de LC";
            }

            return retorno;
        }

        public async Task<PropostaLCDto> Save(PropostaLCDto propostaLC)
        {
            if (propostaLC.ID.Equals(Guid.Empty))
            {
                // Verifica se cliente possui um grupo e é membro principal, caso não prosseguir, caso sim, não permitir criar a proposta por não ser
                var membroAtual = await _unitOfWork.GrupoEconomicoMembroReporitory.GetAsync(c => c.ContaClienteID == propostaLC.ContaClienteID && c.GrupoEconomico.EmpresasID.Equals(propostaLC.EmpresaID) && c.Ativo && c.GrupoEconomico.Ativo && c.GrupoEconomico.TipoRelacaoGrupoEconomico.ClassificacaoGrupoEconomicoID == 1);
                if (membroAtual != null && !membroAtual.MembroPrincipal)
                {
                    var membroPrincipal = await _unitOfWork.GrupoEconomicoMembroReporitory.GetAsync(c => c.GrupoEconomicoID == membroAtual.GrupoEconomicoID && c.MembroPrincipal && c.GrupoEconomico.EmpresasID.Equals(propostaLC.EmpresaID) && c.Ativo && c.GrupoEconomico.Ativo && c.GrupoEconomico.TipoRelacaoGrupoEconomico.ClassificacaoGrupoEconomicoID == 1);
                    throw new ArgumentException($"Cliente faz parte de Grupo Econômico Compartilhado. Favor criar proposta no Integrante Principal {membroPrincipal.ContaCliente.Nome}.");
                }

                // Verifica se o cliente possui alguma proposta de Alçada Comercial em andamento.
                var propostaAC = await _unitOfWork.PropostaAlcadaComercial.GetLatest(c => c.ContaClienteID.Equals(propostaLC.ContaClienteID) && c.EmpresaID.Equals(propostaLC.EmpresaID));
                if (propostaAC != null && propostaAC.PropostaCobrancaStatus.ID != "AA" && propostaAC.PropostaCobrancaStatus.ID != "AP" && propostaAC.PropostaCobrancaStatus.ID != "EN" && propostaAC.PropostaCobrancaStatus.ID != "RE")
                {
                    throw new ArgumentException("Proposta de Alçada Comercial em andamento.");
                }

                // Verifica se o cliente possui alguma proposta de LC Adicional em andamento.
                var propostaLCA = await _unitOfWork.PropostaLCAdicionalRepository.GetLatest(c => c.ContaClienteID.Equals(propostaLC.ContaClienteID) && c.EmpresaID.Equals(propostaLC.EmpresaID));
                if (propostaLCA != null && propostaLCA.PropostaLCStatusID != "AA" && propostaLCA.PropostaLCStatusID != "XE" && propostaLCA.PropostaLCStatusID != "XR")
                {
                    throw new ArgumentException("Proposta de LC Adicional em andamento.");
                }

                var representante = await _unitOfWork.ContaClienteRepresentanteRepository.GetAsync(c => c.ContaClienteID.Equals(propostaLC.ContaClienteID) && c.Representante.Usuarios.Any(u => u.ID.Equals(propostaLC.UsuarioIDCriacao)));
                if (representante != null)
                    propostaLC.RepresentanteID = representante.RepresentanteID;

                var nextNumber = _unitOfWork.PropostaLCRepository.GetMaxNumeroInterno();
                propostaLC.NumeroInternoProposta = nextNumber;
                propostaLC.DataCriacao = DateTime.Now;
                propostaLC.PropostaLCStatusID = "XC";

                // Proprietário da Proposta
                var contaClienteProposta = await _unitOfWork.ContaClienteRepository.GetAsync(cc => cc.ID == propostaLC.ContaClienteID);

                // Patrimônios da Proposta
                var patrimoniosProposta = await this.GetPatrimonio(contaClienteProposta.Documento);

                // Verifica se cliente possui um proposta anterior, caso não criar zerado, caso sim, busca proposta anterior.
                var propostalc = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ContaClienteID.Equals(propostaLC.ContaClienteID) && c.EmpresaID.Equals(propostaLC.EmpresaID) && (c.PropostaLCStatusID == "AA" || c.PropostaLCStatusID == "XE" || c.PropostaLCStatusID == "XR"));
                if (propostalc != null)
                {
                    var propostaLcCopia = await _unitOfWork.PropostaLCRepository.CriaPropostaCopiaAnterior(propostaLC.UsuarioIDCriacao, nextNumber, propostaLC.UsuarioIDCriacao, propostaLC.ContaClienteID, propostaLC.EmpresaID);

                    propostaLC.ID = propostaLcCopia.ID;

                    // Patrimonios
                    var copiaSalva = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ID.Equals(propostaLcCopia.ID));
                    var copiaDto = copiaSalva.MapTo<PropostaLCDto>();

                    copiaDto.BensRurais = new List<PropostaLCBemRuralDto>();
                    copiaDto.BensUrbanos = new List<PropostaLCBemUrbanoDto>();
                    copiaDto.MaquinasEquipamentos = new List<PropostaLCMaquinaEquipamentoDto>();
                    copiaDto.PrecosPorRegiao = new List<PropostaLCPrecoPorRegiaoDto>();

                    if (patrimoniosProposta.BemRural != null && patrimoniosProposta.BemRural.Count > 0)
                        patrimoniosProposta.BemRural.ForEach(br => { br.GarantiaID = null; br.PropostaLCGarantia = null; copiaDto.BensRurais.Add(br); });

                    if (patrimoniosProposta.BemUrbano != null && patrimoniosProposta.BemUrbano.Count > 0)
                        patrimoniosProposta.BemUrbano.ForEach(bu => { bu.GarantiaID = null; bu.PropostaLCGarantia = null; copiaDto.BensUrbanos.Add(bu); });

                    if (patrimoniosProposta.MaquinasEquipamentos != null && patrimoniosProposta.MaquinasEquipamentos.Count > 0)
                        patrimoniosProposta.MaquinasEquipamentos.ForEach(me => { me.GarantiaID = null; me.PropostaLCGarantia = null; copiaDto.MaquinasEquipamentos.Add(me); });

                    if (patrimoniosProposta.PrecosPorRegiao != null && patrimoniosProposta.PrecosPorRegiao.Count > 0)
                        patrimoniosProposta.PrecosPorRegiao.ForEach(ppr => { copiaDto.PrecosPorRegiao.Add(ppr); });

                    // Garantidos
                    foreach (var gar in copiaDto.Garantias)
                    {
                        var patrimoniosGarantia = await this.GetPatrimonio(gar.Documento);

                        if (patrimoniosGarantia.BemRural != null && patrimoniosGarantia.BemRural.Count > 0)
                            patrimoniosGarantia.BemRural.ForEach(br => { br.GarantiaID = gar.ID; copiaDto.BensRurais.Add(br); });

                        if (patrimoniosGarantia.BemUrbano != null && patrimoniosGarantia.BemUrbano.Count > 0)
                            patrimoniosGarantia.BemUrbano.ForEach(bu => { bu.GarantiaID = gar.ID; copiaDto.BensUrbanos.Add(bu); });

                        if (patrimoniosGarantia.MaquinasEquipamentos != null && patrimoniosGarantia.MaquinasEquipamentos.Count > 0)
                            patrimoniosGarantia.MaquinasEquipamentos.ForEach(me => { me.GarantiaID = gar.ID; copiaDto.MaquinasEquipamentos.Add(me); });

                        if (patrimoniosGarantia.PrecosPorRegiao != null && patrimoniosGarantia.PrecosPorRegiao.Count > 0)
                            patrimoniosGarantia.PrecosPorRegiao.ForEach(ppr => { copiaDto.PrecosPorRegiao.Add(ppr); });
                    }

                    await Atualizar(copiaDto);
                }
                else
                {
                    propostaLC.ID = Guid.NewGuid();

                    if (patrimoniosProposta.BemRural != null && patrimoniosProposta.BemRural.Count > 0)
                    {
                        propostaLC.BensRurais = patrimoniosProposta.BemRural;
                        propostaLC.BensRurais.ForEach(br => { br.GarantiaID = null; br.PropostaLCGarantia = null; });
                    }

                    if (patrimoniosProposta.BemUrbano != null && patrimoniosProposta.BemUrbano.Count > 0)
                    {
                        propostaLC.BensUrbanos = patrimoniosProposta.BemUrbano;
                        propostaLC.BensUrbanos.ForEach(bu => { bu.GarantiaID = null; bu.PropostaLCGarantia = null; });
                    }

                    if (patrimoniosProposta.MaquinasEquipamentos != null && patrimoniosProposta.MaquinasEquipamentos.Count > 0)
                    {
                        propostaLC.MaquinasEquipamentos = patrimoniosProposta.MaquinasEquipamentos;
                        propostaLC.MaquinasEquipamentos.ForEach(me => { me.GarantiaID = null; me.PropostaLCGarantia = null; });
                    }

                    if (patrimoniosProposta.PrecosPorRegiao != null && patrimoniosProposta.PrecosPorRegiao.Count > 0)
                        propostaLC.PrecosPorRegiao = patrimoniosProposta.PrecosPorRegiao;

                    // Estes campos são para recuperar o LC e Vigencia caso a proposta seja encerrada por algum motivo.
                    var financeiro = await _unitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.ContaClienteID.Equals(propostaLC.ContaClienteID) && c.EmpresasID == propostaLC.EmpresaID);

                    if (financeiro != null)
                    {
                        propostaLC.LCCliente = financeiro.LC;
                        propostaLC.VigenciaInicialCliente = financeiro.Vigencia;
                        propostaLC.VigenciaFinalCliente = financeiro.VigenciaFim;
                        propostaLC.RatingCliente = financeiro.Rating;
                    }

                    Inserir(propostaLC);
                }
            }
            else
            {
                var propUpd = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ID.Equals(propostaLC.ID));

                if (propUpd != null)
                {
                    if (!propUpd.EmpresaID.Equals(propostaLC.EmpresaID))
                        throw new ArgumentException("Tentando atualizar proposta de outra empresa.");

                    await Atualizar(propostaLC);
                }
            }

            var cliente = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(propostaLC.ContaClienteID));

            cliente.TipoClienteID = propostaLC.TipoClienteID;

            _unitOfWork.ContaClienteRepository.Update(cliente);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }

            try
            {
                await SaveAcompanhamento(propostaLC);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }

            var propostadto = await GetProposalByAccountID(propostaLC.ContaClienteID, propostaLC.EmpresaID, propostaLC.ID);

            return propostadto;
        }

        // Tem envio de e-mail: NÃO.
        // APIs que usam este método:
        // v1/sendpreAnalise
        public async Task<bool> SaveStatusWithAnexo(PropostaLCDto proposta)
        {
            var propostalc = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ID.Equals(proposta.ID) && c.EmpresaID.Equals(proposta.EmpresaID));
            propostalc.PropostaLCStatusID = proposta.PropostaLCStatusID;
            propostalc.DataAlteracao = DateTime.Now;
            propostalc.UsuarioIDAlteracao = proposta.UsuarioIDAlteracao;
            propostalc.ResponsavelID = proposta.ResponsavelID;
            propostalc.CodigoSap = proposta.CodigoSap;

            _unitOfWork.PropostaLCRepository.Update(propostalc);

            SaveHistorico(propostalc);

            await SaveAnexoPreAnalise(propostalc.ID, propostalc.ContaClienteID);

            return _unitOfWork.Commit();
        }

        public async Task SaveAnexoPreAnalise(Guid propostaID, Guid ContaClienteID)
        {
            var anexos = await _unitOfWork.AnexoArquivoRepository.GetAllFilterAsync(c => c.PropostaLCID == propostaID);
            anexos.ForEach(c => c.ContaClienteID = ContaClienteID);

            foreach (var anexo in anexos)
            {
                _unitOfWork.AnexoArquivoRepository.Update(anexo);
            }
        }

        // Tem envio de e-mail: SIM, do tipo Feedback.
        // APIs que usam este método para disparar e-mail:
        // v1/sendAnalise
        // v1/sendCTC
        // v1/sendEncerrar
        // v1/edit
        public async Task<bool> SaveStatus(PropostaLCDto proposta, string Mensagem, string URL)
        {
            var propostalc = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ID.Equals(proposta.ID) && c.EmpresaID.Equals(proposta.EmpresaID));
            propostalc.PropostaLCStatusID = proposta.PropostaLCStatusID;
            propostalc.DataAlteracao = DateTime.Now;
            propostalc.UsuarioIDAlteracao = proposta.UsuarioIDAlteracao;
            propostalc.ResponsavelID = proposta.ResponsavelID;

            switch (proposta.PropostaLCStatusID)
            {
                case "FA": // Enviada para Pré-Análise
                    {
                        propostalc.SolicitanteSerasaID = proposta.SolicitanteSerasaID;
                    }
                    break;
                case "CA": // Aguardando Parecer do CTC
                    {
                        propostalc.CodigoSap = proposta.CodigoSap;

                        if (!proposta.ResponsavelID.HasValue)
                            break;

                        try
                        {
                            await SendEmail(proposta.ID, proposta.ResponsavelID.Value, "Proposta de limite de crédito aguardando a descrição do seu parecer.", URL);
                        }
                        catch
                        {

                        }
                    }
                    break;
                case "XE": // Encerrada
                    {
                        //Retorna Limite de Credito e Vigencias anteriores
                        await RetornaLimiteFinanceiro(proposta);

                        try
                        {
                            await SendEmail(proposta.ID, proposta.UsuarioIDCriacao, "Proposta de limite de crédito encerrada.", URL);
                        }
                        catch
                        {

                        }
                    }
                break;
            }

            _unitOfWork.PropostaLCRepository.Update(propostalc);

            SaveHistorico(propostalc);

            return _unitOfWork.Commit();
        }

        // Tem envio de e-mail: NÃO.
        // APIs que usam este método:
        // v1/sendAnalise
        public async Task<bool> SaveStatusAbortaComite(PropostaLCDto proposta)
        {
            var propostalc = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ID.Equals(proposta.ID) && c.EmpresaID.Equals(proposta.EmpresaID));
            propostalc.PropostaLCStatusID = proposta.PropostaLCStatusID;
            propostalc.DataAlteracao = DateTime.Now;
            propostalc.UsuarioIDAlteracao = proposta.UsuarioIDAlteracao;
            propostalc.ResponsavelID = proposta.ResponsavelID;
            propostalc.PropostaLCStatusID = "FE";

            _unitOfWork.PropostaLCRepository.Update(propostalc);

            SaveHistorico(propostalc);

            //Aborta Fluxo do Comite
            var retorno = await _unitOfWork.PropostaLcComiteRepository.AbortaComite(proposta.ID);

            //Retorna Limite de Credito e Vigencias anteriores
            await RetornaLimiteFinanceiro(proposta);

            if (retorno)
            {
                return _unitOfWork.Commit();
            }
            else
            {
                return false;
            }
        }

        // Tem envio de e-mail: SIM, do tipo Blog.
        // APIs que usam este método para disparar e-mail:
        // v1/pendenciarepresentante
        public async Task<bool> SaveStatusWithRepresentante(PropostaLCDto proposta, string Mensagem, string URL)
        {
            var propostalc = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ID.Equals(proposta.ID) && c.EmpresaID.Equals(proposta.EmpresaID));

            var representant = await _unitOfWork.RepresentanteRepository.GetAsync(c => c.ID == proposta.RepresentanteID);
            if (representant.Usuarios == null)
                throw new ArgumentException("Este Representante não possuí usuários vinculados.");

            var usuario = representant.Usuarios.OrderByDescending(c => c.DataAlteracao).FirstOrDefault();
            if (usuario == null)
                throw new ArgumentException("Este Representante não possuí usuários vinculados.");

            propostalc.PropostaLCStatusID = proposta.PropostaLCStatusID;
            propostalc.DataAlteracao = DateTime.Now;
            propostalc.UsuarioIDAlteracao = proposta.UsuarioIDAlteracao;
            propostalc.ResponsavelID = usuario.ID;
            propostalc.CodigoSap = proposta.CodigoSap;

            _unitOfWork.PropostaLCRepository.Update(propostalc);

            var blog = new AppServiceBlog(_unitOfWork);
            if (propostalc.UsuarioIDAlteracao != null)
            {
                var insertblog = new BlogDto
                {
                    Area = propostalc.ID,
                    ContaClienteID = propostalc.ContaClienteID,
                    EmpresaID = propostalc.EmpresaID,
                    DataCriacao = DateTime.Now,
                    Mensagem = Mensagem,
                    ParaID = propostalc.ResponsavelID,
                    UsuarioCriacaoID = propostalc.UsuarioIDAlteracao.Value
                };

                await blog.InsertAsync(insertblog, URL);
            }

            SaveHistorico(propostalc);

            return _unitOfWork.Commit();
        }

        // Tem envio de e-mail: SIM, do tipo Blog.
        // APIs que usam este método para disparar e-mail:
        // v1/pendenciactc
        // v1/encerraefixalimite
        // v1/sendAprovarPendente
        // v1/sendAprovarGarantia
        public async Task<bool> SaveStatusWithPending(PropostaLCDto proposta, string Mensagem, string URL)
        {
            var propostalc = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ID.Equals(proposta.ID) && c.EmpresaID.Equals(proposta.EmpresaID));

            propostalc.PropostaLCStatusID = proposta.PropostaLCStatusID;
            propostalc.DataAlteracao = DateTime.Now;
            propostalc.UsuarioIDAlteracao = proposta.UsuarioIDAlteracao;
            propostalc.ResponsavelID = proposta.ResponsavelID;
            propostalc.CodigoSap = proposta.CodigoSap;

            _unitOfWork.PropostaLCRepository.Update(propostalc);

            var blog = new AppServiceBlog(_unitOfWork);
            if (propostalc.UsuarioIDAlteracao != null)
            {
                var insertblog = new BlogDto
                {
                    Area = propostalc.ID,
                    ContaClienteID = propostalc.ContaClienteID,
                    EmpresaID = propostalc.EmpresaID,
                    DataCriacao = DateTime.Now,
                    Mensagem = Mensagem,
                    ParaID = propostalc.ResponsavelID,
                    UsuarioCriacaoID = propostalc.UsuarioIDAlteracao.Value
                };

                await blog.InsertAsync(insertblog, URL);
            }

            SaveHistorico(propostalc);

            return _unitOfWork.Commit();
        }

        // Tem envio de e-mail: Sim, do tipo Feedback.
        // APIs que usam este método para disparar e-mail:
        // v1/fixedLimitClient
        // v1/fixedLimitClientList
        public async Task<bool> LimitFixed(ContaClienteFinanceiroDto clienteFinanceiroDto, string URL)
        {
            var proposta = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ID.Equals(clienteFinanceiroDto.PropostaLCId));

            //Fixa LC de acordo com ultimo valor aprovado pelo comitê.
            var contaClienteFinanceiro = await _unitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.ContaClienteID.Equals(clienteFinanceiroDto.ContaClienteID) && c.EmpresasID.Equals(clienteFinanceiroDto.EmpresasID));
            if (contaClienteFinanceiro != null)
            {
                contaClienteFinanceiro.LCAnterior = contaClienteFinanceiro.LC;
                contaClienteFinanceiro.VigenciaAnterior = contaClienteFinanceiro.Vigencia;
                contaClienteFinanceiro.VigenciaFimAnterior = contaClienteFinanceiro.VigenciaFim;

                contaClienteFinanceiro.LC = clienteFinanceiroDto.LC;
                contaClienteFinanceiro.Vigencia = DateTime.Now;
                contaClienteFinanceiro.VigenciaFim = clienteFinanceiroDto.VigenciaFim;

                contaClienteFinanceiro.Rating = proposta.Rating;

                contaClienteFinanceiro.DataAlteracao = DateTime.Now;
                contaClienteFinanceiro.UsuarioIDAlteracao = clienteFinanceiroDto.UsuarioIDCriacao;

                _unitOfWork.ContaClienteFinanceiroRepository.Update(contaClienteFinanceiro);

                await AddProcessamentoCarteira(contaClienteFinanceiro.MapTo<ContaClienteFinanceiroDto>());
            }
            else
            {
                contaClienteFinanceiro.ContaClienteID = clienteFinanceiroDto.ContaClienteID;
                contaClienteFinanceiro.EmpresasID = clienteFinanceiroDto.EmpresasID;

                contaClienteFinanceiro.LCAnterior = Convert.ToDecimal(0);
                contaClienteFinanceiro.VigenciaAnterior = DateTime.Now;
                contaClienteFinanceiro.VigenciaFimAnterior = DateTime.Now;

                contaClienteFinanceiro.LC = clienteFinanceiroDto.LC;
                contaClienteFinanceiro.Vigencia = DateTime.Now;
                contaClienteFinanceiro.VigenciaFim = clienteFinanceiroDto.VigenciaFim;

                // Confirmar se é necessário atualizar o rating para esta situação também...
                // contaClienteFinanceiro.Rating = proposta.Rating;

                contaClienteFinanceiro.DataCriacao = DateTime.Now;
                contaClienteFinanceiro.UsuarioIDCriacao = clienteFinanceiroDto.UsuarioIDCriacao;

                _unitOfWork.ContaClienteFinanceiroRepository.Insert(contaClienteFinanceiro);

                await AddProcessamentoCarteira(contaClienteFinanceiro.MapTo<ContaClienteFinanceiroDto>());
            }

            if (proposta != null)
            {
                proposta.PropostaLCStatusID = "AA";

                try
                {
                    await Atualizar(proposta.MapTo<PropostaLCDto>());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            bool commit = _unitOfWork.Commit();

            if (commit)
            {
                try
                {
                    var rfc = new AppServiceRFCSap(_unitOfWork);
                    await rfc.EnviarFixacaoLimite(proposta.ID);
                }
                catch (Exception e)
                {
                    throw e;
                }

                if (!proposta.ResponsavelID.Equals(proposta.UsuarioIDCriacao))
                {
                    try
                    {
                        await SendEmail(proposta.ID, proposta.UsuarioIDCriacao, "Proposta de limite de crédito aprovada.", URL);
                    }
                    catch
                    {

                    }
                }
            }

            return commit;
        }

        // Tem envio de e-mail: NÃO.
        // APIs que usam este método:
        // v1/fixedLimitClientPartial
        // v1/fixedLimitClientPartialList
        public async Task<bool> LimitFixedPartial(ContaClienteFinanceiroDto clienteFinanceiroDto)
        {
            var proposta = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ID.Equals(clienteFinanceiroDto.PropostaLCId));

            //Fixa LC de acordo com ultimo valor aprovado pelo comitê.
            var contaClienteFinanceiro = await _unitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.ContaClienteID.Equals(clienteFinanceiroDto.ContaClienteID) && c.EmpresasID.Equals(clienteFinanceiroDto.EmpresasID));
            if (contaClienteFinanceiro != null)
            {
                contaClienteFinanceiro.LCAnterior = contaClienteFinanceiro.LC;
                contaClienteFinanceiro.VigenciaAnterior = contaClienteFinanceiro.Vigencia;
                contaClienteFinanceiro.VigenciaFimAnterior = contaClienteFinanceiro.VigenciaFim;

                contaClienteFinanceiro.LC = clienteFinanceiroDto.LC;
                contaClienteFinanceiro.Vigencia = clienteFinanceiroDto.Vigencia;
                contaClienteFinanceiro.VigenciaFim = clienteFinanceiroDto.VigenciaFim;

                contaClienteFinanceiro.Rating = proposta.Rating;

                contaClienteFinanceiro.DataAlteracao = DateTime.Now;
                contaClienteFinanceiro.UsuarioIDAlteracao = clienteFinanceiroDto.UsuarioIDCriacao;

                _unitOfWork.ContaClienteFinanceiroRepository.Update(contaClienteFinanceiro);

                await AddProcessamentoCarteira(contaClienteFinanceiro.MapTo<ContaClienteFinanceiroDto>());
            }
            else
            {
                contaClienteFinanceiro.ContaClienteID = clienteFinanceiroDto.ContaClienteID;
                contaClienteFinanceiro.EmpresasID = clienteFinanceiroDto.EmpresasID;

                contaClienteFinanceiro.LCAnterior = Convert.ToDecimal(0);
                contaClienteFinanceiro.VigenciaAnterior = DateTime.Now;
                contaClienteFinanceiro.VigenciaFimAnterior = DateTime.Now;

                contaClienteFinanceiro.LC = clienteFinanceiroDto.LC;
                contaClienteFinanceiro.Vigencia = clienteFinanceiroDto.Vigencia;
                contaClienteFinanceiro.VigenciaFim = clienteFinanceiroDto.VigenciaFim;

                // Confirmar se é necessário atualizar o rating para esta situação também...
                // contaClienteFinanceiro.Rating = proposta.Rating;

                contaClienteFinanceiro.DataCriacao = DateTime.Now;
                contaClienteFinanceiro.UsuarioIDCriacao = clienteFinanceiroDto.UsuarioIDCriacao;

                _unitOfWork.ContaClienteFinanceiroRepository.Insert(contaClienteFinanceiro);

                await AddProcessamentoCarteira(contaClienteFinanceiro.MapTo<ContaClienteFinanceiroDto>());
            }

            if (proposta != null)
            {
                try
                {
                    await Atualizar(proposta.MapTo<PropostaLCDto>());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            bool commit = _unitOfWork.Commit();

            if (commit)
            {
                try
                {
                    var rfc = new AppServiceRFCSap(_unitOfWork);
                    await rfc.EnviarFixacaoLimite(proposta.ID);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return commit;
        }

        public async Task<List<BuscaPropostaLCContaClienteDto>> GetPropostaLCContaCliente(Guid ContaCliente, string EmpresaID)
        {
            var propostas = await _unitOfWork.PropostaLCRepository.GetPropostaLCContaCliente(ContaCliente, EmpresaID);
            return propostas.MapTo<List<BuscaPropostaLCContaClienteDto>>();
        }

        public async Task<PropostaLCDto> GetProposalByAccountID(Guid id, string empresaId, [Optional] Guid propostaId)
        {
            var proposta = propostaId != Guid.Empty ?
                await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ID == propostaId) :
                _unitOfWork.PropostaLCRepository.GetAllFilterOrderBy(c => c.ContaClienteID.Equals(id) && c.EmpresaID.Equals(empresaId), c => c.DataCriacao);

            if (proposta != null)
            {
                if (proposta.PropostaLCStatusID != "AA" && proposta.PropostaLCStatusID != "XE" && proposta.PropostaLCStatusID != "XR")
                {
                    var propostadto = proposta.MapTo<PropostaLCDto>();

                    var user = propostadto.DataAlteracao.HasValue ? propostadto.UsuarioIDAlteracao : propostadto.UsuarioIDCriacao;

                    var acompanhamento = await _unitOfWork.AcompanhamentoPropostaLCRepository.GetAsync(c => c.PropostaLCID.Equals(propostadto.ID) && c.UsuarioID == user);
                    propostadto.AcompanharProposta = acompanhamento?.Ativo ?? false;

                    var fluxo = await _unitOfWork.PropostaLcComiteRepository.GetAllFilterAsync(c => c.PropostaLCID.Equals(propostadto.ID) && c.StatusComiteID.Equals("AP") && c.Perfil.Descricao.Contains("Analista de Crédito"));
                    propostadto.DataAprovacaoComite = fluxo.Max(m => m.DataAcao);

                    return propostadto;
                }
                return null;
            }
            return null;
        }

        public async Task<PropostaLCDto> GetProposalByID(Guid ID, string empresaID)
        {
            var proposta = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ID.Equals(ID) && c.EmpresaID.Equals(empresaID));
            return proposta.MapTo<PropostaLCDto>();
        }

        public async Task<PropostaLCPatrimoniosDto> GetPatrimonio(string documento)
        {
            var returno = new PropostaLCPatrimoniosDto();

            var ultimaGarantia = await _unitOfWork.PropostaLCGarantiaRepository.GetLatest(g => g.Documento == documento);
            var ultimaProposta = await _unitOfWork.PropostaLCRepository.GetLatest(c => c.ContaCliente.Documento == documento);
            var ultimaPropostaGrupo = await _unitOfWork.PropostaLCRepository.GetLatest(c => c.AnaliseGrupo == true && c.ContaCliente.Documento != documento && (c.BensRurais.Any(bt => bt.Documento == documento) || c.BensUrbanos.Any(bu => bu.Documento == documento) || c.MaquinasEquipamentos.Any(me => me.Documento == documento)));

            var minData = DateTime.MinValue;
            var tipo = "";

            if (ultimaPropostaGrupo != null)
            {
                if ((ultimaPropostaGrupo.DataAlteracao ?? ultimaPropostaGrupo.DataCriacao) > minData)
                {
                    tipo = "C";
                    minData = ultimaPropostaGrupo.DataAlteracao ?? ultimaPropostaGrupo.DataCriacao;
                }
            }

            if (ultimaProposta != null)
            {
                if ((ultimaProposta.DataAlteracao ?? ultimaProposta.DataCriacao) > minData)
                {
                    tipo = "B";
                    minData = ultimaProposta.DataAlteracao ?? ultimaProposta.DataCriacao;
                }
            }

            if (ultimaGarantia != null)
            {
                if (ultimaGarantia.DataCriacao > minData)
                {
                    tipo = "A";
                    minData = ultimaGarantia.DataCriacao;
                }
            }

            if (tipo == "A")
            {
                if (ultimaProposta != null)
                {
                    returno.PotencialReceita = ultimaProposta.ReceitaTotal ?? 0;
                }

                returno.PotencialPatrimonial = ultimaGarantia.PotencialPatrimonial ?? 0;
                returno.Nome = ultimaGarantia.Nome;

                var bemrural = await _unitOfWork.PropostaLCBemRuralRepository.GetAllFilterAsync(c => c.GarantiaID == ultimaGarantia.ID && c.PropostaLCID == ultimaGarantia.PropostaLCID);

                var bemrurais = bemrural.MapTo<IEnumerable<PropostaLCBemRuralDto>>();
                bemrurais.ForEach(c => c.Documento = documento);
                returno.BemRural.AddRange(bemrurais);

                var bemurbano = await _unitOfWork.PropostaLCBemUrbanoRepository.GetAllFilterAsync(c => c.GarantiaID == ultimaGarantia.ID && c.PropostaLCID == ultimaGarantia.PropostaLCID);

                var bemurbanos = bemurbano.MapTo<IEnumerable<PropostaLCBemUrbanoDto>>();
                bemurbanos.ForEach(c => c.Documento = documento);
                returno.BemUrbano.AddRange(bemurbanos);

                var maquina = await _unitOfWork.PropostaLCMaquinaEquipamentoRepository.GetAllFilterAsync(c => c.GarantiaID == ultimaGarantia.ID && c.PropostaLCID == ultimaGarantia.PropostaLCID);

                var maquinas = maquina.MapTo<IEnumerable<PropostaLCMaquinaEquipamentoDto>>();
                maquinas.ForEach(c => c.Documento = documento);
                returno.MaquinasEquipamentos.AddRange(maquinas);

                var precosPorRegiao = await _unitOfWork.PropostaLCPrecoPorRegiaoRepository.GetAllFilterAsync(p => p.Documento == documento && p.PropostaLCID == ultimaGarantia.PropostaLCID);
                returno.PrecosPorRegiao.AddRange(precosPorRegiao.MapTo<IEnumerable<PropostaLCPrecoPorRegiaoDto>>());

                return returno;
            }
            else if (tipo == "B")
            {
                returno.PotencialPatrimonial = ultimaProposta.PotencialPatrimonial ?? 0;
                returno.PotencialReceita = ultimaProposta.ReceitaTotal ?? 0;

                var cliente = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(ultimaProposta.ContaClienteID));
                returno.Nome = cliente.Nome;

                var bemrural = await _unitOfWork.PropostaLCBemRuralRepository.GetAllFilterAsync(c => c.PropostaLCID.Equals(ultimaProposta.ID) && c.Documento == documento);

                var bemrurais = bemrural.MapTo<IEnumerable<PropostaLCBemRuralDto>>();
                returno.BemRural.AddRange(bemrurais);

                var bemurbano = await _unitOfWork.PropostaLCBemUrbanoRepository.GetAllFilterAsync(c => c.PropostaLCID.Equals(ultimaProposta.ID) && c.Documento == documento);

                var bemurbanos = bemurbano.MapTo<IEnumerable<PropostaLCBemUrbanoDto>>();
                returno.BemUrbano.AddRange(bemurbanos);

                var maquina = await _unitOfWork.PropostaLCMaquinaEquipamentoRepository.GetAllFilterAsync(c => c.PropostaLCID.Equals(ultimaProposta.ID) && c.Documento == documento);

                var maquinas = maquina.MapTo<IEnumerable<PropostaLCMaquinaEquipamentoDto>>();
                returno.MaquinasEquipamentos.AddRange(maquinas);

                var precosPorRegiao = await _unitOfWork.PropostaLCPrecoPorRegiaoRepository.GetAllFilterAsync(p => p.PropostaLCID.Equals(ultimaProposta.ID) && p.Documento == documento);
                returno.PrecosPorRegiao.AddRange(precosPorRegiao.MapTo<IEnumerable<PropostaLCPrecoPorRegiaoDto>>());

                return returno;
            }
            else if (tipo == "C")
            {
                var integrante = await _unitOfWork.PropostaLcGrupoEconomico.GetAsync(plg => plg.PropostaLCID == ultimaPropostaGrupo.ID && plg.Documento == documento);
                if (integrante != null)
                {
                    returno.PotencialPatrimonial = integrante.PotencialPatrimonial ?? 0;
                    returno.PotencialReceita = integrante.PotencialReceita ?? 0;
                }

                var cliente = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.Documento == documento);
                returno.Nome = cliente?.Nome;

                var bemrural = await _unitOfWork.PropostaLCBemRuralRepository.GetAllFilterAsync(c => c.PropostaLCID.Equals(ultimaPropostaGrupo.ID) && c.Documento == documento);

                var bemrurais = bemrural.MapTo<IEnumerable<PropostaLCBemRuralDto>>();
                returno.BemRural.AddRange(bemrurais);

                var bemurbano = await _unitOfWork.PropostaLCBemUrbanoRepository.GetAllFilterAsync(c => c.PropostaLCID.Equals(ultimaPropostaGrupo.ID) && c.Documento == documento);

                var bemurbanos = bemurbano.MapTo<IEnumerable<PropostaLCBemUrbanoDto>>();
                returno.BemUrbano.AddRange(bemurbanos);

                var maquina = await _unitOfWork.PropostaLCMaquinaEquipamentoRepository.GetAllFilterAsync(c => c.PropostaLCID.Equals(ultimaPropostaGrupo.ID) && c.Documento == documento);

                var maquinas = maquina.MapTo<IEnumerable<PropostaLCMaquinaEquipamentoDto>>();
                returno.MaquinasEquipamentos.AddRange(maquinas);

                var precosPorRegiao = await _unitOfWork.PropostaLCPrecoPorRegiaoRepository.GetAllFilterAsync(p => p.PropostaLCID.Equals(ultimaPropostaGrupo.ID) && p.Documento == documento);
                returno.PrecosPorRegiao.AddRange(precosPorRegiao.MapTo<IEnumerable<PropostaLCPrecoPorRegiaoDto>>());

                return returno;
            }

            var conta = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.Documento.Contains(documento));
            returno.Nome = conta?.Nome;

            return returno;
        }

        public async Task<PropostaLcTodasReceitasDto> GetTodasReceitas(string documento)
        {
            var retorno = new PropostaLcTodasReceitasDto();

            var cultura = await _unitOfWork.PropostaLCCulturaRepository.GetAllFilterAsync(c => c.Documento.Equals(documento));
            var culturaDto = cultura.MapTo<IEnumerable<PropostaLCCulturaDto>>();
            if (culturaDto != null) retorno.Cultura.AddRange(culturaDto);


            var percuaria = await _unitOfWork.PropostaLCPecuariaRepository.GetAllFilterAsync(c => c.Documento.Equals(documento));
            var percuariaDto = percuaria.MapTo<IEnumerable<PropostaLCPecuariaDto>>();
            percuariaDto.ForEach(c => c.Documento = documento);
            if (percuariaDto != null) retorno.Pecuaria.AddRange(percuariaDto);


            var outrasReceitas = await _unitOfWork.PropostaLCOutraReceitaRepository.GetAllFilterAsync(c => c.Documento.Equals(documento));
            var outrasReceitasDto = outrasReceitas.MapTo<IEnumerable<PropostaLCOutraReceitaDto>>();
            outrasReceitasDto.ForEach(c => c.Documento = documento);
            if (outrasReceitasDto != null) retorno.OutraReceita.AddRange(outrasReceitasDto);


            var tipoEndividamento = await _unitOfWork.PropostaLCTipoEndividamentoRepository.GetAllFilterAsync(c => c.Documento.Equals(documento));
            var tipoEndividamentoDto = tipoEndividamento.MapTo<IEnumerable<PropostaLCTipoEndividamentoDto>>();
            tipoEndividamentoDto.ForEach(c => c.Documento = documento);
            if (tipoEndividamentoDto != null) retorno.TipoEndividamento.AddRange(tipoEndividamentoDto);

            return retorno;
        }

        public async Task<IEnumerable<PropostaLCDto>> GetPropostalList(Guid id)
        {
            var proposta = await _unitOfWork.PropostaLCRepository.GetAllFilterOrderByAsyncList(c => c.ContaClienteID == id, c => c.DataCriacao);
            return proposta.MapTo<IEnumerable<PropostaLCDto>>();
        }

        public async Task<IEnumerable<BuscaGrupoEconomicoPropostaLCDto>> BuscaGrupoEconomicoPropostaLc(Guid grupoEconomicoId)
        {
            var propostaLc = await _unitOfWork.PropostaLCRepository.BuscaGrupoEconomicoPropostaLc(grupoEconomicoId);
            return propostaLc.MapTo<IEnumerable<BuscaGrupoEconomicoPropostaLCDto>>();
        }

        #region Helpers

        private async Task Atualizar(PropostaLCDto propostaLC)
        {
            var propUpd = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ID.Equals(propostaLC.ID));

            propUpd.DataAlteracao = DateTime.Now;
            propUpd.UsuarioIDAlteracao = propostaLC.UsuarioIDAlteracao;

            // Scalar Properties
            propUpd.TipoClienteID = propostaLC.TipoClienteID;
            propUpd.ExperienciaID = propostaLC.ExperienciaID;
            propUpd.EstadoCivil = propostaLC.EstadoCivil;
            propUpd.CPFConjugue = propostaLC.CPFConjugue;
            propUpd.NomeConjugue = propostaLC.NomeConjugue;
            propUpd.RegimeCasamento = propostaLC.RegimeCasamento;
            propUpd.PossuiGerenteTecnico = propostaLC.PossuiGerenteTecnico;
            propUpd.PossuiMaquinarioProprio = propostaLC.PossuiMaquinarioProprio;
            propUpd.PossuiArmazem = propostaLC.PossuiArmazem;
            propUpd.ToneladasArmazem = propostaLC.ToneladasArmazem;
            propUpd.PossuiAreaIrrigada = propostaLC.PossuiAreaIrrigada;
            propUpd.AreaIrrigadaID = propostaLC.AreaIrrigadaID;
            propUpd.PossuiContratoProximaSafra = propostaLC.PossuiContratoProximaSafra;
            propUpd.Trading = propostaLC.Trading;
            propUpd.QtdeSacas = propostaLC.QtdeSacas;
            propUpd.PrecoSaca = propostaLC.PrecoSaca;
            propUpd.ClienteGarantia = propostaLC.ClienteGarantia;
            propUpd.PossuiCriacaoDeAnimais = propostaLC.PossuiCriacaoDeAnimais;
            propUpd.PossuiOutrasReceitas = propostaLC.PossuiOutrasReceitas;
            propUpd.PrincipaisFornecedores = propostaLC.PrincipaisFornecedores;
            propUpd.PrincipaisClientes = propostaLC.PrincipaisClientes;
            propUpd.ComentarioMercado = propostaLC.ComentarioMercado;
            propUpd.NecessidadeAnualFertilizantes = propostaLC.NecessidadeAnualFertilizantes;
            propUpd.PrecoMedioFt = propostaLC.PrecoMedioFt;
            propUpd.NecessidadeAnualFoliar = propostaLC.NecessidadeAnualFoliar;
            propUpd.PrecoMedioLt = propostaLC.PrecoMedioLt;
            propUpd.NumeroComprasAno = propostaLC.NumeroComprasAno;
            propUpd.ResultadoUltimaSafra = propostaLC.ResultadoUltimaSafra;
            propUpd.NumeroClienteCooperados = propostaLC.NumeroClienteCooperados;
            propUpd.PrazoMedioVendas = propostaLC.PrazoMedioVendas;
            propUpd.IdadeMediaCanavialID = propostaLC.IdadeMediaCanavialID;
            propUpd.TotalProducaoAlcool = propostaLC.TotalProducaoAlcool;
            propUpd.TotalProducaoAcucar = propostaLC.TotalProducaoAcucar;
            propUpd.VolumeMoagemPropria = propostaLC.VolumeMoagemPropria;
            propUpd.CustoMedioProducao = propostaLC.CustoMedioProducao;
            propUpd.CapacidadeMoagem = propostaLC.CapacidadeMoagem;
            propUpd.TotalMWAno = propostaLC.TotalMWAno;
            propUpd.CustoCarregamentoTransporte = propostaLC.CustoCarregamentoTransporte;
            propUpd.RestricaoSERASA = propostaLC.RestricaoSERASA;
            propUpd.RestricaoTJ = propostaLC.RestricaoTJ;
            propUpd.RestricaoIBAMA = propostaLC.RestricaoIBAMA;
            propUpd.RestricaoTrabalhoEscravo = propostaLC.RestricaoTrabalhoEscravo;
            propUpd.LCProposto = propostaLC.LCProposto;
            propUpd.SharePretentido = propostaLC.SharePretentido;
            propUpd.FonteRecursosCarteira = propostaLC.FonteRecursosCarteira;
            propUpd.ParecerRepresentante = propostaLC.ParecerRepresentante;
            propUpd.ParecerCTC = propostaLC.ParecerCTC;
            propUpd.ComentarioPatrimonio = propostaLC.ComentarioPatrimonio;
            propUpd.PropostaLCDemonstrativoID = propostaLC.PropostaLCDemonstrativoID;
            propUpd.ResponsavelID = propostaLC.ResponsavelID;

            propUpd.ValorTotalBensRurais = propostaLC.ValorTotalBensRurais;
            propUpd.ValorTotalBensUrbanos = propostaLC.ValorTotalBensUrbanos;
            propUpd.ValorTotalMaquinasEquipamentos = propostaLC.ValorTotalMaquinasEquipamentos;
            propUpd.FixarLimiteCredito = propostaLC.FixarLimiteCredito;
            propUpd.ReceitaTotal = propostaLC.ReceitaTotal;
            propUpd.BalancoAuditado = propostaLC.BalancoAuditado;
            propUpd.EmpresaAuditora = propostaLC.EmpresaAuditora;
            propUpd.Ressalvas = propostaLC.Ressalvas;
            propUpd.DemonstrativoFinanceiroID = propostaLC.DemonstrativoFinanceiroID;
            propUpd.PrazoEmDias = propostaLC.PrazoEmDias;

            propUpd.AnaliseGrupo = propostaLC.AnaliseGrupo;
            propUpd.GrupoEconomicoID = propostaLC.GrupoEconomicoID;
            propUpd.AcompanharProposta = propostaLC.AcompanharProposta;
            propUpd.Documento = propostaLC.Documento;
            propUpd.Rating = propostaLC.Rating;
            propUpd.PotencialCredito = propostaLC.PotencialCredito;
            propUpd.PotencialPatrimonial = propostaLC.PotencialPatrimonial;

            propUpd.ParecerAnalista = propostaLC.ParecerAnalista;

            // Collection Properties
            #region Parcerias Agricolas

            var idsParcerias = propostaLC.ParceriasAgricolas.Where(pa => pa.ID != Guid.Empty).Select(pa => pa.ID).ToList();
            var removeParcerias = propUpd.ParceriasAgricolas.Where(pa => !idsParcerias.Contains(pa.ID)).ToList();
            PropostaLCParceriaAgricola paAtual = null;

            foreach (var rpa in removeParcerias)
            {
                _unitOfWork.PropostaLCParceriaAgricolaRepository.Delete(rpa);
            }

            foreach (var pa in propostaLC.ParceriasAgricolas)
            {
                paAtual = propUpd.ParceriasAgricolas.FirstOrDefault(p => p.ID.Equals(pa.ID));

                if (paAtual == null)
                {
                    pa.PropostaLCID = propostaLC.ID;
                    pa.ID = Guid.NewGuid();
                    propUpd.ParceriasAgricolas.Add(pa.MapTo<PropostaLCParceriaAgricola>());
                }
                else
                {
                    paAtual.InscricaoEstadual = pa.InscricaoEstadual;
                    paAtual.Nome = pa.Nome;
                    paAtual.Documento = pa.Documento;
                    _unitOfWork.PropostaLCParceriaAgricolaRepository.Update(paAtual);
                }
            }

            #endregion

            #region Garantias

            var idsGarantias = propostaLC.Garantias.Where(g => g.ID != Guid.Empty).Select(g => g.ID).ToList();
            var removeGarantias = propUpd.Garantias.Where(g => !idsGarantias.Contains(g.ID)).ToList();
            PropostaLCGarantia gAtual = null;

            foreach (var rg in removeGarantias)
            {
                _unitOfWork.PropostaLCGarantiaRepository.Delete(rg);
            }

            foreach (var g in propostaLC.Garantias)
            {
                gAtual = propUpd.Garantias.FirstOrDefault(gx => gx.ID.Equals(g.ID));

                if (gAtual == null)
                {

                    g.PropostaLCID = propostaLC.ID;
                    g.ID = Guid.NewGuid();
                    g.DataCriacao = DateTime.Now;
                    propUpd.Garantias.Add(g.MapTo<PropostaLCGarantia>());

                }
                else
                {

                    gAtual.DataCriacao = DateTime.Now;
                    gAtual.TipoGarantiaID = g.TipoGarantiaID;
                    gAtual.Descricao = g.Descricao;
                    gAtual.GarantiaRecebida = g.GarantiaRecebida;
                    gAtual.PropostaLCDemonstrativoID = g.PropostaLCDemonstrativoID;
                    gAtual.Documento = g.Documento;
                    gAtual.Nome = g.Nome;
                    gAtual.Comentario = g.Comentario;
                    gAtual.PotencialPatrimonial = g.PotencialPatrimonial;

                    _unitOfWork.PropostaLCGarantiaRepository.Update(gAtual);
                }
            }

            #endregion

            #region Culturas

            var idsCulturas = propostaLC.Culturas.Where(c => c.ID != Guid.Empty).Select(c => c.ID).ToList();
            var removeCulturas = propUpd.Culturas.Where(c => !idsCulturas.Contains(c.ID)).ToList();
            PropostaLCCultura cAtual = null;

            foreach (var rc in removeCulturas)
            {
                _unitOfWork.PropostaLCCulturaRepository.Delete(rc);
            }

            foreach (var ca in propostaLC.Culturas)
            {
                cAtual = propUpd.Culturas.FirstOrDefault(c => c.ID.Equals(ca.ID));

                if (cAtual == null)
                {
                    ca.Cidade = null;
                    ca.PropostaLCID = propostaLC.ID;
                    ca.ID = Guid.NewGuid();
                    propUpd.Culturas.Add(ca.MapTo<PropostaLCCultura>());
                }
                else
                {
                    cAtual.CulturaID = ca.CulturaID;
                    cAtual.CidadeID = ca.CidadeID;
                    cAtual.Area = ca.Area;
                    cAtual.Arrendamento = ca.Arrendamento;
                    cAtual.ProdutividadeMedia = ca.ProdutividadeMedia;
                    cAtual.Preco = ca.Preco;
                    cAtual.CustoHa = ca.CustoHa;
                    cAtual.ConsumoFertilizante = ca.ConsumoFertilizante;
                    cAtual.PrecoMedioFertilizante = ca.PrecoMedioFertilizante;
                    cAtual.MesPlantio = ca.MesPlantio.MapTo<Meses>();
                    cAtual.MesColheita = ca.MesColheita.MapTo<Meses>();
                    cAtual.Quebra = ca.Quebra;
                    cAtual.CustoArrendamentoSacaHa = ca.CustoArrendamentoSacaHa;
                    cAtual.Documento = ca.Documento;
                    cAtual.MediaFertilizantePadrao = ca.MediaFertilizantePadrao;
                    cAtual.PorcentagemFertilizanteCustoPadrao = ca.PorcentagemFertilizanteCustoPadrao;
                    cAtual.PrecoPadrao = ca.PrecoPadrao;
                    cAtual.ProdutividadeMediaPadrao = ca.ProdutividadeMediaPadrao;
                    cAtual.CustoPadrao = ca.CustoPadrao;
                    _unitOfWork.PropostaLCCulturaRepository.Update(cAtual);
                }
            }

            #endregion

            #region Necessidade de Produto

            var idsNecessidades = propostaLC.NecessidadeProduto.Where(c => c.ID != Guid.Empty).Select(c => c.ID).ToList();
            var removeNecessidades = propUpd.NecessidadeProduto.Where(c => !idsNecessidades.Contains(c.ID)).ToList();
            PropostaLCNecessidadeProduto nAtual = null;

            foreach (var rn in removeNecessidades)
            {
                _unitOfWork.PropostaLCNecessidadeProdutoRepository.Delete(rn);
            }

            foreach (var na in propostaLC.NecessidadeProduto)
            {
                nAtual = propUpd.NecessidadeProduto.FirstOrDefault(c => c.ID.Equals(na.ID));

                if (nAtual == null)
                {
                    var produto = na.MapTo<PropostaLCNecessidadeProduto>();
                    produto.ProdutoServicoID = na.ProdutoServicoID;
                    produto.ProdutoServico = null;
                    produto.PropostaLCID = propostaLC.ID;
                    produto.ID = Guid.NewGuid();
                    propUpd.NecessidadeProduto.Add(produto);
                }
                else
                {

                    nAtual.ProdutoServicoID = na.ProdutoServicoID;
                    nAtual.Quantidade = na.Quantidade;
                    _unitOfWork.PropostaLCNecessidadeProdutoRepository.Update(nAtual);
                }
            }

            #endregion

            #region Mercados

            var idsMercados = propostaLC.PrincipaisMercados.Where(c => c.ID != Guid.Empty).Select(c => c.ID).ToList();
            var removeMercados = propUpd.PrincipaisMercados.Where(c => !idsMercados.Contains(c.ID)).ToList();
            PropostaLCMercado mAtual = null;

            foreach (var rm in removeMercados)
            {
                _unitOfWork.PropostaLCMercadoRepository.Delete(rm);
            }

            foreach (var ma in propostaLC.PrincipaisMercados)
            {
                mAtual = propUpd.PrincipaisMercados.FirstOrDefault(c => c.ID.Equals(ma.ID));

                if (mAtual == null)
                {
                    ma.PropostaLCID = propostaLC.ID;
                    ma.ID = Guid.NewGuid();
                    propUpd.PrincipaisMercados.Add(ma.MapTo<PropostaLCMercado>());
                }
                else
                {
                    mAtual.CulturaID = ma.CulturaID;
                    _unitOfWork.PropostaLCMercadoRepository.Update(mAtual);
                }
            }

            #endregion

            #region Pecuaria

            var idsPecuaria = propostaLC.CriacoesAnimais.Where(c => c.ID != Guid.Empty).Select(c => c.ID).ToList();
            var removePecuaria = propUpd.CriacoesAnimais.Where(c => !idsPecuaria.Contains(c.ID)).ToList();
            PropostaLCPecuaria peAtual = null;

            foreach (var pem in removePecuaria)
            {
                _unitOfWork.PropostaLCPecuariaRepository.Delete(pem);
            }

            foreach (var paa in propostaLC.CriacoesAnimais)
            {
                peAtual = propUpd.CriacoesAnimais.FirstOrDefault(c => c.ID.Equals(paa.ID));

                if (peAtual == null)
                {
                    paa.PropostaLCID = propostaLC.ID;
                    paa.ID = Guid.NewGuid();
                    propUpd.CriacoesAnimais.Add(paa.MapTo<PropostaLCPecuaria>());
                }
                else
                {
                    peAtual.TipoPecuariaID = paa.TipoPecuariaID;
                    peAtual.AnoPecuaria = paa.AnoPecuaria;
                    peAtual.Quantidade = paa.Quantidade;
                    peAtual.Preco = paa.Preco;
                    peAtual.Despesa = paa.Despesa;
                    peAtual.Documento = paa.Documento;
                    _unitOfWork.PropostaLCPecuariaRepository.Update(peAtual);
                }
            }

            #endregion

            #region Outras Receitas

            var idsOutraReceita = propostaLC.OutrasReceitas.Where(c => c.ID != Guid.Empty).Select(c => c.ID).ToList();
            var removeOutraReceita = propUpd.OutrasReceitas.Where(c => !idsOutraReceita.Contains(c.ID)).ToList();
            PropostaLCOutraReceita oaAtual = null;

            foreach (var oam in removeOutraReceita)
            {
                _unitOfWork.PropostaLCOutraReceitaRepository.Delete(oam);
            }

            foreach (var oaa in propostaLC.OutrasReceitas)
            {
                oaAtual = propUpd.OutrasReceitas.FirstOrDefault(c => c.ID.Equals(oaa.ID));

                if (oaAtual == null)
                {
                    oaa.PropostaLCID = propostaLC.ID;
                    oaa.ID = Guid.NewGuid();
                    propUpd.OutrasReceitas.Add(oaa.MapTo<PropostaLCOutraReceita>());
                }
                else
                {
                    oaAtual.ReceitaID = oaa.ReceitaID;
                    oaAtual.AnoOutrasReceitas = oaa.AnoOutrasReceitas;
                    oaAtual.ReceitaPrevista = oaa.ReceitaPrevista;
                    oaAtual.Documento = oaa.Documento;
                    _unitOfWork.PropostaLCOutraReceitaRepository.Update(oaAtual);
                }
            }

            #endregion

            #region Referencias

            var idsReferencias = propostaLC.Referencias.Where(c => c.ID != Guid.Empty).Select(c => c.ID).ToList();
            var removeReferencia = propUpd.Referencias.Where(c => !idsReferencias.Contains(c.ID)).ToList();
            PropostaLCReferencia reAtual = null;

            foreach (var rrm in removeReferencia)
            {
                _unitOfWork.PropostaLCReferenciaRepository.Delete(rrm);
            }

            foreach (var raa in propostaLC.Referencias)
            {
                reAtual = propUpd.Referencias.FirstOrDefault(c => c.ID.Equals(raa.ID));

                if (reAtual == null)
                {
                    raa.PropostaLCID = propostaLC.ID;
                    raa.ID = Guid.NewGuid();
                    propUpd.Referencias.Add(raa.MapTo<PropostaLCReferencia>());
                }
                else
                {
                    reAtual.TipoReferencia = raa.TipoReferencia;
                    reAtual.TipoEmpresaID = raa.TipoEmpresaID;
                    reAtual.NomeEmpresa = raa.NomeEmpresa;
                    reAtual.NomeBanco = raa.NomeBanco;
                    reAtual.Municipio = raa.Municipio;
                    reAtual.Telefone = raa.Telefone;
                    reAtual.NomeContato = raa.NomeContato;
                    reAtual.Desde = raa.Desde;
                    reAtual.LCAtual = raa.LCAtual;
                    reAtual.Vigencia = raa.Vigencia;
                    reAtual.Garantias = raa.Garantias;
                    reAtual.Comentarios = raa.Comentarios;
                    _unitOfWork.PropostaLCReferenciaRepository.Update(reAtual);
                }
            }

            #endregion

            #region Bens Rurais + LOG

            var idsBensRurais = propostaLC.BensRurais.Where(c => c.ID != Guid.Empty).Select(c => c.ID).ToList();
            var removeBensRurais = propUpd.BensRurais.Where(c => !idsBensRurais.Contains(c.ID)).ToList();
            PropostaLCBemRural brAtual = null;

            foreach (var brm in removeBensRurais)
            {
                AddLogPatrimony(propUpd.ID, "Removendo", "bem rural", brm.ID, brm.IR);

                _unitOfWork.PropostaLCBemRuralRepository.Delete(brm);
            }

            foreach (var bra in propostaLC.BensRurais)
            {
                if (!string.IsNullOrEmpty(bra.Documento) && propUpd.Garantias != null && propUpd.Garantias.Any())
                {
                    var garantia = propUpd.Garantias.FirstOrDefault(c => c.Documento == bra.Documento);

                    if (garantia != null)
                        bra.GarantiaID = garantia.ID;
                }

                brAtual = propUpd.BensRurais.FirstOrDefault(c => c.ID.Equals(bra.ID));

                if (brAtual == null)
                {
                    bra.PropostaLCID = propostaLC.ID;
                    bra.ID = Guid.NewGuid();
                    propUpd.BensRurais.Add(bra.MapTo<PropostaLCBemRural>());

                    AddLogPatrimony(propUpd.ID, "Adicionando", "bem rural", bra.ID, bra.IR);
                }
                else
                {
                    AddLogPatrimony(propUpd.ID, "Atualizando", "bem rural", bra.ID, bra.IR);

                    brAtual.GarantiaID = bra.GarantiaID;
                    brAtual.IR = bra.IR;
                    brAtual.CidadeID = bra.CidadeID;
                    brAtual.AreaTotalHa = bra.AreaTotalHa;
                    brAtual.Benfeitorias = bra.Benfeitorias;
                    brAtual.Onus = bra.Onus;
                    _unitOfWork.PropostaLCBemRuralRepository.Update(brAtual);
                }
            }

            #endregion

            #region Bens Urbanos + LOG

            var idsBensUrbanos = propostaLC.BensUrbanos.Where(c => c.ID != Guid.Empty).Select(c => c.ID).ToList();
            var removeBensUrbanos = propUpd.BensUrbanos.Where(c => !idsBensUrbanos.Contains(c.ID)).ToList();
            PropostaLCBemUrbano buAtual = null;

            foreach (var bum in removeBensUrbanos)
            {
                AddLogPatrimony(propUpd.ID, "Removendo", "bem urbano", bum.ID, bum.IR);

                _unitOfWork.PropostaLCBemUrbanoRepository.Delete(bum);
            }

            foreach (var bua in propostaLC.BensUrbanos)
            {
                if (!string.IsNullOrEmpty(bua.Documento) && propUpd.Garantias != null && propUpd.Garantias.Any())
                {
                    var garantia = propUpd.Garantias.FirstOrDefault(c => c.Documento == bua.Documento);

                    if (garantia != null)
                        bua.GarantiaID = garantia.ID;
                }

                buAtual = propUpd.BensUrbanos.FirstOrDefault(c => c.ID.Equals(bua.ID));

                if (buAtual == null)
                {
                    bua.PropostaLCID = propostaLC.ID;
                    bua.ID = Guid.NewGuid();
                    propUpd.BensUrbanos.Add(bua.MapTo<PropostaLCBemUrbano>());

                    AddLogPatrimony(propUpd.ID, "Adicionando", "bem urbano", bua.ID, bua.IR);
                }
                else
                {
                    AddLogPatrimony(propUpd.ID, "Atualizando", "bem urbano", bua.ID, bua.IR);

                    buAtual.GarantiaID = bua.GarantiaID;
                    buAtual.IR = bua.IR;
                    buAtual.Descricao = bua.Descricao;
                    buAtual.AreaTotal = bua.AreaTotal;
                    buAtual.ValorComBenfeitorias = bua.ValorComBenfeitorias;
                    buAtual.Onus = bua.Onus;
                    buAtual.ValorAvaliado = bua.ValorAvaliado;
                    _unitOfWork.PropostaLCBemUrbanoRepository.Update(buAtual);
                }
            }

            #endregion

            #region Maquinas e Equipamentos + LOG

            var idsMaquinasEquipamentos = propostaLC.MaquinasEquipamentos.Where(c => c.ID != Guid.Empty).Select(c => c.ID).ToList();
            var removeMaquinasEquipamentos = propUpd.MaquinasEquipamentos.Where(c => !idsMaquinasEquipamentos.Contains(c.ID)).ToList();
            PropostaLCMaquinaEquipamento maAtual = null;

            foreach (var mem in removeMaquinasEquipamentos)
            {
                AddLogPatrimony(propUpd.ID, "Removendo", "máquina ou equipamento", mem.ID, mem.Descricao);

                _unitOfWork.PropostaLCMaquinaEquipamentoRepository.Delete(mem);
            }

            foreach (var mea in propostaLC.MaquinasEquipamentos)
            {
                if (!string.IsNullOrEmpty(mea.Documento) && propUpd.Garantias != null && propUpd.Garantias.Any())
                {
                    var garantia = propUpd.Garantias.FirstOrDefault(c => c.Documento == mea.Documento);

                    if (garantia != null)
                        mea.GarantiaID = garantia.ID;
                }

                maAtual = propUpd.MaquinasEquipamentos.FirstOrDefault(c => c.ID.Equals(mea.ID));

                if (maAtual == null)
                {
                    mea.PropostaLCID = propostaLC.ID;
                    mea.ID = Guid.NewGuid();
                    propUpd.MaquinasEquipamentos.Add(mea.MapTo<PropostaLCMaquinaEquipamento>());

                    AddLogPatrimony(propUpd.ID, "Adicionando", "máquina ou equipamento", mea.ID, mea.Descricao);
                }
                else
                {
                    AddLogPatrimony(propUpd.ID, "Atualizando", "máquina ou equipamento", mea.ID, mea.Descricao);

                    maAtual.GarantiaID = mea.GarantiaID;
                    maAtual.Descricao = mea.Descricao;
                    maAtual.Ano = mea.Ano;
                    maAtual.Valor = mea.Valor;
                    _unitOfWork.PropostaLCMaquinaEquipamentoRepository.Update(maAtual);
                }
            }

            #endregion

            #region Precos por Regiao + LOG

            var idsPrecosPorRegiao = propostaLC.PrecosPorRegiao.Where(c => c.ID != Guid.Empty).Select(c => c.ID).ToList();
            var removePrecosPorRegiao = propUpd.PrecosPorRegiao.Where(c => !idsPrecosPorRegiao.Contains(c.ID)).ToList();
            PropostaLCPrecoPorRegiao pprAtual = null;

            foreach (var prm in removePrecosPorRegiao)
            {
                AddLogPatrimony(propUpd.ID, "Removendo", "preço por região", prm.ID, prm.Cidade.Nome);

                _unitOfWork.PropostaLCPrecoPorRegiaoRepository.Delete(prm);
            }

            foreach (var ppra in propostaLC.PrecosPorRegiao)
            {
                pprAtual = propUpd.PrecosPorRegiao.FirstOrDefault(c => c.ID.Equals(ppra.ID));

                if (pprAtual == null)
                {
                    ppra.PropostaLCID = propostaLC.ID;
                    ppra.ID = Guid.NewGuid();
                    propUpd.PrecosPorRegiao.Add(ppra.MapTo<PropostaLCPrecoPorRegiao>());

                    AddLogPatrimony(propUpd.ID, "Adicionando", "preço por região", ppra.ID, ppra.Cidade.Nome);
                }
                else
                {
                    AddLogPatrimony(propUpd.ID, "Atualizando", "preço por região", ppra.ID, ppra.Cidade.Nome);

                    pprAtual.CidadeID = ppra.CidadeID;
                    pprAtual.ValorHaCultivavel = ppra.ValorHaCultivavel;
                    pprAtual.ValorHaNaoCultivavel = ppra.ValorHaNaoCultivavel;
                    pprAtual.ModuloRural = ppra.ModuloRural;
                    pprAtual.Documento = ppra.Documento;
                    pprAtual.ValorHaCultivavelParametro = ppra.ValorHaCultivavelParametro;
                    pprAtual.ValorHaNaoCultivavelParametro = ppra.ValorHaNaoCultivavelParametro;
                    pprAtual.ModuloRuralParametro = ppra.ModuloRuralParametro;
                    _unitOfWork.PropostaLCPrecoPorRegiaoRepository.Update(pprAtual);
                }
            }

            #endregion

            #region Tipos Endividamento

            var idsTiposEndividamento = propostaLC.TiposEndividamento.Where(c => c.ID != Guid.Empty).Select(c => c.ID).ToList();
            var removeTiposEndividamento = propUpd.TiposEndividamento.Where(c => !idsTiposEndividamento.Contains(c.ID)).ToList();
            PropostaLCTipoEndividamento teAtual = null;

            foreach (var tem in removeTiposEndividamento)
            {
                _unitOfWork.PropostaLCTipoEndividamentoRepository.Delete(tem);
            }

            foreach (var tea in propostaLC.TiposEndividamento)
            {
                teAtual = propUpd.TiposEndividamento.FirstOrDefault(c => c.ID.Equals(tea.ID));

                if (teAtual == null)
                {
                    tea.PropostaLCID = propostaLC.ID;
                    tea.ID = Guid.NewGuid();
                    propUpd.TiposEndividamento.Add(tea.MapTo<PropostaLCTipoEndividamento>());
                }
                else
                {
                    teAtual.TipoEndividamentoID = tea.TipoEndividamentoID;
                    teAtual.CurtoPrazo = tea.CurtoPrazo;
                    teAtual.LongoPrazo = tea.LongoPrazo;
                    teAtual.Documento = tea.Documento;
                    _unitOfWork.PropostaLCTipoEndividamentoRepository.Update(teAtual);
                }

            }

            #endregion
        }

        private void AddLogPatrimony(Guid IDTransacao, string tipoOperacao, string tipoBem, Guid bemUrbanoOuRuralOuEquipamentoOuPrecoRegiao, string IRouDescricao)
        {
            try
            {
                var logService = new AppServiceLog(_unitOfWork);

                var log = new LogDto
                {
                    ID = Guid.NewGuid(),
                    IDTransacao = IDTransacao,
                    Descricao = string.Format("{0} {1}. ID: {2}, IR ou Descrição: {3}", tipoOperacao, tipoBem, bemUrbanoOuRuralOuEquipamentoOuPrecoRegiao, IRouDescricao),
                    LogLevelID = 10,
                    Usuario = "Sistema",
                    DataCriacao = DateTime.Now,
                    UsuarioID = Guid.Parse("00000000-0000-0000-0000-000000000001")
                };

                logService.Create(log);
            }
            catch
            {
                // do not throw anything...
            }
        }

        private async Task AddProcessamentoCarteira(ContaClienteFinanceiroDto clienteFinanceiroDto)
        {
            var contaCliente = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID == clienteFinanceiroDto.ContaClienteID);
            var carteiraDto = new ProcessamentoCarteiraDto();
            carteiraDto.ID = Guid.NewGuid();
            carteiraDto.Cliente = contaCliente.CodigoPrincipal;
            carteiraDto.DataHora = DateTime.Now;
            carteiraDto.Status = 2;
            carteiraDto.Motivo = "LC - Fixou Limite de Crédito para o Cliente: " + contaCliente.Nome;
            carteiraDto.Detalhes = "LC - O Limite de Crédito fixado foi no valor de: " + clienteFinanceiroDto.LC;
            carteiraDto.EmpresaID = clienteFinanceiroDto.EmpresasID;
            _unitOfWork.ProcessamentoCarteiraRepository.Insert(carteiraDto.MapTo<ProcessamentoCarteira>());
        }

        private async Task RetornaLimiteFinanceiro(PropostaLCDto proposta)
        {
            // Estes campos são para recuperar o LC e Vigencia caso a proposta seja encerrada por algum motivo.
            var financeiroContaCliente = await _unitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.ContaClienteID.Equals(proposta.ContaClienteID) && c.EmpresasID.Equals(proposta.EmpresaID));

            if (financeiroContaCliente != null)
            {
                financeiroContaCliente.LC = proposta.LCCliente;
                financeiroContaCliente.Vigencia = proposta.VigenciaInicialCliente;
                financeiroContaCliente.VigenciaFim = proposta.VigenciaFinalCliente;
                financeiroContaCliente.Rating = proposta.RatingCliente;

                _unitOfWork.ContaClienteFinanceiroRepository.Update(financeiroContaCliente);
            }
        }

        private async Task<Guid> SendSerasa(PropostaLCDto propostaLc, string EmpresaID, string urlSerasa, string usuarioSerasa, string senhaSerasa)
        {
            var serasa = new AppServiceSerasa(_unitOfWork);
            var exitserasa = await serasa.ExistSerasa(propostaLc.ContaClienteID, EmpresaID);

            if (exitserasa != null)
                return exitserasa.ID;
            else
            {
                var solicitante = new SolicitanteSerasaDto()
                {
                    UsuarioIDCriacao = propostaLc.UsuarioIDAlteracao ?? propostaLc.UsuarioIDCriacao,
                    ContaClienteID = propostaLc.ContaClienteID
                };

                await serasa.ConsultarSerasa(solicitante, EmpresaID, urlSerasa, usuarioSerasa, senhaSerasa);

                return solicitante.ID;
            }
        }

        private async Task SendEmail(Guid PropostaID, Guid Usuario, string Mensagem, string URL)
        {
            try
            {
                var proposta = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ID.Equals(PropostaID));
                var user = await _unitOfWork.UsuarioRepository.GetAsync(c => c.ID.Equals(Usuario));

                var email = new AppServiceEnvioEmail(_unitOfWork);
                await email.SendMailFeedBackPropostas(user.MapTo<UsuarioDto>(), PropostaID, Mensagem, proposta.ContaClienteID, URL);
            }
            catch
            {

            }
        }

        private async Task SaveAcompanhamento(PropostaLCDto propostaLc)
        {
            var user = propostaLc.UsuarioIDCriacao;

            if (propostaLc.UsuarioIDAlteracao.HasValue)
                user = propostaLc.UsuarioIDAlteracao.Value;

            var acompanhamento = await _unitOfWork.AcompanhamentoPropostaLCRepository.GetAsync(c => c.PropostaLCID.Equals(propostaLc.ID) && c.UsuarioID.Equals(user));

            if (acompanhamento != null)
            {
                acompanhamento.Ativo = propostaLc.AcompanharProposta;

                _unitOfWork.AcompanhamentoPropostaLCRepository.Update(acompanhamento);
            }
            else
            {
                acompanhamento = new PropostaLCAcompanhamento
                {
                    PropostaLCID = propostaLc.ID,
                    UsuarioID = user,
                    Ativo = propostaLc.AcompanharProposta,
                    DataCriacao = DateTime.Now
                };

                _unitOfWork.AcompanhamentoPropostaLCRepository.Insert(acompanhamento);
            }
        }

        private void Inserir(PropostaLCDto propostaLC)
        {
            #region Parcerias Agricolas

            foreach (var pa in propostaLC.ParceriasAgricolas)
            {
                pa.PropostaLCID = propostaLC.ID;
                pa.ID = Guid.NewGuid();
            }

            #endregion

            #region Garantias

            foreach (var ga in propostaLC.Garantias)
            {
                ga.PropostaLCID = propostaLC.ID;
                ga.ID = Guid.NewGuid();
                ga.DataCriacao = DateTime.Now;
            }

            #endregion

            #region Culturas

            foreach (var ca in propostaLC.Culturas)
            {
                ca.PropostaLCID = propostaLC.ID;
                ca.ID = Guid.NewGuid();
            }

            #endregion

            #region Necessidade de Produto

            foreach (var na in propostaLC.NecessidadeProduto)
            {
                na.PropostaLCID = propostaLC.ID;
                na.ID = Guid.NewGuid();
            }

            #endregion

            #region Mercados

            foreach (var ma in propostaLC.PrincipaisMercados)
            {
                ma.PropostaLCID = propostaLC.ID;
                ma.ID = Guid.NewGuid();
            }

            #endregion

            #region Pecuaria

            foreach (var pa in propostaLC.CriacoesAnimais)
            {
                pa.PropostaLCID = propostaLC.ID;
                pa.ID = Guid.NewGuid();
            }

            #endregion

            #region Outras Receitas

            foreach (var oa in propostaLC.OutrasReceitas)
            {
                oa.PropostaLCID = propostaLC.ID;
                oa.ID = Guid.NewGuid();
            }

            #endregion

            #region Referencias

            foreach (var ra in propostaLC.Referencias)
            {
                ra.PropostaLCID = propostaLC.ID;
                ra.ID = Guid.NewGuid();
            }

            #endregion

            #region Bens Rurais

            foreach (var br in propostaLC.BensRurais)
            {
                br.PropostaLCID = propostaLC.ID;
                br.ID = Guid.NewGuid();
            }

            #endregion

            #region Bens Urbanos

            foreach (var bu in propostaLC.BensUrbanos)
            {
                bu.PropostaLCID = propostaLC.ID;
                bu.ID = Guid.NewGuid();
            }

            #endregion

            #region Maquinas e Equipamentos

            foreach (var mee in propostaLC.MaquinasEquipamentos)
            {
                mee.PropostaLCID = propostaLC.ID;
                mee.ID = Guid.NewGuid();
            }

            #endregion

            #region Precos por Regiao

            foreach (var ppr in propostaLC.PrecosPorRegiao)
            {
                ppr.PropostaLCID = propostaLC.ID;
                ppr.ID = Guid.NewGuid();
            }

            #endregion

            #region Tipo Endividamento

            foreach (var te in propostaLC.TiposEndividamento)
            {
                te.PropostaLCID = propostaLC.ID;
                te.ID = Guid.NewGuid();
            }

            #endregion

            var propInsert = propostaLC.MapTo<PropostaLC>();

            _unitOfWork.PropostaLCRepository.Insert(propInsert);
        }

        private void SaveHistorico(PropostaLC proposta)
        {
            var status = proposta.PropostaLcStatus != null ? proposta.PropostaLcStatus.Nome : proposta.PropostaLCStatusID;

            var historico = new PropostaLCHistorico
            {
                ID = Guid.NewGuid(),
                UsuarioID = proposta.UsuarioIDAlteracao ?? proposta.UsuarioIDCriacao,
                PropostaLCID = proposta.ID,
                PropostaLCStatusID = proposta.PropostaLCStatusID,
                DataCriacao = DateTime.Now,
                Descricao = $"A Proposta LC{proposta.NumeroInternoProposta:00000}/{proposta.DataCriacao:yyyy} está no Status { status }"
            };

            _unitOfWork.PropostaLCHistorico.Insert(historico);
        }

        #endregion
    }
}



