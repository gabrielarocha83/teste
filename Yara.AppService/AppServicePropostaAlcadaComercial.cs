using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.AppService
{
    public class AppServicePropostaAlcadaComercial : IAppServicePropostaAlcadaComercial
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServicePropostaAlcadaComercial(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PropostaAlcadaComercialDto> InsertAsync(PropostaAlcadaComercialDto obj)
        {
            try
            {
                var conta = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(obj.ContaClienteID));

                var alcada = new PropostaAlcadaComercial
                {
                    ID = obj.ID,
                    ContaClienteID = obj.ContaClienteID,
                    UsuarioIDCriacao = obj.UsuarioIDCriacao,
                    ResponsavelID = obj.UsuarioIDCriacao,
                    TipoClienteID = conta?.TipoClienteID ?? null,
                    DataFundacao = conta?.DataNascimentoFundacao ?? null,
                    DataCriacao = DateTime.Now,
                    EmpresaID = obj.EmpresaID,
                    PropostaCobrancaStatusID = "EC",
                    NumeroInternoProposta = _unitOfWork.PropostaAlcadaComercial.GetMaxNumeroInterno()
                };

                var dadosalcada = await _unitOfWork.PropostaAlcadaComercial.GetAllFilterAsync(c => c.ContaClienteID.Equals(obj.ContaClienteID));

                var ultimaalcada = dadosalcada.OrderByDescending(c => c.DataCriacao).FirstOrDefault();

                if (ultimaalcada != null)
                {
                    alcada.TipoClienteID = ultimaalcada.TipoClienteID;

                    foreach (var parceria in ultimaalcada.ParceriasAgricolas)
                    {
                        alcada.ParceriasAgricolas.Add(new PropostaAlcadaComercialParceriaAgricola()
                        {
                            ID = Guid.NewGuid(),
                            Nome = parceria.Nome,
                            Documento = parceria.Documento,
                            InscricaoEstadual = parceria.InscricaoEstadual,
                            PropostaAlcadaComercialID = obj.ID
                        });
                    }

                    alcada.EstadoCivil = ultimaalcada.EstadoCivil;
                    alcada.CPFConjugue = ultimaalcada.CPFConjugue;
                    alcada.NomeConjugue = ultimaalcada.NomeConjugue;
                    alcada.RegimeCasamento = ultimaalcada.RegimeCasamento;
                    alcada.ExperienciaID = ultimaalcada.ExperienciaID;

                    foreach (var documento in ultimaalcada.Documentos)
                    {
                        alcada.Documentos.Add(new PropostaAlcadaComercialDocumentos()
                        {
                            ID = Guid.NewGuid(),
                            Documento = documento.Documento,
                            //RestricaoSerasa = documento.RestricaoSerasa,
                            PropostaAlcadaComercialID = obj.ID
                        });
                    }
                }

                _unitOfWork.PropostaAlcadaComercial.Insert(alcada);
                _unitOfWork.Commit();

                var alcadas = await _unitOfWork.PropostaAlcadaComercial.GetAsync(c => c.ID.Equals(obj.ID));
                return alcadas.MapTo<PropostaAlcadaComercialDto>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PropostaAlcadaComercialDto> GetAsync(Guid proposal, Guid user)
        {
            var alcadas = await _unitOfWork.PropostaAlcadaComercial.GetAsync(c => c.ID.Equals(proposal));
            var retornoalcada = alcadas.MapTo<PropostaAlcadaComercialDto>();
            retornoalcada.Acompanhar = (await _unitOfWork.PropostaAlcadaComercialAcompanhamentoRepository.GetAsync(c => c.PropostaAlcadaComercialID.Equals(proposal) && c.UsuarioID.Equals(user)))?.Ativo ?? false;

            return retornoalcada;
        }

        public async Task<bool> UdpateAsync(PropostaAlcadaComercialDto proposta)
        {
            try
            {
                var alcada = await _unitOfWork.PropostaAlcadaComercial.GetAsync(c => c.ID.Equals(proposta.ID) && c.EmpresaID.Equals(proposta.EmpresaID));
                alcada.TipoClienteID = proposta.TipoClienteID;
                alcada.ExperienciaID = proposta.ExperienciaID;
                alcada.EstadoCivil = proposta.EstadoCivil;
                alcada.CPFConjugue = proposta.CPFConjugue;
                alcada.NomeConjugue = proposta.NomeConjugue;
                alcada.RegimeCasamento = proposta.RegimeCasamento;
                alcada.LCProposto = proposta.LCProposto;
                alcada.SharePretendido = proposta.SharePretendido;
                alcada.PrazoDias = proposta.PrazoDias;
                alcada.FontePagamento = proposta.FontePagamento;
                alcada.ParecerRepresentante = proposta.ParecerRepresentante;
                alcada.ParecerCTC = proposta.ParecerCTC;
                alcada.ParecerCredito = proposta.ParecerCredito;
                alcada.PorteCliente = proposta.PorteCliente;
                alcada.DataFundacao = proposta.DataFundacao;
                alcada.TipoGarantiaID = proposta.TipoGarantiaaID;
                alcada.FaturamentoAnual = proposta.FaturamentoAnual;

                if (alcada.PropostaCobrancaStatusID == "EC")
                    alcada.CodigoSap = proposta.CodigoSap;

                alcada.ResponsavelID = proposta.ResponsavelID;
                alcada.TermoAceite = proposta.TermoAceite;
                alcada.DataAlteracao = DateTime.Now;
                alcada.UsuarioIDAlteracao = proposta.UsuarioIDAlteracao;

                // Collection Properties
                #region Parcerias Agrícolas

                var idsParcerias = proposta.ParceriasAgricolas.Where(pa => pa.ID != Guid.Empty).Select(pa => pa.ID).ToList();
                var removeParcerias = alcada.ParceriasAgricolas.Where(pa => !idsParcerias.Contains(pa.ID)).ToList();
                PropostaAlcadaComercialParceriaAgricola paAtual = null;

                foreach (var rpa in removeParcerias)
                    _unitOfWork.PropostaAlcadaComercialParceriaAgricola.Delete(rpa);

                foreach (var pa in proposta.ParceriasAgricolas)
                {
                    paAtual = alcada.ParceriasAgricolas.FirstOrDefault(p => p.ID.Equals(pa.ID));

                    if (paAtual == null)
                    {              
                        alcada.ParceriasAgricolas.Add(new PropostaAlcadaComercialParceriaAgricola {
                            ID = Guid.NewGuid(),
                            PropostaAlcadaComercialID = proposta.ID,
                            InscricaoEstadual = pa.InscricaoEstadual,
                            Nome = pa.Nome,
                            Documento = pa.Documento
                        });
                    }
                    else
                    {
                        paAtual.InscricaoEstadual = pa.InscricaoEstadual;
                        paAtual.Nome = pa.Nome;
                        paAtual.Documento = pa.Documento;
                        paAtual.RestricaoSerasa = pa.RestricaoSerasa;
                        _unitOfWork.PropostaAlcadaComercialParceriaAgricola.Update(paAtual);
                    }
                }

                #endregion

                #region Culturas

                var idsCulturas = proposta.Culturas.Where(c => c.ID != Guid.Empty).Select(c => c.ID).ToList();
                var removeCulturas = alcada.Culturas.Where(c => !idsCulturas.Contains(c.ID)).ToList();
                PropostaAlcadaComercialCultura cAtual = null;

                foreach (var rc in removeCulturas)
                    _unitOfWork.PropostaAlcadaComercialCultura.Delete(rc);

                foreach (var ca in proposta.Culturas)
                {
                    cAtual = alcada.Culturas.FirstOrDefault(c => c.ID.Equals(ca.ID));

                    if (cAtual == null)
                    {
                        alcada.Culturas.Add(new PropostaAlcadaComercialCultura()
                        {
                            ID = Guid.NewGuid(),
                            PropostaAlcadaComercialID = proposta.ID,
                            AreaArrendada = ca.AreaArrendada,
                            AreaPropria = ca.AreaPropria,
                            CidadeID = ca.CidadeID,
                            Documento = ca.Documento,
                            EstadoID = ca.EstadoID,
                            CulturaID = ca.CulturaID
                        });
                    }
                    else
                    {
                        cAtual.AreaArrendada = ca.AreaArrendada;
                        cAtual.AreaPropria = ca.AreaPropria;
                        cAtual.CidadeID = ca.CidadeID;
                        cAtual.Documento = ca.Documento;
                        cAtual.EstadoID = ca.EstadoID;
                        cAtual.CulturaID = ca.CulturaID;
                        _unitOfWork.PropostaAlcadaComercialCultura.Update(cAtual);
                    }
                }

                #endregion

                #region Produtos e Serviços

                var idsProdutos = proposta.Produtos.Where(ps => ps.ID != Guid.Empty).Select(ps => ps.ID).ToList();
                var removeProdutos = alcada.Produtos.Where(ps => !idsProdutos.Contains(ps.ID)).ToList();
                PropostaAlcadaComercialProdutoServico psAtual = null;

                foreach (var rps in removeProdutos)
                    _unitOfWork.PropostaAlcadaComercialProdutoServico.Delete(rps);

                foreach (var ps in proposta.Produtos)
                {
                    psAtual = alcada.Produtos.FirstOrDefault(p => p.ID.Equals(ps.ID));

                    if (psAtual == null)
                    {
                        alcada.Produtos.Add(new PropostaAlcadaComercialProdutoServico
                        {
                            ID = Guid.NewGuid(),
                            PropostaAlcadaComercialID = proposta.ID,
                            ProdutoServicoID = ps.ProdutoServicoID
                        });
                    }
                    else
                    {
                        psAtual.ProdutoServicoID = ps.ProdutoServicoID;
                        _unitOfWork.PropostaAlcadaComercialProdutoServico.Update(psAtual);
                    }
                }

                #endregion

                #region Documentos

                var idsDocumentos = proposta.Documentos.Where(doc => doc.ID != Guid.Empty).Select(doc => doc.ID).ToList();
                var removeDocumentos = alcada.Documentos.Where(doc => !idsDocumentos.Contains(doc.ID)).ToList();
                PropostaAlcadaComercialDocumentos docAtual = null;

                foreach (var rdoc in removeDocumentos)
                    _unitOfWork.PropostaAlcadaComercialDocumento.Delete(rdoc);

                foreach (var doc in proposta.Documentos)
                {
                    docAtual = alcada.Documentos.FirstOrDefault(p => p.ID.Equals(doc.ID));

                    if (docAtual == null)
                    {
                        alcada.Documentos.Add(new PropostaAlcadaComercialDocumentos {
                            ID = Guid.NewGuid(),
                            PropostaAlcadaComercialID = proposta.ID,
                            Documento = doc.Documento
                        });
                    }
                    else
                    {
                        docAtual.Documento = doc.Documento;
                        docAtual.RestricaoSerasa = doc.RestricaoSerasa;
                        _unitOfWork.PropostaAlcadaComercialDocumento.Update(docAtual);
                    }
                }

                #endregion

                #region Salvar Acompanhamento

                var acompanha = await _unitOfWork.PropostaAlcadaComercialAcompanhamentoRepository.GetAsync(c => c.PropostaAlcadaComercialID.Equals(proposta.ID) && c.UsuarioID.Equals(proposta.UsuarioIDAlteracao.Value));

                if (acompanha != null)
                {
                    acompanha.Ativo = proposta.Acompanhar;
                    _unitOfWork.PropostaAlcadaComercialAcompanhamentoRepository.Update(acompanha);
                }
                else
                {
                    _unitOfWork.PropostaAlcadaComercialAcompanhamentoRepository.Insert(new PropostaAlcadaComercialAcompanhamento()
                    {
                        DataCriacao = DateTime.Now,
                        UsuarioID = proposta.UsuarioIDAlteracao.Value,
                        PropostaAlcadaComercialID = proposta.ID,
                        Ativo = true
                    });
                }

                #endregion

                _unitOfWork.PropostaAlcadaComercial.Update(alcada);

                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task SendEmail(Guid PropostaID, Guid Usuario, string Mensagem, string URL)
        {
            try
            {
                var proposta = await _unitOfWork.PropostaAlcadaComercial.GetAsync(c => c.ID.Equals(PropostaID));
                var user = await _unitOfWork.UsuarioRepository.GetAsync(c => c.ID.Equals(Usuario));
                var email = new AppServiceEnvioEmail(_unitOfWork);
                await email.SendMailFeedBackPropostas(user.MapTo<UsuarioDto>(), PropostaID, Mensagem, proposta.ContaClienteID, URL);
            }
            catch
            {

            }
        }

        public async Task<IEnumerable<PropostaAlcadaComercialRestricoesDto>> BuscaRestricaoAlcada(Guid contaClienteId, string empresaId)
        {
            var alcada = await _unitOfWork.PropostaAlcadaComercial.BuscaRestricaoAlcada(contaClienteId, empresaId);
            return Mapper.Map<IEnumerable<PropostaAlcadaComercialRestricoesDto>>(alcada);
        }

        public async Task<bool> CancelAsync(Guid propostaId, Guid UsuarioID, string URL)
        {
            try
            {
                var proposta = await _unitOfWork.PropostaAlcadaComercial.GetAsync(c => c.ID.Equals(propostaId));
                proposta.PropostaCobrancaStatusID = "EN";
                proposta.UsuarioIDAlteracao = UsuarioID;
                proposta.DataAlteracao = DateTime.Now;
                proposta.ResponsavelID = UsuarioID;

                _unitOfWork.PropostaAlcadaComercial.Update(proposta);

                if (proposta.UsuarioIDCriacao != UsuarioID)
                    await SendEmail(proposta.ID, proposta.UsuarioIDCriacao, "Proposta de alçada comercial encerrada.", URL);

                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> UpdateOwner(PropostaAlcadaComercialDto proposta)
        {
            try
            {
                var alcada = await _unitOfWork.PropostaAlcadaComercial.GetAsync(c => c.ID.Equals(proposta.ID) && c.EmpresaID.Equals(proposta.EmpresaID));
                alcada.ResponsavelID = proposta.ResponsavelID;
                alcada.DataAlteracao = DateTime.Now;
                alcada.UsuarioIDAlteracao = proposta.UsuarioIDAlteracao;

                _unitOfWork.PropostaAlcadaComercial.Update(alcada);

                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> SendCtc(PropostaAlcadaComercialDto proposta, string URL)
        {
            try
            {
                var alcada = await _unitOfWork.PropostaAlcadaComercial.GetAsync(c => c.ID.Equals(proposta.ID) && c.EmpresaID.Equals(proposta.EmpresaID));
                alcada.ResponsavelID = proposta.ResponsavelID;
                alcada.DataAlteracao = DateTime.Now;
                alcada.CodigoSap = proposta.CodigoSap;
                alcada.PropostaCobrancaStatusID = "ET";
                alcada.UsuarioIDAlteracao = proposta.UsuarioIDAlteracao;

                _unitOfWork.PropostaAlcadaComercial.Update(alcada);

                await SendEmail(alcada.ID, proposta.UsuarioIDAlteracao.Value, "Proposta enviada para sua atuação, veja maiores informações em seu cockpit.", URL);

                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> SendAnalysis(PropostaAlcadaComercialDto proposta, string URL)
        {
            try
            {
                var alcada = await _unitOfWork.PropostaAlcadaComercial.GetAsync(c => c.ID.Equals(proposta.ID) && c.EmpresaID.Equals(proposta.EmpresaID));
                alcada.ResponsavelID = proposta.ResponsavelID;
                alcada.DataAlteracao = DateTime.Now;
                alcada.CodigoSap = proposta.CodigoSap;
                alcada.PropostaCobrancaStatusID = "EA";
                alcada.UsuarioIDAlteracao = proposta.UsuarioIDAlteracao;

                _unitOfWork.PropostaAlcadaComercial.Update(alcada);

                await SendEmail(alcada.ID, proposta.UsuarioIDAlteracao.Value, "Proposta enviada para sua atuação, veja maiores informações em seu cockpit.", URL);

                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> Disapprove(PropostaAlcadaComercialDto proposta, string URL)
        {
            try
            {
                var alcada = await _unitOfWork.PropostaAlcadaComercial.GetAsync(c => c.ID.Equals(proposta.ID));
                alcada.PropostaCobrancaStatusID = "RE";
                alcada.UsuarioIDAlteracao = proposta.UsuarioIDAlteracao;
                alcada.DataAlteracao = DateTime.Now;
                alcada.Comentario = proposta.Comentario;
                alcada.ResponsavelID = proposta.UsuarioIDAlteracao.Value;

                _unitOfWork.PropostaAlcadaComercial.Update(alcada);

                await SendEmail(proposta.ID, proposta.UsuarioIDCriacao, "Proposta de alçada comercial rejeitada.", URL);

                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> AprovaAlcadaComercial(PropostaAlcadaComercialDto alcadaComercial, string URL)
        {
            var proposta = await _unitOfWork.PropostaAlcadaComercial.GetAsync(c => c.ID.Equals(alcadaComercial.ID));

            var rest = await this.BuscaRestricaoAnoNascimentoFundacao(proposta.ID, proposta.EmpresaID);
            if (rest != null)
            {
                var isCanceled = await this.CancelAsync(proposta.ID, alcadaComercial.UsuarioIDCriacao, URL);
                if (isCanceled)
                {
                    var logService = new AppServiceLog(_unitOfWork);
                    var log = new LogDto
                    {
                        ID = Guid.NewGuid(),
                        IDTransacao = proposta.ID,
                        Descricao = $"Proposta de Alçada Comercial cancelada pelo usuário: Sistema. Motivo: {rest.Mensagem}",
                        LogLevelID = 10,
                        Usuario = "Sistema",
                        DataCriacao = DateTime.Now,
                        UsuarioID = Guid.Parse("00000000-0000-0000-0000-000000000001")
                    };
                    logService.Create(log);
                }

                var epuService = new AppServiceEstruturaPerfilUsuario(_unitOfWork);
                var repre = await epuService.GetActiveProfileAlcadaByCustomer(proposta.ContaClienteID, alcadaComercial.UsuarioIDCriacao, proposta.EmpresaID);
                if (repre == "Representante" || repre == "Consultor Técnico Comercial")
                {
                    throw new ArgumentException("Cliente apresenta restrições que impossibilitam a criação da proposta - Proposta Encerrada. Em caso de dúvidas, contatar o Time de Crédito.");
                }
                else
                {
                    throw new ArgumentException(rest.Mensagem);
                }
            }

            if (proposta != null)
            {
                proposta.PropostaCobrancaStatusID = "AP";
                proposta.Comentario = alcadaComercial.Comentario;
                proposta.UsuarioIDAlteracao = alcadaComercial.UsuarioIDCriacao;
                proposta.DataAlteracao = DateTime.Now;
                _unitOfWork.PropostaAlcadaComercial.Update(proposta);
            }

            var financeiro = await _unitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.ContaClienteID.Equals(proposta.ContaClienteID) && c.EmpresasID.Equals(alcadaComercial.EmpresaID));
            if (financeiro != null)
            {
                financeiro.Rating = "G";
                financeiro.LC = proposta.LCProposto;
                financeiro.Vigencia = DateTime.Now;
                financeiro.VigenciaFim = DateTime.Now.AddYears(1);
                financeiro.UsuarioIDAlteracao = alcadaComercial.UsuarioIDCriacao;
                financeiro.DataAlteracao = DateTime.Now;

                _unitOfWork.ContaClienteFinanceiroRepository.UpdateConceito(financeiro);

                await AddProcessamentoCarteira(financeiro.MapTo<ContaClienteFinanceiroDto>());
            }
            else
            {
                financeiro.ContaClienteID = proposta.ContaClienteID;
                financeiro.EmpresasID = alcadaComercial.EmpresaID;

                financeiro.Rating = "G";
                financeiro.LC = proposta.LCProposto;
                financeiro.Vigencia = DateTime.Now;
                financeiro.VigenciaFim = DateTime.Now.AddYears(1);
                financeiro.UsuarioIDCriacao = alcadaComercial.UsuarioIDCriacao;
                financeiro.DataCriacao = DateTime.Now;

                _unitOfWork.ContaClienteFinanceiroRepository.Insert(financeiro);

                await AddProcessamentoCarteira(financeiro.MapTo<ContaClienteFinanceiroDto>());
            }

            var commit = _unitOfWork.Commit();

            if (commit)
            {
                try
                {
                    var rfc = new AppServiceRFCSap(_unitOfWork);
                    await rfc.EnviarFixacaoLimiteAlcada(proposta.ID);
                }
                catch (Exception e)
                {
                    throw e;
                }

                if (!proposta.ResponsavelID.Equals(alcadaComercial.UsuarioIDCriacao))
                {
                    try
                    {
                        await SendEmail(alcadaComercial.ID, alcadaComercial.UsuarioIDCriacao, "Proposta de alçada comercial aprovada.", URL);
                    }
                    catch
                    {
                      
                    }
                }
            }

            return commit;
        }

        private async Task<PropostaAlcadaComercialRestricoesDto> BuscaRestricaoAnoNascimentoFundacao(Guid PropostaID, string empresaId)
        {
            PropostaAlcadaComercialRestricoesDto retorno = null;

            try
            {
                var propostaAlcada = await _unitOfWork.PropostaAlcadaComercial.GetAsync(c => c.ID == PropostaID);

                if (propostaAlcada != null)
                {
                    var contaCliente = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID == propostaAlcada.ContaClienteID);

                    if (contaCliente.DataNascimentoFundacao.HasValue && contaCliente.DataNascimentoFundacao.Value != DateTime.MinValue)
                    {
                        var diferenca = DateTime.MinValue + DateTime.Now.Subtract(contaCliente.DataNascimentoFundacao.Value);
                        int years = diferenca.Year - 1;

                        if (contaCliente.Documento.Length == 11)
                        {
                            if (years < 25 || years > 85)
                            {
                                retorno = new PropostaAlcadaComercialRestricoesDto
                                {
                                    ID = propostaAlcada.ID,
                                    ContaClienteID = contaCliente.ID,
                                    Mensagem = "Cliente com idade inferior a 25 anos ou superior a 85 anos - Proposta Encerrada.",
                                    CodigoSap = propostaAlcada.CodigoSap
                                };
                            }
                        }
                        else
                        {
                            if (years < 2)
                            {
                                retorno = new PropostaAlcadaComercialRestricoesDto
                                {
                                    ID = propostaAlcada.ID,
                                    ContaClienteID = contaCliente.ID,
                                    Mensagem = "Cliente Pessoa Jurídica com data de Fundação inferior a 2 anos - Proposta Encerrada.",
                                    CodigoSap = propostaAlcada.CodigoSap
                                };
                            }
                        }
                    }
                    else
                    {
                        retorno = new PropostaAlcadaComercialRestricoesDto
                        {
                            ID = propostaAlcada.ID,
                            ContaClienteID = contaCliente.ID,
                            Mensagem = "Cliente sem Ano Nascimento / Fundaçao definido na Conta Cliente - Proposta Encerrada.",
                            CodigoSap = propostaAlcada.CodigoSap
                        };
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return retorno;
        }

        private async Task AddProcessamentoCarteira(ContaClienteFinanceiroDto clienteFinanceiroDto)
        {
            var contaCliente = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID == clienteFinanceiroDto.ContaClienteID);
            var carteiraDto = new ProcessamentoCarteiraDto();
            carteiraDto.ID = Guid.NewGuid();
            carteiraDto.Cliente = contaCliente.CodigoPrincipal;
            carteiraDto.DataHora = DateTime.Now;
            carteiraDto.Status = 2;
            carteiraDto.Motivo = "AC - Fixou Limite de Crédito para o Cliente: " + contaCliente.Nome;
            carteiraDto.Detalhes = "AC - O Limite de Crédito fixado foi no valor de: " + clienteFinanceiroDto.LC;
            carteiraDto.EmpresaID = clienteFinanceiroDto.EmpresasID;
            _unitOfWork.ProcessamentoCarteiraRepository.Insert(carteiraDto.MapTo<ProcessamentoCarteira>());
        }
    }
}