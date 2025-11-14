using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.AppService
{
    public class AppServicePropostaAbono : IAppServicePropostaAbono
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServicePropostaAbono(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CancelAsync(Guid PropostaID, string EmpresaID, Guid Usuario, string URL)
        {
            var proposta = await _unitOfWork.PropostaAbonoRepository.GetAsync(c => c.ID.Equals(PropostaID) && c.EmpresaID == EmpresaID);

            var titulos = await _unitOfWork.PropostaAbonoTituloRepository.GetAllFilterAsync(c => c.PropostaAbonoID.Equals(PropostaID));

            foreach (var titulo in titulos)
            {
                var objtitulo = await _unitOfWork.TituloRepository.GetAsync(c => c.NumeroDocumento.Equals(titulo.NumeroDocumento) && c.AnoExercicio.Equals(titulo.AnoExercicio) && c.Empresa.Equals(titulo.Empresa) && c.Linha.Equals(titulo.Linha));

                objtitulo.PropostaStatus = null;
                objtitulo.Aberto = true;

                _unitOfWork.TituloRepository.Update(objtitulo);
            }

            proposta.PropostaCobrancaStatusID = "CA";
            proposta.DataAlteracao = DateTime.Now;
            proposta.UsuarioIDAlteracao = Usuario;
            proposta.ResponsavelID = Usuario;

            _unitOfWork.PropostaAbonoRepository.Update(proposta);

            _unitOfWork.Commit();

            if (proposta.UsuarioIDCriacao != Usuario)
                await SendEmail(proposta.ID, proposta.UsuarioIDCriacao, "Proposta de abono cancelada.", URL);

            return true;
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
                await rfc.AbonarTitulos(PropostaID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> InsertAutoAsync(PropostaAbonoInserirDto obj)
        {
            try
            {
                var conta = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(obj.ContaClienteID));

                var segmentoid = conta.SegmentoID ?? null;
                if (segmentoid == null)
                {
                    throw new ArgumentException($"Está conta não possuí Segmento válido.");
                }

                var nivel01 = await _unitOfWork.FluxoLiberacaoAbonoRepository.GetAsync(c => c.Ativo && c.Nivel == 1 && c.SegmentoID == segmentoid && c.EmpresaID == obj.EmpresaID);
                if (nivel01 == null)
                {
                    throw new ArgumentException($"O segmento desta conta, não possuí nivel 1 de aprovação.");
                }

                var totaltitulos = obj.Titulos.Sum(c => c.ValorDocumento);
                if (totaltitulos > nivel01.ValorAte)
                {
                    throw new ArgumentException($"O Valor está acima do valor de {nivel01.ValorAte:C} permitido para aprovação para abono automático.");
                }

                await InsertPropostaAbonoAutomatico(obj, "AA");

                await InsertTitulosAuto(obj);

                var insert1 = _unitOfWork.Commit();

                await UpdateDEbitoSinistro(obj);

                await AddProcessamentoCarteira(obj.ContaClienteID, obj.EmpresaID, obj.ID);

                await RFCSap(obj.ID);

                var insert2 = _unitOfWork.Commit();

                return insert1 && insert2;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InsertAsync(PropostaAbonoInserirDto obj)
        {
            try
            {
                await InsertPropostaAbonoAutomatico(obj, "EC");

                await InsertTitulos(obj);

                _unitOfWork.Commit();

                await UpdateDEbitoSinistro(obj);

                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> InsertPaymentAsync(PropostaAbonoInserirDto obj)
        {
            try
            {
                var proposta = await _unitOfWork.PropostaAbonoRepository.GetAsync(c => c.ID.Equals(obj.ID) && c.EmpresaID == obj.EmpresaID);

                proposta.ValorTotalDocumento = proposta.ValorTotalDocumento + obj.Titulos.Sum(c => c.ValorDocumento);
                _unitOfWork.PropostaAbonoRepository.Update(proposta);

                await InsertTitulos(obj);

                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task SaveFollow(PropostaAbonoDto obj)
        {

            var acompanha = await
                _unitOfWork.PropostaAbonoAcompanhamento.GetAsync(c => c.PropostaAbonoID.Equals(obj.ID) &&
                                                                      c.UsuarioID.Equals(obj.UsuarioIDAlteracao.Value));
            if (acompanha != null)
            {
                acompanha.Ativo = obj.Acompanhar;
                _unitOfWork.PropostaAbonoAcompanhamento.Update(acompanha);
            }
            else
            {
                var acompanhar = new PropostaAbonoAcompanhamento()
                {
                    UsuarioID = obj.UsuarioIDAlteracao.Value,
                    DataCriacao = DateTime.Now,
                    Ativo = true,
                    PropostaAbonoID = obj.ID
                };
                _unitOfWork.PropostaAbonoAcompanhamento.Insert(acompanhar);
            }

        }

        public async Task<bool> UdpateAsync(PropostaAbonoDto obj)
        {

            var proposta = await _unitOfWork.PropostaAbonoRepository.GetAsync(c => c.ID.Equals(obj.ID) && c.EmpresaID.Equals(obj.EmpresaID));

            proposta.ConceitoH = obj.ConceitoH;
            proposta.ParecerCobranca = obj.ParecerCobranca ?? proposta.ParecerCobranca;
            proposta.ParecerComercial = obj.ParecerComercial ?? proposta.ParecerComercial;
            proposta.MotivoAbonoID = obj.MotivoAbonoID ?? proposta.MotivoAbonoID;
            proposta.ResponsavelID = obj.UsuarioIDAlteracao.Value;
            proposta.UsuarioIDAlteracao = proposta.ResponsavelID;
            proposta.DataAlteracao = DateTime.Now;

            //proposta.ValorTotalDocumento = obj.Titulos.Sum(c => c.ValorDocumento);

            _unitOfWork.PropostaAbonoRepository.Update(proposta);

            await SaveFollow(obj);

            return _unitOfWork.Commit();
        }

        public async Task<bool> SendCollection(PropostaAbonoDto obj, string URL)
        {
            var abono = await _unitOfWork.PropostaAbonoRepository.GetAsync(c => c.ID.Equals(obj.ID) && c.EmpresaID == obj.EmpresaID);
            abono.ResponsavelID = obj.ResponsavelID;
            abono.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            abono.DataAlteracao = DateTime.Now;
            abono.PropostaCobrancaStatusID = "CC";
            abono.CodigoSap = obj.CodigoSap;
            _unitOfWork.PropostaAbonoRepository.Update(abono);

            return await SendBlog(obj, URL);
        }

        private async Task<bool> SendBlog(PropostaAbonoDto obj, string URL)
        {
            var blog = new AppServiceBlog(_unitOfWork);

            var insertblog = new BlogDto
            {
                Area = obj.ID,
                ContaClienteID = obj.ContaClienteID,
                EmpresaID = obj.EmpresaID,
                DataCriacao = DateTime.Now,
                Mensagem = obj.Motivo,
                ParaID = obj.ResponsavelID,
                UsuarioCriacaoID = obj.UsuarioIDAlteracao.Value
            };

            return await blog.InsertAsync(insertblog, URL);
        }

        public async Task<bool> SendCommittee(PropostaAbonoDto obj, string URL)
        {
            var conta = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(obj.ContaClienteID));
            var segmentoid = Guid.Empty;
            if (!conta.SegmentoID.HasValue)
                throw new ArgumentException("Envio para o comite não permitido, pois, está conta não possuí um Segmento.");
            else
                segmentoid = conta.SegmentoID.Value;

            var item = await _unitOfWork.FluxoLiberacaoAbonoRepository.GetAsync(c => c.SegmentoID.Equals(segmentoid) && c.Ativo && c.EmpresaID.Equals(obj.EmpresaID) && c.Nivel == 1);
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

            var enviocomite = await _unitOfWork.PropostaAbonoRepository.InsertPropostaAbonoComite(obj.ID, segmentoid, obj.CodigoSap, obj.UsuarioIDCriacao, obj.EmpresaID);
            var comite = enviocomite.MapTo<PropostaAbonoComiteDto>();

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

        private async Task EnvioEmailComite(PropostaAbonoComiteDto responsavel, string URL)
        {
            try
            {
                var email = new AppServiceEnvioEmail(_unitOfWork);
                await email.SendMailComiteAbono(responsavel, URL);
            }
            catch
            {

            }
        }

        public async Task<PropostaAbonoDto> GetAsync(Guid ID, Guid usuarioID)
        {
            var abono = await _unitOfWork.PropostaAbonoRepository.GetAsync(c => c.ID.Equals(ID));

            var titulos =
                await _unitOfWork.PropostaAbonoTituloRepository.GetAllFilterAsync(c => c.PropostaAbonoID.Equals(ID));

            abono.Titulos = titulos;
            var retorno = abono.MapTo<PropostaAbonoDto>();

            var acompanhar = await
                _unitOfWork.PropostaAbonoAcompanhamento.GetAsync(
                    c => c.PropostaAbonoID.Equals(abono.ID) && c.UsuarioID.Equals(usuarioID));

            retorno.Acompanhar = acompanhar != null && acompanhar.Ativo;

            return retorno;
        }

        public async Task<IEnumerable<PropostaAbonoComiteDto>> GetCommitteeAsync(Guid ID)
        {
            var comite = await _unitOfWork.PropostaAbonoComite.GetAllFilterAsync(c => c.PropostaAbonoID.Equals(ID));
            comite = comite.OrderBy(c => c.Nivel).ThenBy(c => c.Round).ThenBy(c => c.DataCriacao);
            return comite.MapTo<IEnumerable<PropostaAbonoComiteDto>>();
        }

        public async Task<bool> AprovaReprovaAbono(AprovaReprovaAbonoDto aprovacao, string URL)
        {
            try
            {
                var nivel = await _unitOfWork.PropostaAbonoRepository.AprovaReprovaAbono(aprovacao.FluxoID, aprovacao.UsuarioID, aprovacao.ConceitoH, aprovacao.Aprovado, aprovacao.Comentario, aprovacao.EmpresaID);
                var proposta = await _unitOfWork.PropostaAbonoRepository.GetAsync(c => c.ID.Equals(aprovacao.PropostaAbonoID));

                // Se proposta aprovada enviar para o SAP
                if (proposta.PropostaCobrancaStatusID == "AP")
                {
                    await RFCSap(proposta.ID);
                }

                if (nivel != null)
                {
                    try
                    {
                        if (proposta.PropostaCobrancaStatusID.Equals("AC"))
                        {
                            await SendEmail(proposta.ID, proposta.ResponsavelID, "Proposta de abono enviada para cobrança.", URL);
                        }
                        else if (proposta.PropostaCobrancaStatusID.Equals("RE"))
                        {
                            await SendEmail(proposta.ID, proposta.UsuarioIDCriacao, "Proposta de abono rejeitada.", URL);
                        }
                        else
                        {
                            await EnvioEmailComite(new PropostaAbonoComiteDto()
                            {
                                PropostaAbonoID = aprovacao.PropostaAbonoID,
                                EmpresaID = aprovacao.EmpresaID,
                                UsuarioID = nivel.UsuarioID
                            }, URL);
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
                            await SendEmail(proposta.ID, proposta.ResponsavelID, "Proposta de abono enviada para cobrança.", URL);
                        }
                        else if (proposta.PropostaCobrancaStatusID.Equals("RE"))
                        {
                            await SendEmail(proposta.ID, proposta.UsuarioIDCriacao, "Proposta de abono rejeitada.", URL);
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

        private async Task InsertPropostaAbonoAutomatico(PropostaAbonoInserirDto obj, string Status)
        {
            var total = obj.Titulos?.Sum(c => c.ValorDocumento);

            var abono = new PropostaAbono
            {
                ID = obj.ID,
                DataCriacao = DateTime.Now,
                ValorTotalDocumento = total == null ? 0.0m : total.Value,
                UsuarioIDCriacao = obj.UsuarioIDCriacao,
                ContaClienteID = obj.ContaClienteID,
                NumeroInternoProposta = _unitOfWork.PropostaAbonoRepository.GetMaxNumeroInterno(),
                EmpresaID = obj.EmpresaID,
                MotivoAbonoID = obj.Motivo,
                PropostaCobrancaStatusID = Status,
                TotalDebito = 0,
                Sinistro = false,
                ResponsavelID = obj.UsuarioIDCriacao
            };

            if (obj.Motivo.Equals(Guid.Empty))
                abono.MotivoAbonoID = null;

            _unitOfWork.PropostaAbonoRepository.Insert(abono);
        }

        private async Task UpdateDEbitoSinistro(PropostaAbonoInserirDto obj)
        {
            var abono = await _unitOfWork.PropostaAbonoRepository.GetAsync(c => c.ID.Equals(obj.ID));

            var total = await _unitOfWork.PropostaAbonoRepository.Total(obj.ID);

            var sinistro = await
                _unitOfWork.ContaClienteFinanceiroRepository.GetAsync(
                    c => c.ContaClienteID.Equals(obj.ContaClienteID) && c.EmpresasID == obj.EmpresaID);

            var possuisinistro = sinistro?.Sinistro ?? false;

            abono.TotalDebito = total;
            abono.Sinistro = abono.Sinistro;

            _unitOfWork.PropostaAbonoRepository.Update(abono);
        }

        private async Task AddProcessamentoCarteira(Guid ContaClienteID, string EmpresaID, Guid proposta)
        {
            var contaCliente = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID == ContaClienteID);

            var abono = await _unitOfWork.PropostaAbonoRepository.GetAsync(c => c.ID.Equals(proposta));
            var abonodto = abono.MapTo<PropostaAbonoDto>();

            var carteira = new ProcessamentoCarteira
            {
                ID = Guid.NewGuid(),
                Cliente = contaCliente.CodigoPrincipal,
                DataHora = DateTime.Now,
                Status = 2,
                Motivo = "Abonou titulos para o Cliente: " + contaCliente.Nome,
                Detalhes = $"A proposta {abonodto.NumeroProposta} abonou os titulos do cliente " + contaCliente.Nome,
                EmpresaID = EmpresaID
            };

            _unitOfWork.ProcessamentoCarteiraRepository.Insert(carteira);
        }

        private async Task InsertTitulosAuto(PropostaAbonoInserirDto obj)
        {
            var listTitulo = new List<PropostaAbonoTitulo>();

            foreach (var ob in obj.Titulos)
            {
                var titulo = await
                    _unitOfWork.TituloRepository.GetAsync(
                        c => c.NumeroDocumento.Equals(ob.NumeroDocumento) && c.AnoExercicio.Equals(ob.AnoExercicio) &&
                             c.Empresa.Equals(ob.Empresa) && c.Linha.Equals(ob.Linha));

                var abono = new PropostaAbonoTitulo()
                {
                    ID = Guid.NewGuid(),
                    OrdemVendaNumero = titulo.OrdemVendaNumero,
                    PropostaAbonoID = obj.ID,
                    Aberto = false,
                    AnoExercicio = titulo.AnoExercicio,
                    CobrancaAutomatica = titulo.CobrancaAutomatica,
                    CodigoCliente = titulo.CodigoCliente,
                    CodigoRazao = titulo.CodigoRazao,
                    CreditoDebito = titulo.CreditoDebito,
                    DataAceite = titulo.DataAceite,
                    DataDuplicata = titulo.DataDuplicata,
                    DataEmissaoDocumento = titulo.DataEmissaoDocumento,
                    DataOriginal = titulo.DataOriginal,
                    DataPR = titulo.DataPR,
                    DataPefinExclusao = titulo.DataPefinExclusao,
                    DataPefinInclusao = titulo.DataPefinInclusao,
                    DataPrevisaoPagamento = titulo.DataPrevisaoPagamento,
                    DataProtesto = titulo.DataProtesto,
                    DataProtestoRealizado = titulo.DataProtestoRealizado,
                    DataREPR = titulo.DataREPR,
                    DataTriplicata = titulo.DataTriplicata,
                    DataVencimento = titulo.DataVencimento,
                    Empresa = titulo.Empresa,
                    InstrumentoPagamento = titulo.InstrumentoPagamento,
                    Linha = titulo.Linha,
                    MoedaDocumento = titulo.MoedaDocumento,
                    MoedaInterna = titulo.MoedaInterna,
                    NotaFiscal = titulo.NotaFiscal,
                    NumeroDocumento = titulo.NumeroDocumento,
                    NumeroDocumentoCompensacao = titulo.NumeroDocumentoCompensacao,
                    OrdemVendaItem = titulo.OrdemVendaItem,
                    StatusCobrancaID = titulo.StatusCobrancaID,
                    TaxaJuros = titulo.TaxaJuros,
                    TextoDocumento = titulo.TextoDocumento,
                    TipoDocumento = titulo.TipoDocumento,
                    ValorDocumento = titulo.ValorDocumento,
                    ValorInterno = titulo.ValorInterno
                };

                listTitulo.Add(abono);

                titulo.Aberto = false;
                titulo.PropostaStatus = "A";

                _unitOfWork.TituloRepository.Update(titulo);
            }

            _unitOfWork.PropostaAbonoTituloRepository.InsertRange(listTitulo);
        }

        private async Task InsertTitulos(PropostaAbonoInserirDto obj)
        {
            var listTitulo = new List<PropostaAbonoTitulo>();

            foreach (var ob in obj.Titulos)
            {
                var titulo = await
                    _unitOfWork.TituloRepository.GetAsync(
                        c => c.NumeroDocumento.Equals(ob.NumeroDocumento) && c.AnoExercicio.Equals(ob.AnoExercicio) &&
                             c.Empresa.Equals(ob.Empresa) && c.Linha.Equals(ob.Linha));

                var abono = new PropostaAbonoTitulo()
                {
                    ID = Guid.NewGuid(),
                    OrdemVendaNumero = titulo.OrdemVendaNumero,
                    PropostaAbonoID = obj.ID,
                    Aberto = false,
                    AnoExercicio = titulo.AnoExercicio,
                    CobrancaAutomatica = titulo.CobrancaAutomatica,
                    CodigoCliente = titulo.CodigoCliente,
                    CodigoRazao = titulo.CodigoRazao,
                    CreditoDebito = titulo.CreditoDebito,
                    DataAceite = titulo.DataAceite,
                    DataDuplicata = titulo.DataDuplicata,
                    DataEmissaoDocumento = titulo.DataEmissaoDocumento,
                    DataOriginal = titulo.DataOriginal,
                    DataPR = titulo.DataPR,
                    DataPefinExclusao = titulo.DataPefinExclusao,
                    DataPefinInclusao = titulo.DataPefinInclusao,
                    DataPrevisaoPagamento = titulo.DataPrevisaoPagamento,
                    DataProtesto = titulo.DataProtesto,
                    DataProtestoRealizado = titulo.DataProtestoRealizado,
                    DataREPR = titulo.DataREPR,
                    DataTriplicata = titulo.DataTriplicata,
                    DataVencimento = titulo.DataVencimento,
                    Empresa = titulo.Empresa,
                    InstrumentoPagamento = titulo.InstrumentoPagamento,
                    Linha = titulo.Linha,
                    MoedaDocumento = titulo.MoedaDocumento,
                    MoedaInterna = titulo.MoedaInterna,
                    NotaFiscal = titulo.NotaFiscal,
                    NumeroDocumento = titulo.NumeroDocumento,
                    NumeroDocumentoCompensacao = titulo.NumeroDocumentoCompensacao,
                    OrdemVendaItem = titulo.OrdemVendaItem,
                    StatusCobrancaID = titulo.StatusCobrancaID,
                    TaxaJuros = titulo.TaxaJuros,
                    TextoDocumento = titulo.TextoDocumento,
                    TipoDocumento = titulo.TipoDocumento,
                    ValorDocumento = titulo.ValorDocumento,
                    ValorInterno = titulo.ValorInterno
                };

                listTitulo.Add(abono);

                titulo.PropostaStatus = "A";

                _unitOfWork.TituloRepository.Update(titulo);
            }

            _unitOfWork.PropostaAbonoTituloRepository.InsertRange(listTitulo);
        }
    }
}

