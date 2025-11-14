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
    public class AppServicePropostaProrrogacao : IAppServicePropostaProrrogacao
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServicePropostaProrrogacao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private async Task SaveFollow(PropostaProrrogacaoDto obj)
        {
            var acompanha = await
                _unitOfWork.PropostaProrrogacaoAcompanhamento.GetAsync(c => c.PropostaProrrogacaoID.Equals(obj.ID) &&
                                                                      c.UsuarioID.Equals(obj.UsuarioIDAlteracao.Value));
            if (acompanha != null)
            {
                acompanha.Ativo = obj.Acompanhar;
                _unitOfWork.PropostaProrrogacaoAcompanhamento.Update(acompanha);
            }
            else
            {
                var acompanhar = new PropostaProrrogacaoAcompanhamento()
                {
                    UsuarioID = obj.UsuarioIDAlteracao.Value,
                    DataCriacao = DateTime.Now,
                    Ativo = true,
                    PropostaProrrogacaoID = obj.ID
                };

                _unitOfWork.PropostaProrrogacaoAcompanhamento.Insert(acompanhar);
            }
        }

        public async Task<bool> InsertAsync(PropostaProrrogacaoInserirDto obj)
        {
            try
            {
                await InsertProposal(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _unitOfWork.Commit();
        }

        private async Task InsertProposal(PropostaProrrogacaoInserirDto prorrogacao)
        {
            var proposta = new PropostaProrrogacao
            {
                ID = prorrogacao.ID,
                EmpresaID = prorrogacao.EmpresaID,
                ContaClienteID = prorrogacao.ContaClienteID,
                NumeroInternoProposta = _unitOfWork.PropostaProrrogacao.GetMaxNumeroInterno(),
                DataCriacao = DateTime.Now,
                PropostaCobrancaStatusID = "EC",
                UsuarioIDCriacao = prorrogacao.UsuarioIDCriacao,
                ValorProrrogado = prorrogacao.Titulos.Sum(c => c.ValorInterno),
                ResponsavelID = prorrogacao.UsuarioIDCriacao
            };

            _unitOfWork.PropostaProrrogacao.Insert(proposta);

            await InsertTitulo(prorrogacao);

            await ConceitoInsertII(prorrogacao);
        }

        private async Task InsertTitulo(PropostaProrrogacaoInserirDto prorrogacao)
        {
            foreach (var obj in prorrogacao.Titulos)
            {
                var uptitulo = await _unitOfWork.TituloRepository.GetAsync(c => c.NumeroDocumento.Equals(obj.NumeroDocumento) && c.Linha.Equals(obj.Linha) && c.AnoExercicio.Equals(obj.AnoExercicio) && c.Empresa.Equals(obj.Empresa));

                if (uptitulo != null)
                {
                    uptitulo.PropostaStatus = "P";
                    _unitOfWork.TituloRepository.Update(uptitulo);
                }

                var titulo = new PropostaProrrogacaoTitulo()
                {
                    ID = Guid.NewGuid(),

                    VencimentoProrrogado = obj.VencimentoProrrogado,
                    NotaFiscal = obj.NotaFiscal,
                    NumeroDocumento = obj.NumeroDocumento,
                    Linha = obj.Linha,
                    AnoExercicio = obj.AnoExercicio,
                    Empresa = obj.Empresa,
                    Pedido = obj.OrdemVendaNumero,
                    Emissao = obj.DataEmissaoDocumento,
                    PayT = obj.CondPagto,
                    Valor = obj.ValorInterno,
                    VencimentoOriginal = obj.VencimentoOriginal,
                    ComentarioHistorico = obj.UltimoComentario,
                    PRRPR = obj.TipoPR,
                    Aceite = obj.DataAceite,
                    NaoCobranca = obj.NaoCobranca,
                    NovoVencimento = prorrogacao.NovoVencimento,
                    ContaClienteID = obj.ContaClienteID,
                    PropostaProrrogacaoID = prorrogacao.ID
                };

                _unitOfWork.PropostaProrrogacaoTitulo.Insert(titulo);
            }
        }

        private async Task ConceitoInsertII(PropostaProrrogacaoInserirDto proposta)
        {
            var conceito = await _unitOfWork.ConceitoCobrancaRepository.GetAsync(c => c.Nome.Equals("II"));
            var titulos = proposta.Titulos.Select(c => new { c.ContaClienteID, c.NaoCobranca }).Distinct();
            List<Guid> contasInseridas = new List<Guid>();

            foreach (var titulo in titulos)
            {
                if (!contasInseridas.Any(c => c.Equals(titulo.ContaClienteID)) && titulo.NaoCobranca == false)
                {
                    var financeiroCliente = await _unitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.ContaClienteID == titulo.ContaClienteID && c.EmpresasID == proposta.EmpresaID);
                    if (financeiroCliente != null)
                    {
                        financeiroCliente.ConceitoCobrancaIDAnterior = financeiroCliente.ConceitoCobrancaID;
                        financeiroCliente.ConceitoAnterior = financeiroCliente.Conceito;
                        financeiroCliente.DescricaoConceitoAnterior = financeiroCliente.DescricaoConceito;
                        financeiroCliente.ConceitoCobrancaID = conceito.ID;
                        financeiroCliente.Conceito = false;
                        financeiroCliente.DescricaoConceito = "Adicionado Conceito de Cobrança II após criar a Proposta de Prorrogação para este cliente";
                        _unitOfWork.ContaClienteFinanceiroRepository.UpdateConceito(financeiroCliente);

                        var pc = new ProcessamentoCarteira();
                        pc.ID = Guid.NewGuid();
                        pc.Cliente = financeiroCliente.ContaCliente.CodigoPrincipal;
                        pc.DataHora = DateTime.Now;
                        pc.Status = 2;
                        pc.Motivo = "Conceito de Cobrança II na Proposta de Prorrogação";
                        pc.Detalhes = "Proposta " + proposta.ID + " criada e enviada para prorrogação";
                        pc.EmpresaID = proposta.EmpresaID;
                        _unitOfWork.ProcessamentoCarteiraRepository.Insert(pc);

                        contasInseridas.Add(titulo.ContaClienteID);
                    }
                }
            }
        }

        public async Task<bool> InsertPaymentAsync(PropostaProrrogacaoInserirDto obj)
        {
            try
            {
                var proposta = await _unitOfWork.PropostaProrrogacao.GetAsync(c => c.ID.Equals(obj.ID));

                proposta.ValorProrrogado = proposta.ValorProrrogado + obj.Titulos.Sum(c => c.ValorInterno);
                _unitOfWork.PropostaProrrogacao.Update(proposta);

                await InsertTitulo(obj);

                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> UdpateAsync(PropostaProrrogacaoDto obj)
        {
            var proposta = await _unitOfWork.PropostaProrrogacao.GetAsync(c => c.ID.Equals(obj.ID));
            proposta.MotivoProrrogacaoID = obj.MotivoProrrogacaoID;
            proposta.OrigemRecursoID = obj.OrigemRecursoID;
            proposta.TaxaSugerida = obj.TaxaSugerida;
            proposta.Favoravel = obj.Favoravel;
            proposta.RestricaoSerasa = obj.RestricaoSerasa;
            proposta.AgregaGarantia = obj.AgregaGarantia;
            proposta.ParecerComercial = obj.ParecerComercial;
            proposta.ParecerCobranca = obj.ParecerCobranca;
            proposta.TipoGarantiaID = obj.TipoGarantiaID;
            _unitOfWork.PropostaProrrogacao.Update(proposta);

            var parcelamentos = await _unitOfWork.PropostaProrrogacaoParcelamento.GetAllFilterAsync(c => c.PropostaProrrogacaoID.Equals(obj.ID));

            foreach (var parcela in parcelamentos)
            {
                _unitOfWork.PropostaProrrogacaoParcelamento.Delete(parcela);
            }

            foreach (var pa in obj.Parcelamentos)
            {
                pa.PropostaProrrogacaoID = obj.ID;
                pa.ID = Guid.NewGuid();

                _unitOfWork.PropostaProrrogacaoParcelamento.Insert(pa.MapTo<PropostaProrrogacaoParcelamento>());
            }

            await SaveFollow(obj);

            return _unitOfWork.Commit();
        }

        public async Task<bool> SendCollection(PropostaProrrogacaoDto obj, string URL)
        {
            var proposta = await _unitOfWork.PropostaProrrogacao.GetAsync(c => c.ID.Equals(obj.ID));
            proposta.ResponsavelID = obj.ResponsavelID;
            proposta.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            proposta.DataAlteracao = DateTime.Now;
            proposta.PropostaCobrancaStatusID = "CC";
            proposta.CodigoSap = obj.CodigoSap;
            _unitOfWork.PropostaProrrogacao.Update(proposta);

            return await SendBlog(obj, URL);
        }

        private async Task<bool> SendBlog(PropostaProrrogacaoDto obj, string URL)
        {
            var blog = new AppServiceBlog(_unitOfWork);

            var insertblog = new BlogDto
            {
                Area = obj.ID,
                ContaClienteID = (Guid)obj.ContaClienteID,
                EmpresaID = obj.EmpresaID,
                DataCriacao = DateTime.Now,
                Mensagem = obj.Motivo,
                ParaID = obj.ResponsavelID,
                UsuarioCriacaoID = obj.UsuarioIDAlteracao.Value
            };

            return await blog.InsertAsync(insertblog, URL);
        }

        public async Task<bool> SendCommittee(PropostaProrrogacaoDto obj, string URL)
        {
            var conta = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID == obj.ContaClienteID);
            var segmentoid = Guid.Empty;
            if (!conta.SegmentoID.HasValue)
                throw new ArgumentException("Envio para o comite não permitido, pois, está conta não possuí um Segmento.");
            else
                segmentoid = conta.SegmentoID.Value;

            var item = await _unitOfWork.FluxoLiberacaoProrrogacaoRepository.GetAsync(c => c.SegmentoID.Equals(segmentoid) && c.Ativo && c.EmpresaID.Equals(obj.EmpresaID) && c.Nivel == 1);
            if (item == null)
                throw new ArgumentException("Envio para o comite não permitido, pois, não possuí fluxo de aprovação para este segmento.");

            var primeiroaprovador = await _unitOfWork.EstruturaPerfilUsuarioRepository.GetAsync(c => c.CodigoSap.Equals(obj.CodigoSap) && c.PerfilId.Equals(item.PrimeiroPerfilID));
            if (!primeiroaprovador.UsuarioId.HasValue)
                throw new ArgumentException("Envio para o comite não permitido, pois, não possuí um responsável para o primeiro nivel.");

            if (item.SegundoPerfilID.HasValue)
            {
                var segundoaprovador = await _unitOfWork.EstruturaPerfilUsuarioRepository.GetAsync(c => c.CodigoSap.Equals(obj.CodigoSap) && c.PerfilId.Equals(item.SegundoPerfilID.Value));
                if (!segundoaprovador.UsuarioId.HasValue)
                    throw new ArgumentException("Envio para o comite não permitido, pois, não possuí um segundo responsável para o primeiro nivel.");
            }

            var enviocomite = await _unitOfWork.PropostaProrrogacao.InsertPropostaProrrogacaoComite(obj.ID, segmentoid, obj.CodigoSap, obj.UsuarioIDCriacao, obj.EmpresaID);
            var comite = enviocomite.MapTo<PropostaProrrogacaoComiteDto>();

            comite.EmpresaID = obj.EmpresaID;

            try
            {
                await EnvioEmailComite(comite, URL);
            }
            catch
            {
                // Não deve lançar exception!
            }

            return true;
        }

        private async Task EnvioEmailComite(PropostaProrrogacaoComiteDto responsavel, string URL)
        {
            try
            {
                var email = new AppServiceEnvioEmail(_unitOfWork);
                await email.SendMailComiteProrrogacao(responsavel, URL);
            }
            catch
            {

            }
        }

        public async Task<PropostaProrrogacaoDto> GetAsync(Guid ID, Guid usuarioID)
        {
            var proposta = await _unitOfWork.PropostaProrrogacao.GetAsync(c => c.ID.Equals(ID));
            var titulos = await _unitOfWork.PropostaProrrogacaoTitulo.GetAllFilterAsync(c => c.PropostaProrrogacaoID.Equals(ID));

            IList<PropostaProrrogacaoTituloDto> titulosDto = new List<PropostaProrrogacaoTituloDto>();
            foreach (var titulo in titulos)
            {
                titulosDto.Add(new PropostaProrrogacaoTituloDto
                {
                    VencimentoProrrogado = titulo.VencimentoProrrogado,
                    NotaFiscal = titulo.NotaFiscal,
                    NumeroDocumento = titulo.NumeroDocumento,
                    Linha = titulo.Linha,
                    AnoExercicio = titulo.AnoExercicio,
                    Empresa = titulo.Empresa,
                    OrdemVendaNumero = titulo.Pedido,
                    DataEmissaoDocumento = titulo.Emissao,
                    CondPagto = titulo.PayT,
                    ValorInterno = titulo.Valor,
                    VencimentoOriginal = titulo.VencimentoOriginal,
                    UltimoComentario = titulo.ComentarioHistorico,
                    TipoPR = titulo.PRRPR,
                    DataAceite = titulo.Aceite,
                    NaoCobranca = titulo.NaoCobranca,
                    NovoVencimento = titulo.NovoVencimento
                });
            }

            var parcelamentos = await _unitOfWork.PropostaProrrogacaoParcelamento.GetAllFilterAsync(c => c.PropostaProrrogacaoID.Equals(ID));
            proposta.Parcelamentos = parcelamentos;

            var retorno = proposta.MapTo<PropostaProrrogacaoDto>();
            retorno.Titulos = titulosDto;

            var acompanhar = await _unitOfWork.PropostaProrrogacaoAcompanhamento.GetAsync(c => c.PropostaProrrogacaoID.Equals(ID) && c.UsuarioID.Equals(usuarioID));
            retorno.Acompanhar = acompanhar != null && acompanhar.Ativo;

            return retorno;
        }

        public async Task<IEnumerable<PropostaProrrogacaoComiteDto>> GetCommitteeAsync(Guid ID)
        {
            var comite = await _unitOfWork.PropostaProrrogacaoComite.GetAllFilterAsync(c => c.PropostaProrrogacaoID.Equals(ID));
            comite = comite.OrderBy(c => c.Nivel).ThenBy(c => c.Round).ThenBy(c => c.DataCriacao);

            return comite.MapTo<IEnumerable<PropostaProrrogacaoComiteDto>>();
        }

        public async Task<bool> CancelAsync(Guid PropostaID, string EmpresaID, Guid Usuario, string URL)
        {
            var proposta = await _unitOfWork.PropostaProrrogacao.GetAsync(c => c.ID.Equals(PropostaID)); // ? Se a busca é por ID, qual a necessidade da empresa!?
            proposta.PropostaCobrancaStatusID = "CA";
            proposta.DataAlteracao = DateTime.Now;
            proposta.UsuarioIDAlteracao = Usuario;
            proposta.ResponsavelID = Usuario;
            _unitOfWork.PropostaProrrogacao.Update(proposta);

            await ConceitoRemove(PropostaID);

            _unitOfWork.Commit();

            if (proposta.UsuarioIDCriacao != Usuario)
            {
                await SendEmail(proposta.ID, proposta.UsuarioIDCriacao, "Proposta de prorrogação cancelada.", URL);
            }

            return true;
        }

        private async Task ConceitoRemove(Guid PropostaID)
        {
            var proposta = await _unitOfWork.PropostaProrrogacao.GetAsync(c => c.ID.Equals(PropostaID));
            var titulos = await _unitOfWork.PropostaProrrogacaoTitulo.GetAllFilterAsync(c => c.PropostaProrrogacaoID.Equals(PropostaID));
            var tituloss = titulos.Select(c => new { c.ContaClienteID, c.NaoCobranca }).Distinct();
            List<Guid> contasRemovidas = new List<Guid>();

            foreach (var titulo in titulos)
            {
                var objtitulo = await _unitOfWork.TituloRepository.GetAsync(c => c.NumeroDocumento.Equals(titulo.NumeroDocumento) && c.AnoExercicio.Equals(titulo.AnoExercicio) && c.Empresa.Equals(titulo.Empresa) && c.Linha.Equals(titulo.Linha));
                objtitulo.PropostaStatus = string.Empty;
                _unitOfWork.TituloRepository.Update(objtitulo);
            }

            foreach (var titulo in tituloss)
            {
                if (!contasRemovidas.Any(c => c.Equals(titulo.ContaClienteID)) && titulo.NaoCobranca == false)
                {
                    var financeiroCliente = await _unitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.ContaClienteID == titulo.ContaClienteID && c.EmpresasID == proposta.EmpresaID);
                    if (financeiroCliente != null)
                    {
                        financeiroCliente.ConceitoCobrancaID = financeiroCliente.ConceitoCobrancaIDAnterior;
                        financeiroCliente.DescricaoConceito = financeiroCliente.DescricaoConceitoAnterior;
                        financeiroCliente.Conceito = financeiroCliente.ConceitoAnterior;
                        _unitOfWork.ContaClienteFinanceiroRepository.UpdateConceito(financeiroCliente);

                        var pc = new ProcessamentoCarteira();
                        pc.ID = Guid.NewGuid();
                        pc.Cliente = financeiroCliente.ContaCliente.CodigoPrincipal;
                        pc.DataHora = DateTime.Now;
                        pc.Status = 2;
                        pc.Motivo = "Remover Conceito de Cobrança na Proposta de Prorrogação";
                        pc.Detalhes = "Proposta " + proposta.ID + " cancelada ou rejeitada";
                        pc.EmpresaID = proposta.EmpresaID;
                        _unitOfWork.ProcessamentoCarteiraRepository.Insert(pc);

                        contasRemovidas.Add(titulo.ContaClienteID);
                    }
                }
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

        private async Task RFCSap(Guid PropostaID)
        {
            var rfc = new AppServiceRFCSap(_unitOfWork);

            try
            {
                await rfc.ProrrogarTitulos(PropostaID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> AprovaReprovaProrrogacao(AprovaReprovaProrrogacaoDto aprovacao, string URL)
        {
            try
            {
                var nivel = await _unitOfWork.PropostaProrrogacao.AprovaReprovaProrrogacao(aprovacao.FluxoID, aprovacao.UsuarioID, aprovacao.NovasLiberacoes, aprovacao.Aprovado, aprovacao.TaxaJuros, aprovacao.Comentario, aprovacao.EmpresaID);
                var proposta = await _unitOfWork.PropostaProrrogacao.GetAsync(c => c.ID.Equals(aprovacao.PropostaProrrogacaoID));

                if (nivel != null)
                {
                    try
                    {
                        if (proposta.PropostaCobrancaStatusID.Equals("AC"))
                        {
                            await SendEmail(proposta.ID, proposta.ResponsavelID.Value, "Proposta de prorrogação enviada para aprovação.", URL);
                        }
                        else if (proposta.PropostaCobrancaStatusID.Equals("RE"))
                        {
                            await ConceitoRemove(proposta.ID);

                            _unitOfWork.Commit();

                            await SendEmail(proposta.ID, proposta.UsuarioIDCriacao, "Proposta de prorrogação rejeitada.", URL);
                        }
                        else
                        {
                            await EnvioEmailComite(new PropostaProrrogacaoComiteDto() { PropostaProrrogacaoID = aprovacao.PropostaProrrogacaoID, EmpresaID = aprovacao.EmpresaID, UsuarioID = nivel.UsuarioID }, URL);
                        }
                    }
                    catch
                    {
                        // Não deve lançar exception!
                    }

                    return true;
                }
                else
                {
                    try
                    {
                        if (proposta.PropostaCobrancaStatusID.Equals("AC"))
                        {
                            await SendEmail(proposta.ID, proposta.ResponsavelID.Value, "Proposta de prorrogação enviada para aprovação.", URL);
                        }
                        else if (proposta.PropostaCobrancaStatusID.Equals("RE"))
                        {
                            await ConceitoRemove(proposta.ID);

                            _unitOfWork.Commit();

                            await SendEmail(proposta.ID, proposta.UsuarioIDCriacao, "Proposta de prorrogação rejeitada.", URL);
                        }
                    }
                    catch
                    {
                        // Não deve lançar exception!
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return true;
        }

        public async Task<bool> EfetivaProrrogacao(PropostaProrrogacaoDto prorrogacao, string URL)
        {
            var titulosProposta = await _unitOfWork.PropostaProrrogacaoTitulo.GetAllFilterAsync(c => c.PropostaProrrogacaoID.Equals(prorrogacao.ID));

            foreach (var item in titulosProposta)
            {
                var titulo = await _unitOfWork.TituloRepository.GetAsync(c => c.NumeroDocumento.Equals(item.NumeroDocumento) && c.Linha.Equals(item.Linha) && c.AnoExercicio.Equals(item.AnoExercicio) && c.Empresa.Equals(item.Empresa));

                if (titulo.DataOriginal == null)
                {
                    titulo.DataOriginal = titulo.DataVencimento;
                }

                titulo.DataVencimento = item.NovoVencimento;

                if (titulo.DataPR == null)
                {
                    titulo.DataPR = DateTime.Now;
                }
                else
                {
                    titulo.DataREPR = DateTime.Now;
                }

                titulo.PropostaStatus = "";

                _unitOfWork.TituloRepository.Update(titulo);
            }

            var proposta = await _unitOfWork.PropostaProrrogacao.GetAsync(c => c.ID.Equals(prorrogacao.ID));
            proposta.PropostaCobrancaStatusID = "AP";
            _unitOfWork.PropostaProrrogacao.Update(proposta);

            await ConceitoInsertI(prorrogacao);

            var commit = _unitOfWork.Commit();

            if (commit)
            {
                try
                {
                    await RFCSap(proposta.ID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                try
                {
                    await SendEmail(proposta.ID, proposta.UsuarioIDCriacao, "proposta de prorrogação aprovada.", URL);
                }
                catch
                {
                    // Não deve lançar exception!
                }
            }

            return commit;
        }

        private async Task ConceitoInsertI(PropostaProrrogacaoDto proposta)
        {
            var conceito = await _unitOfWork.ConceitoCobrancaRepository.GetAsync(c => c.Nome.Equals("I"));
            var titulos = await _unitOfWork.PropostaProrrogacaoTitulo.GetAllFilterAsync(c => c.PropostaProrrogacaoID.Equals(proposta.ID));
            var tituloss = titulos.Select(c => new { c.ContaClienteID, c.NaoCobranca }).Distinct();
            var comites = await _unitOfWork.PropostaProrrogacaoComite.GetAllFilterAsync(c => c.PropostaProrrogacaoID.Equals(proposta.ID));
            var aprovador = comites.OrderByDescending(c => c.Nivel).ThenByDescending(c => c.Round).ThenByDescending(c => c.DataCriacao).FirstOrDefault();
            List<Guid> contasInseridas = new List<Guid>();

            foreach (var titulo in tituloss)
            {
                if (!contasInseridas.Any(c => c.Equals(titulo.ContaClienteID)) && titulo.NaoCobranca == false && aprovador.NovasLiberacoes == false)
                {
                    var financeiroCliente = await _unitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.ContaClienteID == titulo.ContaClienteID && c.EmpresasID == proposta.EmpresaID);
                    if (financeiroCliente != null)
                    {
                        financeiroCliente.ConceitoCobrancaIDAnterior = financeiroCliente.ConceitoCobrancaID;
                        financeiroCliente.ConceitoAnterior = financeiroCliente.Conceito;
                        financeiroCliente.DescricaoConceitoAnterior = financeiroCliente.DescricaoConceito;
                        financeiroCliente.ConceitoCobrancaID = conceito.ID;
                        financeiroCliente.Conceito = false;
                        financeiroCliente.DescricaoConceito = "Adicionado Conceito de Cobrança I após aprovar Proposta de Prorrogação para este cliente";
                        _unitOfWork.ContaClienteFinanceiroRepository.UpdateConceito(financeiroCliente);

                        var pc = new ProcessamentoCarteira();
                        pc.ID = Guid.NewGuid();
                        pc.Cliente = financeiroCliente.ContaCliente.CodigoPrincipal;
                        pc.DataHora = DateTime.Now;
                        pc.Status = 2;
                        pc.Motivo = "Conceito de Cobrança I na Proposta de Prorrogação";
                        pc.Detalhes = "Proposta " + proposta.ID + " aprovada";
                        pc.EmpresaID = proposta.EmpresaID;
                        _unitOfWork.ProcessamentoCarteiraRepository.Insert(pc);

                        contasInseridas.Add(titulo.ContaClienteID);
                    }
                }
            }
        }

        private async Task AddProcessamentoCarteira(Guid ContaClienteID, string EmpresaID, Guid proposta)
        {
            var contaCliente = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID == ContaClienteID);

            var prorrogacao = await _unitOfWork.PropostaProrrogacao.GetAsync(c => c.ID.Equals(proposta));
            var prorrogacaodto = prorrogacao.MapTo<PropostaProrrogacaoDto>();

            var carteira = new ProcessamentoCarteira
            {
                ID = Guid.NewGuid(),
                Cliente = contaCliente.CodigoPrincipal,
                DataHora = DateTime.Now,
                Status = 2,
                Motivo = "Prorrogou titulos para o Cliente: " + contaCliente.Nome,
                Detalhes = $"A proposta {prorrogacaodto.NumeroProposta} prorrogou os titulos do cliente " + contaCliente.Nome,
                EmpresaID = EmpresaID
            };

            _unitOfWork.ProcessamentoCarteiraRepository.Insert(carteira);
        }

        public async Task<IEnumerable<BuscaDetalhesPropostaProrrogacaoTituloDto>> BuscaDetalhesTitulos(Guid propostaProrrogacaoId, string empresaId)
        {
            var prorrogacao = await _unitOfWork.PropostaProrrogacao.BuscaDetalhesTitulos(propostaProrrogacaoId, empresaId);
            return Mapper.Map<IEnumerable<BuscaDetalhesPropostaProrrogacaoTituloDto>>(prorrogacao);
        }
    }
}