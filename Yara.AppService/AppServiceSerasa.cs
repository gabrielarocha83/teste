using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.Domain.Repository;
using Yara.Service.Serasa.Concentre.Entities;
using Yara.Service.Serasa.Concentre.Return;
using Yara.Service.Serasa.Concentre.Send;
using Yara.Service.Serasa.Crednet.Entities;
using Yara.Service.Serasa.Crednet.Return;
using Yara.Service.Serasa.Crednet.Send;
using Yara.Service.Serasa.Relato.Entities;
using Yara.Service.Serasa.Relato.Send;
using System.Text.RegularExpressions;
using Yara.Service.Serasa;
using Yara.Domain.Entities.Procedures;

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.AppService
{
    public class AppServiceSerasa : IAppServiceSerasa
    {
        private readonly IUnitOfWork _unitOfWork;

        private string _urlSerasa;
        private string _usuarioSerasa;
        private string _senhaSerasa;

        public AppServiceSerasa(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<dynamic> ConsultarSerasa(SolicitanteSerasaDto solicitante, string EmpresaID, string urlSerasa, string usuarioSerasa, string senhaSerasa)
        {
            _urlSerasa = urlSerasa;
            _usuarioSerasa = usuarioSerasa;
            _senhaSerasa = senhaSerasa;

            var conta = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(solicitante.ContaClienteID));

            if (conta.TipoCliente == null || conta.TipoClienteID == Guid.Empty)
                throw new ArgumentException($"O cliente {conta.Nome} não possuí especificação de Tipo de Cliente para realizar uma consulta no Serasa.");

            solicitante.ID = Guid.NewGuid();
            solicitante.Documento = conta.Documento;
            solicitante.TipoSerasa = conta.TipoCliente.TipoSerasa == TipoSerasa.Concentre && conta.Documento.Trim().Length > 11 ? TipoSerasaDto.CredNet : conta.TipoCliente.TipoSerasa.MapTo<TipoSerasaDto>();

            var serasa = solicitante.MapTo<SolicitanteSerasa>();
            var temPendenciaSerasa = false;
            DateTime? dataFundacao = null;

            decimal total = 0;

            switch (serasa.TipoSerasa)
            {
                case (TipoSerasa)1:
                    ReturnConcentre retornoConcentre;
                    try
                    {
                        retornoConcentre = await SendConcentre(conta.Documento.Trim());
                        temPendenciaSerasa = retornoConcentre.ParticipacaoSocietarias.Any(c => c.RestricaoParticipante);
                        dataFundacao = retornoConcentre.DataConfirmacao;
                    }
                    catch (SerasaException ex)
                    {
                        throw new ArgumentException(ex.Message);
                    }
                    serasa.Json = JsonConvert.SerializeObject(retornoConcentre);
                    serasa.TemPendenciaSerasa = temPendenciaSerasa;
                    total = await SavePendenciasConcentre(serasa?.MapTo<SolicitanteSerasaDto>(), retornoConcentre);
                    break;
                case (TipoSerasa)2:
                    ReturnCrednet retornoCrednet;
                    try
                    {
                        retornoCrednet = await SendCredNet(conta.Documento.Trim());
                        temPendenciaSerasa = false;
                        dataFundacao = retornoCrednet.Data;
                    }
                    catch (SerasaException ex)
                    {
                        throw new ArgumentException(ex.Message);
                    }
                    serasa.Json = JsonConvert.SerializeObject(retornoCrednet);
                    serasa.TemPendenciaSerasa = temPendenciaSerasa;
                    total = await SavePendenciasCredNet(serasa?.MapTo<SolicitanteSerasaDto>(), retornoCrednet);
                    break;
                case (TipoSerasa)3:
                    if (conta.Documento.Trim().Length < 14)
                        throw new ArgumentException($"O cliente {conta.Nome} não pode consultar o tipo serasa Relato, pois possui um CPF cadastrado.");

                    ReturnRelato retornoRelato;
                    try
                    {
                        retornoRelato = await SendSerasaRelato(conta.Documento.Trim());
                        temPendenciaSerasa = retornoRelato.ParticipantesAnotacoes.Any();
                        dataFundacao = retornoRelato.Fundacao;
                    }
                    catch (SerasaException ex)
                    {
                        throw new ArgumentException(ex.Message);
                    }
                    serasa.Json = JsonConvert.SerializeObject(retornoRelato);
                    serasa.TemPendenciaSerasa = temPendenciaSerasa;
                    total = await SavePendenciasRelato(serasa?.MapTo<SolicitanteSerasaDto>(), retornoRelato);
                    break;
                case 0:
                    // throw new ArgumentException($"Este cliente não possuí especificação de Serasa para o Tipo de Cliente{conta.TipoCliente.Nome}");
                    break;
            }

            // serasa.ID = Guid.NewGuid();
            serasa.DataCriacao = DateTime.Now;
            serasa.UsuarioID = solicitante.UsuarioIDCriacao;
            serasa.UsuarioIDCriacao = solicitante.UsuarioIDCriacao;

            serasa.ContaCliente = null;
            serasa.ContaClienteID = conta.ID;

            serasa.TipoCliente = null;
            serasa.TipoClienteID = conta.TipoClienteID.Value;

            serasa.TipoSerasa = solicitante.TipoSerasa.MapTo<TipoSerasa>();
            serasa.Total = total;

            try
            {
                _unitOfWork.SolicitanteSerasaRepository.Insert(serasa);
                _unitOfWork.Commit();

                // if ((total > 0 || temPendenciaSerasa))
                    await SaveContaCliente(new ClienteSolicitanteSerasaDto { ContaClienteID = conta.ID, TemPendenciaSerasa = temPendenciaSerasa, TipoClienteID = conta.TipoClienteID.Value, DataRegistro = dataFundacao, OrigemDocumento = OrigemDocumentoDto.ContaCliente }, serasa.ID, total, serasa.TipoSerasa, EmpresaID);

                return JsonConvert.DeserializeObject<dynamic>(serasa.Json);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task<bool> SaveContaCliente(ClienteSolicitanteSerasaDto obj, Guid SolicitanteID, decimal total, TipoSerasa tipo, string EmpresaID)
        {
            var conta = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(obj.ContaClienteID));
            var tipoCliente = await _unitOfWork.TipoClienteRepository.GetAsync(c => c.ID.Equals(obj.TipoClienteID));

            conta.SolicitanteSerasaID = SolicitanteID;
            conta.TipoConsultaSolicitanteSerasaID = (int)tipo;
            
            if (obj.DataRegistro.HasValue && obj.OrigemDocumento == OrigemDocumentoDto.ContaCliente)
            {
                conta.DataNascimentoFundacao = obj.DataRegistro;
            }

            if (total == 0 && !obj.TemPendenciaSerasa)
            {
                conta.RestricaoSerasa = false;
                conta.PendenciaSerasa = RestricaoSerasa.NaoPossuiRestricao;
            }
            else if (total < tipoCliente.Valor && !obj.TemPendenciaSerasa)
            {
                conta.RestricaoSerasa = false;
                conta.PendenciaSerasa = RestricaoSerasa.RestricaoIrrelevante;
            }
            else
            {
                conta.RestricaoSerasa = true;
                conta.PendenciaSerasa = RestricaoSerasa.RestricaoRelevante;
            }

            _unitOfWork.ContaClienteRepository.Update(conta);

            var processamentocarteira = new ProcessamentoCarteira
            {
                Cliente = conta.CodigoPrincipal,
                DataHora = DateTime.Now,
                Motivo = $"Consulta de Serasa na conta cliente {conta.Nome}",
                Status = 2,
                Detalhes = "Consulta Serasa",
                EmpresaID = EmpresaID,
                ID = Guid.NewGuid()
            };

            _unitOfWork.ProcessamentoCarteiraRepository.Insert(processamentocarteira);

            return _unitOfWork.Commit();
        }

        public async Task<dynamic> ConsultarSerasaAlcadaComercial(SolicitanteSerasaDto solicitante, Guid Proposta, string EmpresaID, string urlSerasa, string usuarioSerasa, string senhaSerasa)
        {
            _urlSerasa = urlSerasa;
            _usuarioSerasa = usuarioSerasa;
            _senhaSerasa = senhaSerasa;

            var alcada = await _unitOfWork.PropostaAlcadaComercial.GetAsync(c => c.ID.Equals(Proposta));
            var conta = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(alcada.ContaClienteID));

            if (conta.TipoCliente == null || conta.TipoClienteID == null || conta.TipoClienteID == Guid.Empty || alcada.TipoCliente == null || alcada.TipoClienteID == null || alcada.TipoClienteID == Guid.Empty)
                throw new ArgumentException("Este cliente não possuí especificação de Tipo de Cliente para realizar uma consulta no Serasa.");
            else
                alcada.RestricaoSerasa = false;

            // Antes de consultar Serasa para a proposta, limpa a flag dela para que a nova consulta não utilize a flag já gravada no banco.
            _unitOfWork.PropostaAlcadaComercial.Update(alcada);
            _unitOfWork.Commit();

            // Regex para remover pontos, dígitos e barras do número do documento.
            var rgx = new Regex("[\\.\\-\\/]+");

            IList<ClienteSolicitanteSerasaDto> solicitantes = new List<ClienteSolicitanteSerasaDto>();

            // Conta.
            solicitantes.Add(new ClienteSolicitanteSerasaDto()
            {
                Codigo = alcada.ID,
                ContaClienteID = alcada.ContaClienteID,
                Documento = alcada.ContaCliente.Documento,
                TipoClienteID = alcada.TipoClienteID.Value,
                TemPendenciaSerasa = false,
                TipoSerasa = (alcada.TipoCliente.TipoSerasa == TipoSerasa.Concentre && rgx.Replace(alcada.ContaCliente.Documento.Trim(), "").Length > 11 ? TipoSerasaDto.CredNet : alcada.TipoCliente.TipoSerasa.MapTo<TipoSerasaDto>()),
                OrigemDocumento = OrigemDocumentoDto.ContaCliente
            });

            // Cônjugue.
            if (!string.IsNullOrEmpty(alcada.CPFConjugue))
            { // Se tiver CPFConjugue, o documento deles(as) sempre deverá ser consultado como Concentre por se tratar de uma PF.
                solicitantes.Add(new ClienteSolicitanteSerasaDto()
                {
                    Codigo = alcada.ID,
                    ContaClienteID = alcada.ContaClienteID,
                    Documento = alcada.CPFConjugue,
                    TipoClienteID = alcada.TipoClienteID.Value,
                    TemPendenciaSerasa = false,
                    TipoSerasa = TipoSerasaDto.Concentre,
                    OrigemDocumento = OrigemDocumentoDto.Conjugue
                });
            }

            // CPFs ou CNPJs de outras pessoas / empresas.
            foreach (var documento in alcada.Documentos)
            { // Se tiver Documentos, verificar se é CPF ou CNPJ para buscar Concentre ou CredNet respectivamente.
                solicitantes.Add(new ClienteSolicitanteSerasaDto()
                {
                    Codigo = documento.ID,
                    ContaClienteID = alcada.ContaClienteID,
                    Documento = documento.Documento,
                    TipoSerasa = (rgx.Replace(documento.Documento.Trim(), "").Length > 14 ? TipoSerasaDto.CredNet : TipoSerasaDto.Concentre),
                    TipoClienteID = alcada.TipoClienteID.Value,
                    OrigemDocumento = OrigemDocumentoDto.Proposta
                });
            }

            // Parceiros Agrícolas.
            foreach (var parceria in alcada.ParceriasAgricolas)
            { // Se tiver parceiros, o documento deles sempre deverá ser consultado como Concentre.
                solicitantes.Add(new ClienteSolicitanteSerasaDto()
                {
                    Codigo = parceria.ID,
                    ContaClienteID = alcada.ContaClienteID,
                    Documento = parceria.Documento,
                    TipoSerasa = TipoSerasaDto.Concentre,
                    TipoClienteID = alcada.TipoClienteID.Value,
                    OrigemDocumento = OrigemDocumentoDto.Parceiro
                });
            }

            var parametro = await _unitOfWork.ParametroSistemaRepository.GetAsync(c => c.Chave == "serasa" && c.EmpresasID == EmpresaID);
            var dias = int.Parse(parametro.Valor);
            var datalimite = DateTime.Now.AddDays(dias * -1);

            foreach (var sol in solicitantes)
            {
                // Documento trimado, sem pontos e vírgulas.
                var docSolicitante = rgx.Replace(sol.Documento.Trim(), "");

                var exist = await _unitOfWork.SolicitanteSerasaRepository.GetAllFilterAsync(c => c.DataCriacao >= datalimite && (c.Documento.Equals(sol.Documento) || c.Documento.Equals(docSolicitante)));
                var registro = exist.OrderByDescending(c => c.DataCriacao).FirstOrDefault();

                if (registro != null)
                {
                    // Replica o resultado da consulta para a nova conta-cliente
                    if (registro.ContaClienteID != sol.ContaClienteID)
                    {
                        registro.ID = Guid.NewGuid();
                        registro.ContaCliente = null;
                        registro.ContaClienteID = sol.ContaClienteID;
                        _unitOfWork.SolicitanteSerasaRepository.Insert(registro);
                        _unitOfWork.Commit();
                    }

                    sol.TemPendenciaSerasa = registro.TemPendenciaSerasa;

                    await SaveAlcada(sol, registro.ID, registro.TipoSerasa, registro.Total ?? 0);
                }
                else
                {
                    var solicitanteserasa = new SolicitanteSerasa
                    {
                        ID = Guid.NewGuid(),
                        Documento = sol.Documento,
                        TipoSerasa = sol.TipoSerasa.MapTo<TipoSerasa>(),
                    };

                    DateTime? dataFundacao = null;
                    decimal total = 0;

                    switch (solicitanteserasa.TipoSerasa)
                    {
                        case (TipoSerasa)1: // Concentre
                            ReturnConcentre retornoConcentre;
                            try
                            {
                                retornoConcentre = await SendConcentre(docSolicitante);
                                sol.TemPendenciaSerasa = retornoConcentre.ParticipacaoSocietarias.Any(c => c.RestricaoParticipante);
                                dataFundacao = (sol.OrigemDocumento == OrigemDocumentoDto.ContaCliente ? retornoConcentre.DataConfirmacao : null);
                            }
                            catch (SerasaException ex)
                            {
                                throw new SerasaException(ex.Message);
                            }
                            solicitanteserasa.Json = JsonConvert.SerializeObject(retornoConcentre);
                            solicitanteserasa.TemPendenciaSerasa = sol.TemPendenciaSerasa;
                            total = await SavePendenciasConcentre(solicitanteserasa.MapTo<SolicitanteSerasaDto>(), retornoConcentre);
                            break;
                        case (TipoSerasa)2: // CredNet
                            ReturnCrednet retornoCrednet;
                            try
                            {
                                retornoCrednet = await SendCredNet(docSolicitante);
                                sol.TemPendenciaSerasa = false;
                                dataFundacao = (sol.OrigemDocumento == OrigemDocumentoDto.ContaCliente ? retornoCrednet.Data : null);
                            }
                            catch (SerasaException ex)
                            {
                                throw new SerasaException(ex.Message);
                            }
                            solicitanteserasa.Json = JsonConvert.SerializeObject(retornoCrednet);
                            solicitanteserasa.TemPendenciaSerasa = sol.TemPendenciaSerasa;
                            total = await SavePendenciasCredNet(solicitanteserasa?.MapTo<SolicitanteSerasaDto>(), retornoCrednet);
                            break;
                        case (TipoSerasa)3: // Relato
                            ReturnRelato retornoRelato;
                            try
                            {
                                retornoRelato = await SendSerasaRelato(docSolicitante);
                                sol.TemPendenciaSerasa = retornoRelato.ParticipantesAnotacoes.Any();
                                dataFundacao = (sol.OrigemDocumento == OrigemDocumentoDto.ContaCliente ? retornoRelato.Fundacao : null);
                            }
                            catch (SerasaException ex)
                            {
                                throw new SerasaException(ex.Message);
                            }
                            solicitanteserasa.Json = JsonConvert.SerializeObject(retornoRelato);
                            solicitanteserasa.TemPendenciaSerasa = sol.TemPendenciaSerasa;
                            total = await SavePendenciasRelato(solicitanteserasa?.MapTo<SolicitanteSerasaDto>(), retornoRelato);
                            break;
                        case 0:
                            // throw new ArgumentException($"Este cliente não possuí especificação de Serasa para o Tipo de Cliente{conta.TipoCliente.Nome}");
                            break;
                    }

                    // Procura alguma conta cliente que tenha o documento do solicitante atrelada a ela
                    var contadocumento = await _unitOfWork.ContaClienteRepository.GetAsync(c => (c.Documento.Equals(sol.Documento) || c.Documento.Equals(docSolicitante)));

                    // solicitanteserasa.ID = Guid.NewGuid();
                    solicitanteserasa.DataCriacao = DateTime.Now;
                    solicitanteserasa.UsuarioID = solicitante.UsuarioIDCriacao;
                    solicitanteserasa.UsuarioIDCriacao = solicitante.UsuarioIDCriacao;

                    solicitanteserasa.ContaCliente = null;
                    solicitanteserasa.ContaClienteID = contadocumento?.ID ?? sol.ContaClienteID;

                    solicitanteserasa.TipoCliente = null;
                    solicitanteserasa.TipoClienteID = sol.TipoClienteID; // contadocumento?.TipoClienteID ?? sol.TipoClienteID;

                    solicitanteserasa.TipoSerasa = sol.TipoSerasa.MapTo<TipoSerasa>();
                    solicitanteserasa.Total = total;

                    try
                    {
                        _unitOfWork.SolicitanteSerasaRepository.Insert(solicitanteserasa);
                        _unitOfWork.Commit();

                        await SaveAlcada(sol, solicitanteserasa.ID, solicitanteserasa.TipoSerasa, total);

                        if ((total > 0 || solicitanteserasa.TemPendenciaSerasa || dataFundacao != null))
                            await SaveContaCliente(new ClienteSolicitanteSerasaDto() { ContaClienteID = solicitanteserasa.ContaClienteID, TemPendenciaSerasa = solicitanteserasa.TemPendenciaSerasa, TipoClienteID = solicitanteserasa.TipoClienteID, DataRegistro = dataFundacao, OrigemDocumento = sol.OrigemDocumento }, solicitanteserasa.ID, total, solicitanteserasa.TipoSerasa, EmpresaID);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            var alcadaretorno = await _unitOfWork.PropostaAlcadaComercial.GetAsync(c => c.ID.Equals(Proposta));
            return alcadaretorno.RestricaoSerasa || alcadaretorno.Documentos.Any(c => c.RestricaoSerasa) || alcadaretorno.ParceriasAgricolas.Any(c => c.RestricaoSerasa);
        }
     
        private async Task<bool> SaveAlcada(ClienteSolicitanteSerasaDto obj, Guid SolicitanteID, TipoSerasa tipo, decimal total)
        {
            if (obj.OrigemDocumento.Equals(OrigemDocumentoDto.ContaCliente))
            {
                var alcada = await _unitOfWork.PropostaAlcadaComercial.GetAsync(c => c.ID.Equals(obj.Codigo));
                alcada.RestricaoSerasa = alcada.RestricaoSerasa || total > 0 || obj.TemPendenciaSerasa;
                alcada.SolicitanteSerasaPropostaID = SolicitanteID;
                alcada.TipoSerasaID = tipo;
                _unitOfWork.PropostaAlcadaComercial.Update(alcada);
            }
            else if (obj.OrigemDocumento.Equals(OrigemDocumentoDto.Conjugue))
            {
                var alcada = await _unitOfWork.PropostaAlcadaComercial.GetAsync(c => c.ID.Equals(obj.Codigo));
                alcada.RestricaoSerasa = alcada.RestricaoSerasa || total > 0 || obj.TemPendenciaSerasa;
                alcada.SolicitanteSerasaConjugeID = SolicitanteID;
                alcada.TipoSerasaConjugeID = tipo;
                _unitOfWork.PropostaAlcadaComercial.Update(alcada);
            }
            else if (obj.OrigemDocumento.Equals(OrigemDocumentoDto.Proposta))
            {
                var documento = await _unitOfWork.PropostaAlcadaComercialDocumento.GetAsync(c => c.ID.Equals(obj.Codigo));
                documento.RestricaoSerasa = total > 0 || obj.TemPendenciaSerasa;
                documento.SolicitanteSerasaID = SolicitanteID;
                documento.TipoSerasaID = tipo;
                _unitOfWork.PropostaAlcadaComercialDocumento.Update(documento);
            }
            else if (obj.OrigemDocumento.Equals(OrigemDocumentoDto.Parceiro))
            {
                var parceria = await _unitOfWork.PropostaAlcadaComercialParceriaAgricola.GetAsync(c => c.ID.Equals(obj.Codigo));
                parceria.RestricaoSerasa = total > 0 || obj.TemPendenciaSerasa;
                parceria.SolicitanteSerasaID = SolicitanteID;
                parceria.TipoSerasaID = tipo;
                _unitOfWork.PropostaAlcadaComercialParceriaAgricola.Update(parceria);
            }

            return _unitOfWork.Commit();
        }

        public async Task<dynamic> ConsultarSerasaPropostaLC(SolicitanteSerasaDto solicitante, Guid PropostaLcID, string EmpresaID, string urlSerasa, string usuarioSerasa, string senhaSerasa)
        {
            _urlSerasa = urlSerasa;
            _usuarioSerasa = usuarioSerasa;
            _senhaSerasa = senhaSerasa;

            var propostaLC = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ID.Equals(PropostaLcID));
            var conta = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(propostaLC.ContaClienteID));

            if (conta.TipoCliente == null || conta.TipoClienteID == null || conta.TipoClienteID == Guid.Empty || propostaLC.TipoCliente == null || propostaLC.TipoClienteID == null || propostaLC.TipoClienteID == Guid.Empty)
                throw new ArgumentException("Este cliente não possuí especificação de Tipo de Cliente para realizar uma consulta no Serasa.");
            else
                propostaLC.RestricaoSerasaFlag = false;

            // Antes de consultar Serasa para a proposta, limpa a flag dela para que a nova consulta não utilize a flag já gravada no banco.
            _unitOfWork.PropostaLCRepository.Update(propostaLC);
            _unitOfWork.Commit();

            // Regex para remover pontos, dígitos e barras do número do documento.
            var rgx = new Regex("[\\.\\-\\/]+");

            IList<ClienteSolicitanteSerasaDto> solicitantes = new List<ClienteSolicitanteSerasaDto>();

            // Conta.
            solicitantes.Add(new ClienteSolicitanteSerasaDto
            {
                Codigo = propostaLC.ID,
                ContaClienteID = propostaLC.ContaClienteID,
                Documento = propostaLC.ContaCliente.Documento,
                TipoClienteID = propostaLC.TipoClienteID.Value,
                TipoSerasa = (propostaLC.TipoCliente.TipoSerasa == TipoSerasa.Concentre && rgx.Replace(propostaLC.ContaCliente.Documento.Trim(), "").Length > 11 ? TipoSerasaDto.CredNet : propostaLC.TipoCliente.TipoSerasa.MapTo<TipoSerasaDto>()),
                OrigemDocumento = OrigemDocumentoDto.ContaCliente
            });

            // Cônjugue.
            if (!String.IsNullOrEmpty(propostaLC.CPFConjugue))
            { // Se tiver CPFConjugue, o documento deles(as) sempre deverá ser consultado como Concentre por se tratar de uma PF.
                solicitantes.Add(new ClienteSolicitanteSerasaDto
                {
                    Codigo = propostaLC.ID,
                    ContaClienteID = propostaLC.ContaClienteID,
                    Documento = propostaLC.CPFConjugue,
                    TipoClienteID = propostaLC.TipoClienteID.Value,
                    TipoSerasa = TipoSerasaDto.Concentre,
                    OrigemDocumento = OrigemDocumentoDto.Conjugue
                });
            }

            // CPFs ou CNPJs de outras pessoas / empresas.
            if (!String.IsNullOrEmpty(propostaLC.Documento))
            { // Se tiver Documentos, verificar se é CPF ou CNPJ para buscar Concentre ou CredNet respectivamente.
                var listaDocumentos = propostaLC.Documento.Trim().Split(',').ToList();

                foreach (var documento in listaDocumentos)
                {
                    if (String.IsNullOrEmpty(documento))
                        continue;

                    solicitantes.Add(new ClienteSolicitanteSerasaDto
                    {
                        Codigo = propostaLC.ID,
                        ContaClienteID = propostaLC.ContaClienteID,
                        Documento = documento,
                        TipoClienteID = propostaLC.TipoClienteID.Value,
                        TipoSerasa = (rgx.Replace(documento.Trim(), "").Length > 14 ? TipoSerasaDto.CredNet : TipoSerasaDto.Concentre),
                        OrigemDocumento = OrigemDocumentoDto.Proposta
                    });
                }
            }

            // Parceiros Agrícolas.
            if (propostaLC.ParceriasAgricolas.Any())
            { // Se tiver parceiros, o documento deles sempre deverá ser consultado como Concentre.
                foreach (var parceiroAgricola in propostaLC.ParceriasAgricolas)
                {
                    solicitantes.Add(new ClienteSolicitanteSerasaDto
                    {
                        Codigo = parceiroAgricola.ID,
                        ContaClienteID = propostaLC.ContaClienteID,
                        Documento = parceiroAgricola.Documento,
                        TipoClienteID = propostaLC.TipoClienteID.Value,
                        TipoSerasa = TipoSerasaDto.Concentre,
                        OrigemDocumento = OrigemDocumentoDto.Parceiro
                    });
                }
            }

            // Membros de Grupos Econômicos.
            var membro = await _unitOfWork.GrupoEconomicoMembroReporitory.GetAsync(c => c.ContaClienteID.Equals(propostaLC.ContaClienteID) && c.Ativo && (c.StatusGrupoEconomicoFluxoID == "AP" || c.StatusGrupoEconomicoFluxoID == "PE" || c.StatusGrupoEconomicoFluxoID == "RE"));
            if (membro != null)
            { // Se tiver grupo econômico com membros.
                var buscaGrupoEconomicoDetalhe = await _unitOfWork.GrupoEconomicoMembroReporitory.BuscaContaCliente(membro.GrupoEconomicoID, EmpresaID);
                if (buscaGrupoEconomicoDetalhe != null)
                {
                    var listaMembrosDetalhes = buscaGrupoEconomicoDetalhe.Where(c => !c.ClienteId.Equals(membro.ContaClienteID) && (c.StatusMembro.Contains("AP - ") || c.StatusMembro.Contains("PE - ") || c.StatusMembro.Contains("RE - "))).ToList();
                    foreach (var membroDetalhe in listaMembrosDetalhes)
                    {
                        if (membroDetalhe.ClienteTipoClienteID == null || membroDetalhe.ClienteTipoClienteID == Guid.Empty)
                            throw new ArgumentException($"O cliente {membroDetalhe.ClienteNome} não possuí especificação de Tipo de Cliente para realizar uma consulta no Serasa.");

                        solicitantes.Add(new ClienteSolicitanteSerasaDto
                        {
                            Codigo = propostaLC.ID,
                            ContaClienteID = membroDetalhe.ClienteId,
                            Documento = membroDetalhe.ClienteDocumento,
                            TipoClienteID = membroDetalhe.ClienteTipoClienteID,
                            TipoSerasa = (membroDetalhe.ClienteTipoSerasa == TipoSerasa.Concentre && rgx.Replace(membroDetalhe.ClienteDocumento.Trim(), "").Length > 11 ? TipoSerasaDto.CredNet : membroDetalhe.ClienteTipoSerasa.MapTo<TipoSerasaDto>()),
                            OrigemDocumento = OrigemDocumentoDto.MembroGrupo
                        });
                    }
                }
            }

            var parametro = await _unitOfWork.ParametroSistemaRepository.GetAsync(c => c.Chave == "serasa" && c.EmpresasID == EmpresaID);
            var dias = int.Parse(parametro.Valor);
            var datalimite = DateTime.Now.AddDays(dias * -1);

            foreach (var novoSolicitante in solicitantes)
            {
                // Documento trimado, sem pontos e vírgulas.
                var docSolicitante = rgx.Replace(novoSolicitante.Documento.Trim(), "");

                var exist = await _unitOfWork.SolicitanteSerasaRepository.GetAllFilterAsync(c => c.DataCriacao >= datalimite && (c.Documento.Equals(novoSolicitante.Documento) || c.Documento.Equals(docSolicitante)));
                var registro = exist.OrderByDescending(c => c.DataCriacao).FirstOrDefault();

                if (registro != null)
                {
                    // Replica o resultado da consulta para a nova conta-cliente
                    if (registro.ContaClienteID != novoSolicitante.ContaClienteID)
                    {
                        registro.ID = Guid.NewGuid();
                        registro.ContaCliente = null;
                        registro.ContaClienteID = novoSolicitante.ContaClienteID;
                        _unitOfWork.SolicitanteSerasaRepository.Insert(registro);
                        _unitOfWork.Commit();
                    }

                    novoSolicitante.TemPendenciaSerasa = registro.TemPendenciaSerasa;

                    await SaveLC(novoSolicitante, registro.ID, registro.TipoSerasa.MapTo<TipoSerasa>(), registro.Total ?? 0, EmpresaID);
                }
                else
                {
                    SolicitanteSerasa serasa = new SolicitanteSerasa
                    {
                        ID = Guid.NewGuid(),
                        Documento = novoSolicitante.Documento,
                        TipoSerasa = novoSolicitante.TipoSerasa.MapTo<TipoSerasa>()
                    };

                    DateTime? dataFundacao = null;
                    decimal total = 0;

                    switch (serasa.TipoSerasa)
                    {
                        case (TipoSerasa)1: // Concentre
                            ReturnConcentre retornoConcentre;
                            try
                            {
                                retornoConcentre = await SendConcentre(docSolicitante);
                                novoSolicitante.TemPendenciaSerasa = retornoConcentre.ParticipacaoSocietarias.Any(c => c.RestricaoParticipante);
                                dataFundacao = (novoSolicitante.OrigemDocumento == OrigemDocumentoDto.ContaCliente ? retornoConcentre.DataConfirmacao : null);
                            }
                            catch (SerasaException ex)
                            {
                                throw new SerasaException(ex.Message);
                            }
                            serasa.Json = JsonConvert.SerializeObject(retornoConcentre);
                            serasa.TemPendenciaSerasa = novoSolicitante.TemPendenciaSerasa;
                            total = await SavePendenciasConcentre(serasa?.MapTo<SolicitanteSerasaDto>(), retornoConcentre);
                            break;
                        case (TipoSerasa)2: // CredNet
                            ReturnCrednet retornoCrednet;
                            try
                            {
                                retornoCrednet = await SendCredNet(docSolicitante);
                                novoSolicitante.TemPendenciaSerasa = false;
                                dataFundacao = (novoSolicitante.OrigemDocumento == OrigemDocumentoDto.ContaCliente ? retornoCrednet.Data : null);
                            }
                            catch (SerasaException ex)
                            {
                                throw new SerasaException(ex.Message);
                            }
                            serasa.Json = JsonConvert.SerializeObject(retornoCrednet);
                            serasa.TemPendenciaSerasa = novoSolicitante.TemPendenciaSerasa;
                            total = await SavePendenciasCredNet(serasa?.MapTo<SolicitanteSerasaDto>(), retornoCrednet);
                            break;
                        case (TipoSerasa)3: // Relato
                            ReturnRelato retornoRelato;
                            try
                            {
                                retornoRelato = await SendSerasaRelato(docSolicitante);
                                novoSolicitante.TemPendenciaSerasa = retornoRelato.ParticipantesAnotacoes.Any();
                                dataFundacao = (novoSolicitante.OrigemDocumento == OrigemDocumentoDto.ContaCliente ? retornoRelato.Fundacao : null);
                            }
                            catch (SerasaException ex)
                            {
                                throw new SerasaException(ex.Message);
                            }
                            serasa.Json = JsonConvert.SerializeObject(retornoRelato);
                            serasa.TemPendenciaSerasa = novoSolicitante.TemPendenciaSerasa;
                            total = await SavePendenciasRelato(serasa?.MapTo<SolicitanteSerasaDto>(), retornoRelato);
                            break;
                        case 0:
                            // throw new ArgumentException($"Este cliente não possuí especificação de Serasa para o Tipo de Cliente{conta.TipoCliente.Nome}");
                            break;
                    }

                    // Procura alguma conta cliente que tenha o documento do solicitante atrelada a ela
                    var contadocumento = await _unitOfWork.ContaClienteRepository.GetAsync(c => (c.Documento.Equals(novoSolicitante.Documento) || c.Documento.Equals(docSolicitante)));

                    // serasa.ID = Guid.NewGuid();
                    serasa.DataCriacao = DateTime.Now;
                    serasa.UsuarioID = solicitante.UsuarioIDCriacao;
                    serasa.UsuarioIDCriacao = solicitante.UsuarioIDCriacao;

                    serasa.ContaCliente = null;
                    serasa.ContaClienteID = contadocumento?.ID ?? novoSolicitante.ContaClienteID;

                    serasa.TipoCliente = null;
                    serasa.TipoClienteID = novoSolicitante.TipoClienteID;

                    serasa.TipoSerasa = novoSolicitante.TipoSerasa.MapTo<TipoSerasa>();
                    serasa.Total = total;

                    try
                    {
                        _unitOfWork.SolicitanteSerasaRepository.Insert(serasa);
                        _unitOfWork.Commit();

                        await SaveLC(novoSolicitante, serasa.ID, serasa.TipoSerasa, total, EmpresaID);

                        if ((total > 0 || serasa.TemPendenciaSerasa || dataFundacao != null))
                            await SaveContaCliente(new ClienteSolicitanteSerasaDto() { ContaClienteID = serasa.ContaClienteID, TemPendenciaSerasa = serasa.TemPendenciaSerasa, TipoClienteID = serasa.TipoClienteID, DataRegistro = dataFundacao, OrigemDocumento = novoSolicitante.OrigemDocumento }, serasa.ID, total, serasa.TipoSerasa, EmpresaID);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            var lcretorno = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ID.Equals(PropostaLcID));

            IEnumerable<BuscaGrupoEconomicoMembros> gruporetorno;
            if (membro != null)
            {
                var buscaGrupoEconomicoDetalhe = await _unitOfWork.GrupoEconomicoMembroReporitory.BuscaContaCliente(membro.GrupoEconomicoID, EmpresaID);
                gruporetorno = buscaGrupoEconomicoDetalhe.Where(c => !c.ClienteId.Equals(membro.ContaClienteID) && (c.StatusMembro.Contains("AP - ") || c.StatusMembro.Contains("PE - ") || c.StatusMembro.Contains("RE - "))).ToList();
            }
            else
                gruporetorno = new List<BuscaGrupoEconomicoMembros>();

            return lcretorno.RestricaoSerasaFlag || lcretorno.ParceriasAgricolas.Any(c => c.RestricaoSerasa) || gruporetorno.Any(c => c.RestricaoSerasa);
        }

        private async Task<bool> SaveLC(ClienteSolicitanteSerasaDto obj, Guid SolicitanteID, TipoSerasa tipo, decimal total, string EmpresaID)
        {
            if (obj.OrigemDocumento.Equals(OrigemDocumentoDto.ContaCliente))
            {
                var lc = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ID.Equals(obj.Codigo));
                lc.RestricaoSerasaFlag = lc.RestricaoSerasaFlag || total > 0 || obj.TemPendenciaSerasa;
                lc.SolicitanteSerasaID = SolicitanteID;
                lc.TipoSerasaID = tipo;
                _unitOfWork.PropostaLCRepository.Update(lc);
            }
            else if (obj.OrigemDocumento.Equals(OrigemDocumentoDto.Conjugue))
            {
                var lc = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ID.Equals(obj.Codigo));
                lc.RestricaoSerasaFlag = lc.RestricaoSerasaFlag || total > 0 || obj.TemPendenciaSerasa;
                lc.SolicitanteSerasaConjugeID = SolicitanteID;
                lc.TipoSerasaConjugeID = tipo;
                _unitOfWork.PropostaLCRepository.Update(lc);
            }
            else if (obj.OrigemDocumento.Equals(OrigemDocumentoDto.Proposta))
            {
                var lc = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ID.Equals(obj.Codigo));
                lc.RestricaoSerasaFlag = lc.RestricaoSerasaFlag || total > 0 || obj.TemPendenciaSerasa;
                lc.SolicitanteSerasaID = SolicitanteID;
                lc.TipoSerasaID = tipo;
                _unitOfWork.PropostaLCRepository.Update(lc);
            }
            else if (obj.OrigemDocumento.Equals(OrigemDocumentoDto.Parceiro))
            {
                var parceria = await _unitOfWork.PropostaLCParceriaAgricolaRepository.GetAsync(c => c.ID.Equals(obj.Codigo));
                parceria.RestricaoSerasa = total > 0 || obj.TemPendenciaSerasa;
                parceria.SolicitanteSerasaID = SolicitanteID;
                parceria.TipoSerasaID = tipo;
                _unitOfWork.PropostaLCParceriaAgricolaRepository.Update(parceria);
            }
            else if (obj.OrigemDocumento.Equals(OrigemDocumentoDto.MembroGrupo))
            {
                var lc = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ID.Equals(obj.Codigo));

                var membro = await _unitOfWork.GrupoEconomicoMembroReporitory.GetAsync(c => c.ContaClienteID.Equals(obj.ContaClienteID) && c.Ativo && (c.StatusGrupoEconomicoFluxoID == "AP" || c.StatusGrupoEconomicoFluxoID == "PE" || c.StatusGrupoEconomicoFluxoID == "RE"));
                if (membro != null)
                {
                    var buscaGrupoEconomicoDetalhe = await _unitOfWork.GrupoEconomicoMembroReporitory.BuscaContaCliente(membro.GrupoEconomicoID, EmpresaID);
                    if (buscaGrupoEconomicoDetalhe != null)
                    {
                        var listaMembrosDetalhes = buscaGrupoEconomicoDetalhe.Where(c => !c.ClienteId.Equals(obj.ContaClienteID)).ToList();
                        foreach (var membroDetalhe in listaMembrosDetalhes)
                        {
                            if (membroDetalhe.ClienteId == obj.ContaClienteID)
                            {
                                lc.RestricaoSerasaFlag = lc.RestricaoSerasaFlag || total > 0 || obj.TemPendenciaSerasa;
                                lc.SolicitanteSerasaID = SolicitanteID;
                                lc.TipoSerasaID = tipo;
                                _unitOfWork.PropostaLCRepository.Update(lc);
                            }
                        }
                    }
                }
            }

            return _unitOfWork.Commit();
        }

        public async Task<dynamic> HitoricoDetalhe(Guid ID)
        {
            try
            {
                dynamic retorno = null;
                var solicitante = await _unitOfWork.SolicitanteSerasaRepository.GetAsync(c => c.ID.Equals(ID));

                switch (solicitante.TipoSerasa)
                {
                    case TipoSerasa.Relato:
                        retorno = JsonConvert.DeserializeObject<ReturnRelato>(solicitante.Json);
                        break;
                    case TipoSerasa.Concentre:
                        retorno = JsonConvert.DeserializeObject<ReturnConcentre>(solicitante.Json);
                        break;
                    case TipoSerasa.CredNet:
                        retorno = JsonConvert.DeserializeObject<ReturnCrednet>(solicitante.Json);
                        break;
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SolicitanteSerasaDto> ExistSerasa(Guid ContaClienteID, string EmpresaID)
        {
            var solicitante = new SolicitanteSerasaDto();
            var contacliente = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(ContaClienteID));

            if (contacliente.TipoCliente == null || contacliente.TipoClienteID == null || contacliente.TipoClienteID == Guid.Empty)
                throw new ArgumentException($"O Cliente {contacliente.Nome} não possuí um tipo de cliente especificado.");

            var parametro = await _unitOfWork.ParametroSistemaRepository.GetAsync(c => c.Chave == "serasa" && c.EmpresasID == EmpresaID);

            if (parametro == null)
                return solicitante;

            var dias = int.Parse(parametro.Valor);
            var datalimite = DateTime.Now.AddDays(dias * -1);

            var exist = await _unitOfWork.SolicitanteSerasaRepository.GetAllFilterAsync(c => c.ContaClienteID.Equals(ContaClienteID) && c.DataCriacao >= datalimite && c.Documento.Equals(contacliente.Documento));

            if (exist == null || !exist.Any())
                return solicitante;

            var retorno = exist.OrderByDescending(c => c.DataCriacao).FirstOrDefault();
            solicitante = retorno.MapTo<SolicitanteSerasaDto>();

            return solicitante;
        }

        public async Task<IEnumerable<SolicitanteSerasaDto>> Historico(Guid ContaClienteID)
        {
            var historico = await _unitOfWork.SolicitanteSerasaRepository.GetAllFilterAsync(c => c.ContaClienteID.Equals(ContaClienteID));
            return historico.MapTo<IEnumerable<SolicitanteSerasaDto>>();
        }

        public async Task<ReturnConcentre> PesquisaSerasaConcentre(SolicitanteSerasaDto solicitante)
        {
            var serasa = solicitante.MapTo<SolicitanteSerasa>();
            serasa.DataCriacao = DateTime.Now;
            decimal total = 0;
            ReturnConcentre retorno = null;

            try
            {
                var concentre = new HeaderSerasaConcentre(solicitante.ContaCliente.Documento, _usuarioSerasa, _senhaSerasa);
                var concentreRetorno = await concentre.Send(_urlSerasa);
                retorno = new ReturnConcentreNet().ReturnConcentre(concentreRetorno);
                serasa.Json = JsonConvert.SerializeObject(retorno);
                serasa.ContaCliente = null;
                serasa.TipoCliente = null;
                serasa.TipoClienteID = solicitante.TipoClienteID;
                serasa.Total = total;
                _unitOfWork.Commit();

            }
            catch (Exception e)
            {
                throw e;
            }

            return retorno;
        }

        private async Task<ReturnConcentre> SendConcentre(string documento)
        {
            HeaderSerasaConcentre concentre = new HeaderSerasaConcentre(documento, _usuarioSerasa, _senhaSerasa);
            string retornoCompleto = string.Empty;
            bool continuar = false;

            do
            {
                var retorno = await concentre.Send(_urlSerasa);

                if (string.IsNullOrEmpty(retorno))
                    throw new SerasaException("SERASA: Sem retorno do Serasa.");

                if (retorno.Length < 60)
                    throw new SerasaException("SERASA: Retorno inválido do Serasa.");

                if (retorno.Contains("T999000STRING PARCIAL - HA MAIS REGISTROS A ENVIAR"))
                {
                    retornoCompleto += retorno.Substring(0, retorno.IndexOf("T999"));
                    continuar = true;
                }
                else
                {
                    retornoCompleto += retorno;
                    continuar = false;
                }

                concentre.ChangeHeader(continuar);

                if (retorno.Substring(57, 3) == "ERR")
                {
                    string mensagemErro = string.Empty;
                    var t999pos = retorno.IndexOf("T999");

                    if (t999pos > 0)
                    {
                        t999pos += 8;

                        var t999fim = t999pos + 70;

                        if (retorno.Length < t999fim)
                        {
                            t999fim = retorno.Length - 1;
                        }

                        t999fim = t999fim - t999pos;
                        mensagemErro = retorno.Substring(t999pos, t999fim);
                    }

                    throw new SerasaException(string.Format("SERASA: Retorno de mensagem de erro: {0}", mensagemErro).Trim());
                }
            }
            while(continuar);

            var objectConcentreNet = new ReturnConcentreNet().ReturnConcentre(retornoCompleto);
            return objectConcentreNet;
        }

        private async Task<ReturnCrednet> SendCredNet(string documento)
        {
            var crednet = new HeaderSerasaCrednet(documento, _usuarioSerasa, _senhaSerasa);
            var retorno = await crednet.Send(_urlSerasa);

            // Tratar erro
            if (string.IsNullOrEmpty(retorno))
                throw new SerasaException("SERASA: Sem retorno do Serasa.");

            if (retorno.Length < 60)
                throw new SerasaException("SERASA: Retorno inválido do Serasa.");

            if (retorno.Substring(57, 3) == "ERR")
            {
                string mensagemErro = string.Empty;
                var t999pos = retorno.IndexOf("T999");

                if (t999pos > 0)
                {
                    t999pos += 8;

                    var t999fim = t999pos + 70;

                    if (retorno.Length < t999fim)
                    {
                        t999fim = retorno.Length - 1;
                    }

                    t999fim = t999fim - t999pos;
                    mensagemErro = retorno.Substring(t999pos, t999fim);
                }

                throw new SerasaException(string.Format("SERASA: Retorno de mensagem de erro: {0}", mensagemErro).Trim());
            }

            var objectCredNet = new ReturnCredNet().ReturnCrednet(retorno);
            return objectCredNet;
        }

        private async Task<ReturnRelato> SendSerasaRelato(string documento)
        {
            var relato = new SerasaRelato(documento, _usuarioSerasa, _senhaSerasa);
            var retorno = await relato.Send(_urlSerasa);

            if (string.IsNullOrEmpty(retorno))
                throw new SerasaException("SERASA: Sem retorno do Serasa.");

            if (retorno.Length < 16)
                throw new SerasaException("SERASA: Retorno inválido do Serasa.");

            var posFim = 0;
            posFim = retorno.IndexOf("#FIM");

            if (posFim < 0)
                posFim = 17;

            posFim = posFim - 16;

            if (retorno.Substring(12, 4) == "RTMC")
                throw new SerasaException(string.Format("SERASA: Retorno de mensagem de consistencia: {0}", retorno.Substring(16, posFim)).Trim());

            if (retorno.Substring(12, 4) == "RTME")
                throw new SerasaException(string.Format("SERASA: Retorno de mensagem de erro: {0}", retorno.Substring(16, posFim)).Trim());

            if (retorno.Substring(12, 4) == "RTMI")
                throw new SerasaException(string.Format("SERASA: Retorno de mensagem de informação: {0}", retorno.Substring(16, posFim)).Trim());

            return relato.Serializar(retorno);
        }

        private async Task<decimal> SavePendenciasConcentre(SolicitanteSerasaDto solicitante, ReturnConcentre concentre)
        {
            var pendencia = new List<PendenciasSerasa>();
            decimal total = 0;

            foreach (var acao in concentre.Acoes)
            {
                total += acao.Total;

                pendencia.Add(new PendenciasSerasa()
                {
                    ID = Guid.NewGuid(),
                    Quantidade = 1,
                    Data = acao.DataOcorrencia,
                    Valor = acao.Valor,
                    Modalidade = "Ação judicial",
                    SolicitanteSerasaID = solicitante.ID
                });
            }

            foreach (var protesto in concentre.Protestos)
            {
                total += protesto.Total;
                pendencia.Add(new PendenciasSerasa()
                {
                    ID = Guid.NewGuid(),
                    Quantidade = 1,
                    Data = protesto.DataOcorrencia,
                    Valor = protesto.Valor,
                    Modalidade = $"Protestos {protesto.Natureza}",
                    SolicitanteSerasaID = solicitante.ID
                });
            }

            foreach (var cheque in concentre.ChequesAchei)
            {
                total += cheque.Total;
                pendencia.Add(new PendenciasSerasa()
                {
                    ID = Guid.NewGuid(),
                    Quantidade = 1,
                    Data = cheque.DataOcorrencia,
                    Valor = cheque.Valor,
                    Modalidade = "Cheque sem Fundo",
                    SolicitanteSerasaID = solicitante.ID
                });
            }

            foreach (var cheque in concentre.ChequesSemFundos)
            {
                pendencia.Add(new PendenciasSerasa()
                {
                    ID = Guid.NewGuid(),
                    Quantidade = 1,
                    Data = cheque.DataOcorrencia,
                    Valor = 0,
                    Modalidade = "Cheque CCF",
                    SolicitanteSerasaID = solicitante.ID
                });
            }

            foreach (var falencia in concentre.Falencia)
            {
                pendencia.Add(new PendenciasSerasa()
                {
                    ID = Guid.NewGuid(),
                    Quantidade = 1,
                    Data = falencia.DataOcorrencia,
                    Modalidade = $"Falência {falencia.DescricaoNatureza}",
                    CNPJ = falencia.CnpjFilial.ToString(),
                    Empresa = "",
                    Falencia = true,
                    SolicitanteSerasaID = solicitante.ID
                });
            }

            foreach (var pefin in concentre.Pefin)
            {
                total += pefin.Total;
                pendencia.Add(new PendenciasSerasa()
                {
                    ID = Guid.NewGuid(),
                    Quantidade = 1,
                    Data = pefin.DataOcorrencia,
                    Valor = pefin.ValorOcorrencia,
                    Modalidade = $"Pendência Pefin {pefin.Natureza}",
                    SolicitanteSerasaID = solicitante.ID
                });
            }

            foreach (var refin in concentre.Refin)
            {
                total += refin.ValorOcorrencia;
                pendencia.Add(new PendenciasSerasa()
                {
                    ID = Guid.NewGuid(),
                    Quantidade = 1,
                    Data = refin.DataOcorrencia,
                    Valor = refin.ValorOcorrencia,
                    Modalidade = $"Pendência Refin {refin.Natureza}",
                    SolicitanteSerasaID = solicitante.ID
                });
            }

            foreach (var pend in pendencia)
                _unitOfWork.PendenciaSerasaRepository.Insert(pend);

            return total;
        }

        private async Task<decimal> SavePendenciasCredNet(SolicitanteSerasaDto solicitante, ReturnCrednet credNet)
        {
            var pendencia = new List<PendenciasSerasa>();
            decimal total = 0;
            foreach (var item in credNet.ProtestoEstaduais)
            {
                total += item.Total;
                pendencia.Add(new PendenciasSerasa()
                {
                    ID = Guid.NewGuid(),
                    Quantidade = 1,
                    Data = item.DataOcorrencia,
                    Valor = item.Valor,
                    Modalidade = "Protestos do Estadual",
                    SolicitanteSerasaID = solicitante.ID
                });
            }

            foreach (var item in credNet.PendenciasInternasDetalhes)
            {
                total += item.Total;
                pendencia.Add(new PendenciasSerasa()
                {
                    ID = Guid.NewGuid(),
                    Quantidade = 1,
                    Data = item.DataOcorrencia,
                    Valor = item.Valor,
                    Modalidade = "Pendencias Internas",
                    SolicitanteSerasaID = solicitante.ID
                });
            }

            foreach (var item in credNet.ChequeBacen)
            {
                total += item.Total;
                pendencia.Add(new PendenciasSerasa()
                {
                    ID = Guid.NewGuid(),
                    Quantidade = 1,
                    Data = item.DataOcorrencia,
                    Valor = item.Valor,
                    Modalidade = "Cheques Sem Fundo BACEN",
                    SolicitanteSerasaID = solicitante.ID
                });
            }

            foreach (var item in credNet.PendenciasFinanceiras)
            {
                total += item.Total;
                pendencia.Add(new PendenciasSerasa()
                {
                    ID = Guid.NewGuid(),
                    Quantidade = 1,
                    Data = item.DataOcorrencia,
                    Valor = item.Valor,
                    Modalidade = "Pendencias Financeiras",
                    SolicitanteSerasaID = solicitante.ID
                });
            }

            foreach (var pend in pendencia)
                _unitOfWork.PendenciaSerasaRepository.Insert(pend);

            return total;
        }

        private async Task<decimal> SavePendenciasRelato(SolicitanteSerasaDto solicitante, ReturnRelato relato)
        {
            var pendencia = new List<PendenciasSerasa>();
            decimal total = 0;

            if (relato.DividasVencidas != null && relato.DividasVencidas.Any())
            {
                total += relato.DividasVencidas[0].Total;
                foreach (var divida in relato.DividasVencidas)
                {
                    pendencia.Add(new PendenciasSerasa()
                    {
                        ID = Guid.NewGuid(),
                        Quantidade = divida.Quantidade,
                        Data = divida.Data,
                        Valor = divida.Valor,
                        Modalidade = $"Divida {divida.Titulo}",
                        SolicitanteSerasaID = solicitante.ID
                    });
                }
            }

            if (relato.AcoesJudiciais != null && relato.AcoesJudiciais.Any())
            {
                total += relato.AcoesJudiciais[0].Total;
                foreach (var acao in relato.AcoesJudiciais)
                {
                    pendencia.Add(new PendenciasSerasa()
                    {
                        ID = Guid.NewGuid(),
                        Quantidade = acao.Quantidade,
                        Data = acao.Data,
                        Valor = acao.Valor,
                        Modalidade = "Ação judicial",
                        SolicitanteSerasaID = solicitante.ID
                    });
                }
            }

            if (relato.ProtestosConcentre != null && relato.ProtestosConcentre.Any())
            {
                total += relato.ProtestosConcentre[0].Total;
                foreach (var protesto in relato.ProtestosConcentre)
                {
                    pendencia.Add(new PendenciasSerasa()
                    {
                        ID = Guid.NewGuid(),
                        Quantidade = protesto.Quantidade,
                        Data = protesto.Data,
                        Valor = protesto.Valor,
                        Modalidade = $"Protestos {protesto.Mensagem}",
                        SolicitanteSerasaID = solicitante.ID
                    });
                }
            }

            foreach (var cheque in relato.ChequesSemFundo)
            {
                total += cheque.Total;
                pendencia.Add(new PendenciasSerasa()
                {
                    ID = Guid.NewGuid(),
                    Quantidade = cheque.Quantidade,
                    Data = cheque.Data,
                    Valor = cheque.Valor,
                    Modalidade = "Cheque sem Fundo",
                    SolicitanteSerasaID = solicitante.ID
                });
            }

            foreach (var falencia in relato.FalenciaConcordatas)
            {
                pendencia.Add(new PendenciasSerasa()
                {
                    ID = Guid.NewGuid(),
                    Quantidade = falencia.Quantidade,
                    Modalidade = "Falência " + falencia.Natureza,
                    CNPJ = "",
                    Data = falencia.Data,
                    Empresa = falencia.Origem,
                    Falencia = true,
                    SolicitanteSerasaID = solicitante.ID
                });
            }

            foreach (var cheque in relato.ChequesCCF)
            {
                pendencia.Add(new PendenciasSerasa()
                {
                    ID = Guid.NewGuid(),
                    Quantidade = cheque.Quantidade,
                    Data = cheque.Data,
                    Valor = 0,
                    Modalidade = "Cheque CCF",
                    SolicitanteSerasaID = solicitante.ID
                });
            }

            foreach (var falencia in relato.ParticipacoesFalencia)
            {
                pendencia.Add(new PendenciasSerasa()
                {
                    ID = Guid.NewGuid(),
                    Quantidade = falencia.Quantidade,
                    Modalidade = "Falência",
                    CNPJ = falencia.CNPJ,
                    Empresa = falencia.Empresa,
                    Falencia = true,
                    SolicitanteSerasaID = solicitante.ID
                });
            }

            foreach (var anotacao in relato.ParticipantesAnotacoes)
            {
                pendencia.Add(new PendenciasSerasa()
                {
                    ID = Guid.NewGuid(),
                    Quantidade = 1,
                    Modalidade = "Anotações dos Participantes",
                    CNPJ = anotacao.Documento,
                    Empresa = anotacao.Nome,
                    SolicitanteSerasaID = solicitante.ID
                });
            }

            if (relato.Pefin != null)
            {
                total += relato.Pefin.Total;
                foreach (var pefin in relato.Pefin.PendenciaPefin)
                {
                    pendencia.Add(new PendenciasSerasa()
                    {
                        ID = Guid.NewGuid(),
                        Quantidade = pefin.Quantidade,
                        Data = pefin.DataUltimaOcorrencia,
                        Valor = pefin.Valor,
                        Modalidade = $"Pendência Pefin {pefin.Origem}",
                        SolicitanteSerasaID = solicitante.ID
                    });
                }
            }

            if (relato.Refin != null)
            {
                total += relato.Refin.Total;
                foreach (var refin in relato.Refin.PendenciaRefin)
                {
                    pendencia.Add(new PendenciasSerasa()
                    {
                        ID = Guid.NewGuid(),
                        Quantidade = refin.Quantidade,
                        Data = refin.DataUltimaOcorrencia,
                        Valor = refin.Valor,
                        Modalidade = $"Pendência Refin {refin.Origem}",
                        SolicitanteSerasaID = solicitante.ID
                    });
                }
            }

            foreach (var pend in pendencia)
                _unitOfWork.PendenciaSerasaRepository.Insert(pend);

            return total;
        }

        public async Task<bool> AlterarStatusSerasa(Guid contaClienteID, string empresaID, int tipoRestricao)
        {
            var contaCliente = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(contaClienteID));
            if (contaCliente.TipoCliente == null)
                throw new ArgumentException($"O Cliente {contaCliente.Nome} não possuí um tipo de cliente especificado.");

            var parametro = await _unitOfWork.ParametroSistemaRepository.GetAsync(c => c.Chave == "serasa" && c.EmpresasID == empresaID);
            if (parametro == null)
                return false;

            var enumTipoRestricao = (RestricaoSerasaDto)tipoRestricao;
            contaCliente.DataAlteracao = DateTime.Now;
            contaCliente.UsuarioIDAlteracao = contaCliente.UsuarioIDCriacao;

            contaCliente.RestricaoSerasa = (enumTipoRestricao == RestricaoSerasaDto.RestricaoRelevante ? true : false);
            contaCliente.PendenciaSerasa = enumTipoRestricao.MapTo<RestricaoSerasa>();

            _unitOfWork.ContaClienteRepository.Update(contaCliente);

            var processamentocarteira = new ProcessamentoCarteira
            {
                Cliente = contaCliente.CodigoPrincipal,
                DataHora = DateTime.Now,
                Motivo = $"Alteração manual de Serasa na conta cliente {contaCliente.Nome}",
                Status = 2,
                Detalhes = "Consulta Serasa",
                EmpresaID = empresaID,
                ID = Guid.NewGuid()
            };

            _unitOfWork.ProcessamentoCarteiraRepository.Insert(processamentocarteira);

            return _unitOfWork.Commit();
        }
    }
}

