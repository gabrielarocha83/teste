using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.Domain.Repository;
using System.Linq;
using Yara.Domain.Entities.Procedures;

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.AppService
{
    public class AppServiceContaClienteFinanceiro : IAppServiceContaClienteFinanceiro
    {
        private readonly IUnitOfWork _untUnitOfWork;

        public AppServiceContaClienteFinanceiro(IUnitOfWork untUnitOfWork)
        {
            _untUnitOfWork = untUnitOfWork;
        }

        public async Task<ContaClienteFinanceiroDto> GetAsync(Expression<Func<ContaClienteFinanceiroDto, bool>> expression)
        {
            var contacliente = await _untUnitOfWork.ContaClienteFinanceiroRepository.GetAsync(Mapper.Map<Expression<Func<ContaClienteFinanceiro, bool>>>(expression)) ?? new ContaClienteFinanceiro { LC = 0, LCAdicional = 0, Exposicao = 0 };

            var limiteYara = await _untUnitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.EmpresasID.Equals("Y") && c.ContaClienteID.Equals(contacliente.ContaClienteID)) ?? new ContaClienteFinanceiro { LC = 0, LCAdicional = 0, Exposicao = 0, Empresas = new Empresas() { ID = "Y", Nome = "Yara" } };

            var limiteGalvani = await _untUnitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.EmpresasID.Equals("G") && c.ContaClienteID.Equals(contacliente.ContaClienteID)) ?? new ContaClienteFinanceiro { LC = 0, LCAdicional = 0, Exposicao = 0, Empresas = new Empresas() { ID = "G", Nome = "Galvani" } };

            #region Grupo Compartilhado

            var grupoYara = await _untUnitOfWork.GrupoEconomicoMembroReporitory.GetAsync(c => c.ContaClienteID == contacliente.ContaClienteID
                                                                                    && (c.StatusGrupoEconomicoFluxoID.Equals("AP") || c.StatusGrupoEconomicoFluxoID.Equals("PE"))
                                                                                    && c.GrupoEconomico.EmpresasID.Equals("Y")
                                                                                    && c.Ativo && c.GrupoEconomico.Ativo
                                                                                    && c.GrupoEconomico.TipoRelacaoGrupoEconomico.ClassificacaoGrupoEconomicoID == 1);

            if (grupoYara != null)
            {
                var membroPrincipalY = await _untUnitOfWork.GrupoEconomicoMembroReporitory.GetAsync(
                                c => c.GrupoEconomicoID == grupoYara.GrupoEconomicoID
                                && c.MembroPrincipal
                                && c.Ativo);

                if (membroPrincipalY != null)
                {
                    limiteYara =
                        await _untUnitOfWork.ContaClienteFinanceiroRepository.GetAsync(
                            c => c.EmpresasID.Equals("Y") && c.ContaClienteID.Equals(membroPrincipalY.ContaClienteID)) ??
                        new ContaClienteFinanceiro
                        {
                            LC = 0,
                            LCAdicional = 0,
                            Exposicao = 0,
                            Empresas = new Empresas() { ID = "Y", Nome = "Yara" }
                        };
                }
            }

            var grupoGalvani = await _untUnitOfWork.GrupoEconomicoMembroReporitory.GetAsync(c => c.ContaClienteID == contacliente.ContaClienteID
                                                                        && (c.StatusGrupoEconomicoFluxoID.Equals("AP") || c.StatusGrupoEconomicoFluxoID.Equals("PE"))
                                                                        && c.GrupoEconomico.EmpresasID.Equals("G")
                                                                        && c.Ativo && c.GrupoEconomico.Ativo
                                                                        && c.GrupoEconomico.TipoRelacaoGrupoEconomico.ClassificacaoGrupoEconomicoID == 1);

            if (grupoGalvani != null)
            {
                var membroPrincipalG = await _untUnitOfWork.GrupoEconomicoMembroReporitory.GetAsync(
                                c => c.GrupoEconomicoID == grupoGalvani.GrupoEconomicoID
                                && c.MembroPrincipal
                                && c.Ativo);

                if (membroPrincipalG != null)
                {
                    limiteGalvani =
                           await _untUnitOfWork.ContaClienteFinanceiroRepository.GetAsync(
                               c => c.EmpresasID.Equals("G") && c.ContaClienteID.Equals(membroPrincipalG.ContaClienteID)) ??
                           new ContaClienteFinanceiro
                           {
                               LC = 0,
                               LCAdicional = 0,
                               Exposicao = 0,
                               Empresas = new Empresas() { ID = "G", Nome = "Galvani" }
                           };
                }
            }

            #endregion

            var dadosfinanceiros = contacliente.MapTo<ContaClienteFinanceiroDto>();
            dadosfinanceiros.Limites.Add(limiteYara.MapTo<ContaClienteFinanceiroDto>());
            dadosfinanceiros.Limites.Add(limiteGalvani.MapTo<ContaClienteFinanceiroDto>());

            return dadosfinanceiros;
        }

        public async Task<ContaClienteFinanceiroDto> GetRawAsync(Expression<Func<ContaClienteFinanceiroDto, bool>> expression)
        {
            var contacliente = await _untUnitOfWork.ContaClienteFinanceiroRepository.GetAsync(Mapper.Map<Expression<Func<ContaClienteFinanceiro, bool>>>(expression));

            var limiteYara = await _untUnitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.EmpresasID.Equals("Y") && c.ContaClienteID.Equals(contacliente.ContaClienteID)) ?? new ContaClienteFinanceiro { LC = 0, LCAdicional = 0, Exposicao = 0, Empresas = new Empresas() { ID = "Y", Nome = "Yara" } };
            var limiteGalvani = await _untUnitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.EmpresasID.Equals("G") && c.ContaClienteID.Equals(contacliente.ContaClienteID)) ?? new ContaClienteFinanceiro { LC = 0, LCAdicional = 0, Exposicao = 0, Empresas = new Empresas() { ID = "G", Nome = "Galvani" } };

            var dadosfinanceiros = contacliente.MapTo<ContaClienteFinanceiroDto>();
            dadosfinanceiros.Limites.Add(limiteYara.MapTo<ContaClienteFinanceiroDto>());
            dadosfinanceiros.Limites.Add(limiteGalvani.MapTo<ContaClienteFinanceiroDto>());

            return dadosfinanceiros;
        }

        public async Task<IEnumerable<ContaClienteFinanceiroDto>> GetAllFilterAsync(Expression<Func<ContaClienteFinanceiroDto, bool>> expression)
        {
            var contaCliente = await _untUnitOfWork.ContaClienteFinanceiroRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<ContaClienteFinanceiro, bool>>>(expression));
            return Mapper.Map<IEnumerable<ContaClienteFinanceiroDto>>(contaCliente);
        }

        public async Task<IEnumerable<ContaClienteFinanceiroDto>> GetAllAsync()
        {
            var contaCliente = await _untUnitOfWork.ContaClienteFinanceiroRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<ContaClienteFinanceiroDto>>(contaCliente);
        }

        public bool Insert(ContaClienteFinanceiroDto obj)
        {
            var financeiro = obj.MapTo<ContaClienteFinanceiro>();

            if (!obj.PermiteEnviarSeguradora)
                obj.DataSeguradora = null;

            _untUnitOfWork.ContaClienteFinanceiroRepository.Insert(financeiro);

            return _untUnitOfWork.Commit();
        }

        public async Task<bool> Update(ContaClienteFinanceiroDto obj)
        {
            var financeiro = await _untUnitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.ContaClienteID.Equals(obj.ContaClienteID) && c.EmpresasID.Equals(obj.EmpresasID));

            if (obj.DataSeguradora != null && obj.PermiteEnviarSeguradora)
                financeiro.DataSeguradora = obj.DataSeguradora;
            else
            {
                if (obj.PermiteConceito)
                    financeiro.Conceito = obj.Conceito;

                if (obj.PermiteSinistro)
                    financeiro.Sinistro = obj.Sinistro;

                if (obj.PermitePdd && obj.Pdd != null)
                {
                    // Atualizar % de PDD na ultima proposta juridica em aberta do cliente caso exista.
                    var juridico = await _untUnitOfWork.PropostaJuridicoRepository.GetAsync(c => c.ContaClienteID == obj.ContaClienteID && c.EmpresaID == obj.EmpresasID && (c.PropostaJuridicoStatus != "CA" || c.PropostaJuridicoStatus != "AP"));

                    if (juridico != null)
                    {
                        juridico.PercentualPdd = obj.Pdd;

                        _untUnitOfWork.PropostaJuridicoRepository.Update(juridico);
                    }

                    financeiro.Pdd = obj.Pdd;
                }

                if (obj.ConceitoCobrancaID == null || obj.ConceitoCobrancaID == Guid.Empty) // Removendo!
                {
                    // Remove o conceito.
                    await UpdateConceitoCobranca(obj.ContaClienteID, obj.EmpresasID, null);
                }
                else if (obj.ConceitoCobrancaID != null && obj.ConceitoCobrancaID != Guid.Empty)
                {
                    // Atualiza o conceito.
                    await UpdateConceitoCobranca(obj.ContaClienteID, obj.EmpresasID, obj.ConceitoCobrancaID);
                }
            }

            _untUnitOfWork.ContaClienteFinanceiroRepository.Update(financeiro);

            return _untUnitOfWork.Commit();
        }

        public async Task<bool> UpdateConceitoCobranca(ConceitoCobrancaLiberacaoLogDto obj)
        {
            try
            {
                var financeiro = await _untUnitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.ContaClienteID.Equals(obj.ContaClienteId) && c.EmpresasID.Equals(obj.EmpresaID));
                financeiro.Conceito = obj.Status;
                financeiro.DescricaoConceito = obj.ComentarioStatus;

                _untUnitOfWork.ContaClienteFinanceiroRepository.UpdateConceito(financeiro);

                var contacliente = await _untUnitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(obj.ContaClienteId));
                var usuario = await _untUnitOfWork.UsuarioRepository.GetAsync(c => c.ID.Equals(obj.UsuarioID));

                // Reprocessar a carteira do cliente
                var processamentocarteira = new ProcessamentoCarteiraDto();
                processamentocarteira.ID = Guid.NewGuid();
                processamentocarteira.DataHora = DateTime.Now;
                processamentocarteira.Status = 2;
                processamentocarteira.Motivo = (obj.Status ? "Bloqueio" : "Liberação") + " de Conceito de Cobrança";
                processamentocarteira.Detalhes = $"O Conceito de Cobrança do cliente {contacliente.CodigoPrincipal} com o comentário feito pelo usuário {usuario.Nome}";
                processamentocarteira.Cliente = contacliente.CodigoPrincipal;
                processamentocarteira.EmpresaID = obj.EmpresaID;
                _untUnitOfWork.ProcessamentoCarteiraRepository.Insert(processamentocarteira.MapTo<ProcessamentoCarteira>());

                return _untUnitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ConceitoManualContaClienteDto> UpdateConceitoCobranca(ConceitoManualContaClienteDto conceitoManualContaClienteDto)
        {
            try
            {
                // Atualiza o conceito.
                var ret = await UpdateConceitoCobranca(conceitoManualContaClienteDto.ContaClienteID, conceitoManualContaClienteDto.EmpresasID, (conceitoManualContaClienteDto.ConceitoCobrancaID == null || conceitoManualContaClienteDto.ConceitoCobrancaID == Guid.Empty ? (Guid?)null : conceitoManualContaClienteDto.ConceitoCobrancaID));
                conceitoManualContaClienteDto.LogMessage = ret;

                return conceitoManualContaClienteDto;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<LimiteCreditoClienteDto> GetLimiteCreditoContaCliente(string codigoprincipal)
        {
            try
            {
                var limitecredito = await _untUnitOfWork.ContaClienteFinanceiroRepository.GetLimiteCreditoContaCliente(codigoprincipal);
                return limitecredito.MapTo<LimiteCreditoClienteDto>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ContaClienteFinanceiroDto> GetCodSAPFinanceiro(string codigoSAP, string Empresa)
        {
            var codigo = await _untUnitOfWork.ContaClienteCodigoRepository.GetAsync(c => c.Codigo.Equals(codigoSAP));
            ContaClienteFinanceiroDto financeiro = new ContaClienteFinanceiroDto();

            if (codigo != null)
            {
                financeiro = await this.GetAsync(c => c.ContaClienteID.Equals(codigo.ContaClienteID) && c.EmpresasID.Equals(Empresa));
            }

            return financeiro;
        }

        public async Task<DadosFinanceiroTituloDto> GetDadosFinanceiroSomatoriaTitulos(Guid contaClienteId, string empresaId)
        {
            try
            {
                var somatoriaTitulos = await _untUnitOfWork.ContaClienteFinanceiroRepository.GetDadosFinanceiroSomatoriaTitulos(contaClienteId, empresaId);
                return somatoriaTitulos.MapTo<DadosFinanceiroTituloDto>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ContaClienteResumoCobrancaDto> GetResumoCobranca(Guid contaClienteId, string empresaId)
        {
            ContaClienteResumoCobrancaDto resumo = new ContaClienteResumoCobrancaDto();

            // Buscar Dados Financeiros
            var contaClienteFinanceiro = await _untUnitOfWork.ContaClienteFinanceiroRepository.GetAsync(ccf => ccf.ContaClienteID == contaClienteId && ccf.EmpresasID == empresaId);

            if (contaClienteFinanceiro != null)
            {
                resumo.Pdd = contaClienteFinanceiro.Pdd;
                resumo.Sinistro = contaClienteFinanceiro.Sinistro;
                resumo.DataSeguradora = contaClienteFinanceiro.DataSeguradora;
            }

            // Buscar Visita
            var visitas = await _untUnitOfWork.ContaClienteVisitaRepository.GetAllFilterAsync(v => v.ContaClienteID == contaClienteId && v.EmpresasID == empresaId);

            if (visitas != null && visitas.Any())
            {
                if (visitas.Any(v => v.DataParecer.HasValue))
                {
                    resumo.UltimaVisitaRealizada = visitas.Where(v => v.DataParecer.HasValue).Max(v => v.DataParecer);
                }
                resumo.UltimaVisitaSolicitada = visitas.Max(v => v.DataSolicitacao);
            }

            // Buscar Principais Culturas
            var culturas = await _untUnitOfWork.PropostaLCRepository.GetCulturasCliente(contaClienteId, empresaId);

            if (culturas != null)
            {
                resumo.PrincipaisCulturas = String.Join(", ", culturas.Select(c => c.Cultura.Descricao).ToArray());
            }

            return resumo;
        }

        public async Task<LimiteYaraGalvaniDto> UpdateLimiteCredito(LimiteYaraGalvaniDto limiteYaraGalvaniDto)
        {
            var retorno = new LimiteYaraGalvaniDto();

            try
            {
                var financeiroClientes = await _untUnitOfWork.ContaClienteFinanceiroRepository.GetAllFilterAsync(c => c.ContaClienteID == limiteYaraGalvaniDto.ContaClienteId);

                var dadosYara = financeiroClientes.FirstOrDefault(c => c.EmpresasID == "Y");
                var dadosGalvani = financeiroClientes.FirstOrDefault(c => c.EmpresasID == "G");

                var LCYara = (DateTime.Now <= dadosYara.VigenciaFim ? dadosYara.LC : 0);
                var LCGalvani = (DateTime.Now <= dadosGalvani.VigenciaFim ? dadosGalvani.LC : 0);

                var LCAtualTotal = LCYara + LCGalvani;
                var NovoLCTotal = limiteYaraGalvaniDto.LimiteGalvani + limiteYaraGalvaniDto.LimiteYara;

                if (NovoLCTotal > LCAtualTotal)
                    throw new ArgumentException($"Valor superior ao total de {LCAtualTotal.Value.ToString("C")}.");

                // Grupos Econômicos em que o cliente não é membro principal
                IEnumerable<BuscaGrupoEconomico> grupos;
                BuscaGrupoEconomico grupoCompartilhado;

                grupos = await _untUnitOfWork.GrupoEconomicoReporitory.BuscaGrupoEconomico(limiteYaraGalvaniDto.ContaClienteId, "Y");
                grupoCompartilhado = grupos.FirstOrDefault(c => c.ClassificacaoID.Equals(1));

                if (grupoCompartilhado != null)
                {
                    var membroPrincipalGrupoYara = await _untUnitOfWork.GrupoEconomicoMembroReporitory.GetAsync(c => c.GrupoEconomicoID.Equals(grupoCompartilhado.GrupoId) && c.MembroPrincipal);
                    if (membroPrincipalGrupoYara.ContaClienteID != limiteYaraGalvaniDto.ContaClienteId /* && LCYara != limiteYaraGalvaniDto.LimiteYara */)
                    {
                        throw new ArgumentException($"O cliente faz parte de Grupo Econômico Compartilhado e seu LC não pode ser editado. Favor realizar a alteração no membro principal do grupo.");
                    }
                }

                grupos = await _untUnitOfWork.GrupoEconomicoReporitory.BuscaGrupoEconomico(limiteYaraGalvaniDto.ContaClienteId, "G");
                grupoCompartilhado = grupos.FirstOrDefault(c => c.ClassificacaoID.Equals(1));

                if (grupoCompartilhado != null)
                {
                    var membroPrincipalGrupoGalvani = await _untUnitOfWork.GrupoEconomicoMembroReporitory.GetAsync(c => c.GrupoEconomicoID.Equals(grupoCompartilhado.GrupoId) && c.MembroPrincipal);
                    if (membroPrincipalGrupoGalvani.ContaClienteID != limiteYaraGalvaniDto.ContaClienteId /* && LCYara != limiteYaraGalvaniDto.LimiteYara */)
                    {
                        throw new ArgumentException($"O cliente faz parte de Grupo Econômico Compartilhado e seu LC não pode ser editado. Favor realizar a alteração no membro principal do grupo.");
                    }
                }

                retorno.LimiteYara = limiteYaraGalvaniDto.LimiteYara;
                retorno.LimiteGalvani = limiteYaraGalvaniDto.LimiteGalvani;

                // Só efetivar a ação se houver diferença de valores -> houve alteração.
                if (LCYara != limiteYaraGalvaniDto.LimiteYara)
                {
                    retorno.LimiteYara = LCYara;
                    dadosYara.LC = limiteYaraGalvaniDto.LimiteYara;
                    _untUnitOfWork.ContaClienteFinanceiroRepository.Update(dadosYara);

                    // Edita o LC da proposta de LC em andamento
                    var propostaLC = (await _untUnitOfWork.PropostaLCRepository.GetAllFilterAsync(c => c.ContaClienteID.Equals(limiteYaraGalvaniDto.ContaClienteId) && c.EmpresaID.Equals("Y") && (c.PropostaLCStatusID != "AA" && c.PropostaLCStatusID != "XE" && c.PropostaLCStatusID != "XR"))).OrderByDescending(c => c.DataCriacao).FirstOrDefault();
                    if (propostaLC != null)
                    {
                        propostaLC.LCCliente = limiteYaraGalvaniDto.LimiteYara;
                        _untUnitOfWork.PropostaLCRepository.Update(propostaLC);
                    }

                    // Edita o LC da proposta de LA em andamento
                    var propostaLA = (await _untUnitOfWork.PropostaLCAdicionalRepository.GetAllFilterAsync(c => c.ContaClienteID.Equals(limiteYaraGalvaniDto.ContaClienteId) && c.EmpresaID.Equals("Y") && (c.PropostaLCStatusID != "AA" && c.PropostaLCStatusID != "XE" && c.PropostaLCStatusID != "XR"))).OrderByDescending(c => c.DataCriacao).FirstOrDefault();
                    if (propostaLA != null)
                    {
                        propostaLA.LCCliente = limiteYaraGalvaniDto.LimiteYara;
                        _untUnitOfWork.PropostaLCAdicionalRepository.Update(propostaLA);
                    }

                    var pcYara = new ProcessamentoCarteira();
                    pcYara.ID = Guid.NewGuid();
                    pcYara.Cliente = dadosYara.ContaCliente.CodigoPrincipal;
                    pcYara.DataHora = DateTime.Now;
                    pcYara.Status = 2;
                    pcYara.Motivo = "Alteração manual de Limite de Crédito.";
                    pcYara.Detalhes = pcYara.Motivo;
                    pcYara.EmpresaID = "Y";
                    _untUnitOfWork.ProcessamentoCarteiraRepository.Insert(pcYara);
                }

                // Só efetivar a ação se houver diferença de valores -> houve alteração.
                if (LCGalvani != limiteYaraGalvaniDto.LimiteGalvani)
                {
                    retorno.LimiteGalvani = LCGalvani;
                    dadosGalvani.LC = limiteYaraGalvaniDto.LimiteGalvani;
                    _untUnitOfWork.ContaClienteFinanceiroRepository.Update(dadosGalvani);

                    // Edita o LC da proposta de LC em andamento
                    var propostaLC = (await _untUnitOfWork.PropostaLCRepository.GetAllFilterAsync(c => c.ContaClienteID.Equals(limiteYaraGalvaniDto.ContaClienteId) && c.EmpresaID.Equals("G") && (c.PropostaLCStatusID != "AA" && c.PropostaLCStatusID != "XE" && c.PropostaLCStatusID != "XR"))).OrderBy(c => c.DataCriacao).FirstOrDefault();
                    if (propostaLC != null)
                    {
                        propostaLC.LCCliente = limiteYaraGalvaniDto.LimiteGalvani;
                        _untUnitOfWork.PropostaLCRepository.Update(propostaLC);
                    }

                    // Edita o LC da proposta de LA em andamento
                    var propostaLA = (await _untUnitOfWork.PropostaLCAdicionalRepository.GetAllFilterAsync(c => c.ContaClienteID.Equals(limiteYaraGalvaniDto.ContaClienteId) && c.EmpresaID.Equals("G") && (c.PropostaLCStatusID != "AA" && c.PropostaLCStatusID != "XE" && c.PropostaLCStatusID != "XR"))).OrderBy(c => c.DataCriacao).FirstOrDefault();
                    if (propostaLA != null)
                    {
                        propostaLC.LCCliente = limiteYaraGalvaniDto.LimiteGalvani;
                        _untUnitOfWork.PropostaLCAdicionalRepository.Update(propostaLA);
                    }

                    var pcGalvani = new ProcessamentoCarteira();
                    pcGalvani.ID = Guid.NewGuid();
                    pcGalvani.Cliente = dadosGalvani.ContaCliente.CodigoPrincipal;
                    pcGalvani.DataHora = DateTime.Now;
                    pcGalvani.Status = 2;
                    pcGalvani.Motivo = "Alteração manual de Limite de Crédito.";
                    pcGalvani.Detalhes = pcGalvani.Motivo;
                    pcGalvani.EmpresaID = "G";
                    _untUnitOfWork.ProcessamentoCarteiraRepository.Insert(pcGalvani);
                }

                if (LCYara != limiteYaraGalvaniDto.LimiteYara || LCGalvani != limiteYaraGalvaniDto.LimiteGalvani)
                    _untUnitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }

            return retorno;
        }

        #region Helpers

        private async Task<string> UpdateConceitoCobranca(Guid contaClienteID, string empresaId, Guid? novoConceito)
        {
            string retorno = null;

            try
            {
                var financeiroCliente = await _untUnitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.ContaClienteID == contaClienteID && c.EmpresasID == empresaId);
                if (financeiroCliente != null)
                {
                    financeiroCliente.ConceitoCobrancaIDAnterior = financeiroCliente.ConceitoCobrancaID;
                    financeiroCliente.DescricaoConceitoAnterior = financeiroCliente.DescricaoConceito;
                    financeiroCliente.ConceitoAnterior = financeiroCliente.Conceito;

                    if (novoConceito == null)
                    {
                        financeiroCliente.ConceitoCobrancaID = null;
                        financeiroCliente.DescricaoConceito = string.Empty;
                        financeiroCliente.Conceito = false;
                    }
                    else
                    {
                        var conceitoCobranca = await _untUnitOfWork.ConceitoCobrancaRepository.GetAsync(c => c.ID == novoConceito);

                        financeiroCliente.ConceitoCobrancaID = conceitoCobranca.ID;
                        financeiroCliente.DescricaoConceito = conceitoCobranca.Descricao;
                        financeiroCliente.Conceito = conceitoCobranca.Ativo;
                    }

                    _untUnitOfWork.ContaClienteFinanceiroRepository.UpdateConceito(financeiroCliente);

                    retorno = $"Conceito de Cobrança {(financeiroCliente.ConceitoCobrancaID == null ? "removido do" : financeiroCliente.DescricaoConceito + " adicionado ao")} cliente {financeiroCliente.ContaCliente.CodigoPrincipal}.";

                    var pc = new ProcessamentoCarteira();
                    pc.ID = Guid.NewGuid();
                    pc.Cliente = financeiroCliente.ContaCliente.CodigoPrincipal;
                    pc.DataHora = DateTime.Now;
                    pc.Status = 2;
                    pc.Motivo = $"{(novoConceito == null ? "Remoção" : "Adição")} de Conceito de Cobrança";
                    pc.Detalhes = retorno;
                    pc.EmpresaID = empresaId;

                    _untUnitOfWork.ProcessamentoCarteiraRepository.Insert(pc);

                    _untUnitOfWork.Commit();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return retorno;
        }

        #endregion
    }
}