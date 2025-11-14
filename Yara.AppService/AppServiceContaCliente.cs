using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;
using Yara.Domain.Repository;

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.AppService
{
    public class AppServiceContaCliente : IAppServiceContaCliente
    {
        private readonly IUnitOfWork _untUnitOfWork;

        public AppServiceContaCliente(IUnitOfWork untUnitOfWork)
        {
            _untUnitOfWork = untUnitOfWork;
        }

        public async Task<ContaClienteDto> GetAsync(Expression<Func<ContaClienteDto, bool>> expression)
        {
            var contacliente = await _untUnitOfWork.ContaClienteRepository.GetAsync(
                 Mapper.Map<Expression<Func<ContaCliente, bool>>>(expression));
            var contaclienteobj = Mapper.Map<ContaClienteDto>(contacliente);
            var dtocontaclientecodigo = await _untUnitOfWork.ContaClienteCodigoRepository.GetAllFilterAsync(c => c.ContaClienteID.Equals(contaclienteobj.ID) && c.CodigoPrincipal.Equals(false));
            contaclienteobj.OutrosCodigo = dtocontaclientecodigo.MapTo<List<ContaClienteCodigoDto>>();
            return contaclienteobj;
        }

        public async Task<IEnumerable<ContaClienteDto>> GetAllFilterAsync(Expression<Func<ContaClienteDto, bool>> expression)
        {
            var contaCliente = await
                _untUnitOfWork.ContaClienteRepository.GetAllFilterAsync(
                    Mapper.Map<Expression<Func<ContaCliente, bool>>>(expression));

            return Mapper.Map<IEnumerable<ContaClienteDto>>(contaCliente);
        }

        public async Task<IEnumerable<ContaClienteDto>> GetAllAsync()
        {
            var contaCliente = await _untUnitOfWork.ContaClienteRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<ContaClienteDto>>(contaCliente);
        }

        public bool Insert(ContaClienteDto obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(ContaClienteDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsyncAllowManualLock(ContaClienteDto obj)
        {
            var contacliente = await _untUnitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(obj.ID));

            contacliente.ID = obj.ID;
            contacliente.LiberacaoManual = obj.LiberacaoManual;
            contacliente.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            contacliente.DataAlteracao = DateTime.Now;

            _untUnitOfWork.ContaClienteRepository.Update(contacliente);
            return _untUnitOfWork.Commit();
        }

        public async Task<bool> UpdateEstruturaContaCliente(MovimentacaoEstruturaComercialDto obj)
        {
            var estruturaComercial = await _untUnitOfWork.EstruturaComercialRepository.GetAsync(c => c.CodigoSap.Equals(obj.CodSap));

            ContaClienteEstruturaComercial ccec = null;

            foreach (var contacliente in obj.ContaClientes)
            {
                ccec = await _untUnitOfWork.ContaClienteEstruturaComercialRepository.GetAsync(cce => cce.ContaClienteId.Equals(contacliente.ID) && cce.EstruturaComercialId.Equals(obj.CodSap) && cce.EmpresasId.Equals(obj.EmpresaId));

                if (ccec == null)
                {
                    ccec = new ContaClienteEstruturaComercial();
                    ccec.DataCriacao = DateTime.Now;
                    ccec.ContaClienteId = contacliente.ID;
                    ccec.EstruturaComercialId = estruturaComercial.CodigoSap;
                    ccec.EmpresasId = obj.EmpresaId;

                    _untUnitOfWork.ContaClienteEstruturaComercialRepository.Insert(ccec);
                }
            }

            return _untUnitOfWork.Commit();
        }

        public async Task<bool> UpdateRepresentanteContaCliente(MovimentacaoEstruturaComercialDto obj)
        {
            var repid = new Guid(obj.RepresentanteID);
            var rep = await _untUnitOfWork.RepresentanteRepository.GetAsync(r => r.ID == repid);
            var estruturaComercial = await _untUnitOfWork.EstruturaComercialRepository.GetAsync(c => c.CodigoSap.Equals(obj.CodSap));

            ContaClienteRepresentante ccr = null;
            ContaClienteEstruturaComercial ccec = null;

            foreach (var contacliente in obj.ContaClientes)
            {
                ccec = await _untUnitOfWork.ContaClienteEstruturaComercialRepository.GetAsync(cce => cce.ContaClienteId.Equals(contacliente.ID) && cce.EstruturaComercialId.Equals(obj.CodSap) && cce.EmpresasId.Equals(obj.EmpresaId));

                if (ccec == null)
                {
                    ccec = new ContaClienteEstruturaComercial();
                    ccec.DataCriacao = DateTime.Now;
                    ccec.ContaClienteId = contacliente.ID;
                    ccec.EstruturaComercialId = estruturaComercial.CodigoSap;
                    ccec.EmpresasId = obj.EmpresaId;

                    _untUnitOfWork.ContaClienteEstruturaComercialRepository.Insert(ccec);
                }

                ccr = await _untUnitOfWork.ContaClienteRepresentanteRepository.GetAsync(cr => cr.ContaClienteID.Equals(contacliente.ID) && cr.RepresentanteID.Equals(repid) && cr.EmpresasID.Equals(obj.EmpresaId));

                if (ccr == null)
                {
                    ccr = new ContaClienteRepresentante();
                    ccr.DataCriacao = DateTime.Now;
                    ccr.ContaClienteID = contacliente.ID;
                    ccr.RepresentanteID = repid;
                    ccr.EmpresasID = obj.EmpresaId;

                    if (!string.IsNullOrEmpty(obj.CodSap))
                        ccr.CodigoSapCTC = obj.CodSap;

                    _untUnitOfWork.ContaClienteRepresentanteRepository.Insert(ccr);
                }
                else
                {
                    if (!string.IsNullOrEmpty(obj.CodSap))
                    {
                        if (ccr.CodigoSapCTC != obj.CodSap)
                        {
                            ccr.CodigoSapCTC = obj.CodSap;
                            _untUnitOfWork.ContaClienteRepresentanteRepository.Update(ccr);
                        }
                    }
                }
            }

            return _untUnitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(ContaClienteDto obj)
        {
            var cliente = obj.MapTo<ContaCliente>();
            var exist = await _untUnitOfWork.ContaClienteRepository.GetAsync(c => c.Documento.Equals(obj.Documento));

            if (exist != null)
                throw new ArgumentException("Este Cliente já esta cadastrado.");

            cliente.DataCriacao = DateTime.Now;
            cliente.DataNascimentoFundacao = DateTime.Now;
            cliente.UsuarioIDCriacao = obj.UsuarioIDCriacao;

            _untUnitOfWork.ContaClienteRepository.Insert(cliente);

            return _untUnitOfWork.Commit();
        }

        public BuscaOrdemVendaSumarizadoDto GetOrdemVendaSumarizado(Guid ContaClienteID, string Empresa)
        {
            var sumarizado = _untUnitOfWork.ContaClienteRepository.OrdemVendaSumarizado(ContaClienteID, Empresa);
            return sumarizado.MapTo<BuscaOrdemVendaSumarizadoDto>();
        }

        public async Task<IEnumerable<BuscaOrdemVendasPrazoDto>> GetOrdemVendaPorClientePrazo(BuscaOrdemVendasPrazoDto ordemVendasPrazo)
        {
            var buscaContaClientePrazo = ordemVendasPrazo.MapTo<BuscaOrdemVendasPrazo>();
            var listaContaClientePrazo = await _untUnitOfWork.ContaClienteRepository.OrdemVendaPorClientePrazo(buscaContaClientePrazo);
            return Mapper.Map<IEnumerable<BuscaOrdemVendasPrazoDto>>(listaContaClientePrazo);
        }

        public async Task<IEnumerable<BuscaOrdemVendasAVistaDto>> GetOrdemVendaPorClienteVista(BuscaOrdemVendasAVistaDto ordemVendasAVista)
        {
            var buscaContaClienteAVista = ordemVendasAVista.MapTo<BuscaOrdemVendasAVista>();
            var listaContaClienteAVista = await _untUnitOfWork.ContaClienteRepository.OrdemVendaPorClienteVista(buscaContaClienteAVista);
            return Mapper.Map<IEnumerable<BuscaOrdemVendasAVistaDto>>(listaContaClienteAVista);
        }

        public async Task<IEnumerable<BuscaOrdemVendasPagaRetiraDto>> GetOrdemVendaPorClienteRetira(BuscaOrdemVendasPagaRetiraDto vendasPagaRetiraDto)
        {
            var buscaContaClienteRetira = vendasPagaRetiraDto.MapTo<BuscaOrdemVendasPagaRetira>();
            var listaContaClienteRetira = await _untUnitOfWork.ContaClienteRepository.OrdemVendaPorClienteRetira(buscaContaClienteRetira);
            return Mapper.Map<IEnumerable<BuscaOrdemVendasPagaRetiraDto>>(listaContaClienteRetira);
        }

        public async Task<IEnumerable<BuscaContaClienteDto>> GetListAccountClient(BuscaContaClienteDto buscaContaClienteDto, Guid usuarioId)
        {
            var buscaContaCliente = buscaContaClienteDto.MapTo<BuscaContaCliente>();
            var listaContaCliente = await _untUnitOfWork.ContaClienteRepository.BuscaContaCliente(buscaContaCliente, usuarioId);
            return Mapper.Map<IEnumerable<BuscaContaClienteDto>>(listaContaCliente);
        }

        public async Task<IEnumerable<BuscaContaClienteEstComlDto>> GetListByComlStruc(BuscaContaClienteEstComlDto busca)
        {
            var buscaContaCliente = busca.MapTo<BuscaContaClienteEstComl>();
            var listaContaCliente = await _untUnitOfWork.ContaClienteRepository.BuscaContaClienteEstComl(buscaContaCliente);
            return Mapper.Map<IEnumerable<BuscaContaClienteEstComlDto>>(listaContaCliente);
        }

        public async Task<ContaClienteDto> GetByID(Guid id, Guid usuarioId, string empresaId)
        {
            // Checar se usuário tem acesso a conta cliente.
            var temAcesso = await _untUnitOfWork.ContaClienteRepository.ValidaAcessoContaCliente(id, usuarioId);
            if (!temAcesso)
                throw new UnauthorizedAccessException();

            var contacliente = await _untUnitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(id));
            //var contacliente = await _untUnitOfWork.ContaClienteRepository.GetOneByIDAsync(id);
            var contaclienteobj = Mapper.Map<ContaClienteDto>(contacliente);
            if (contaclienteobj.ContaClienteEstruturaComercial != null)
                contaclienteobj.ContaClienteEstruturaComercial = contaclienteobj.ContaClienteEstruturaComercial.Where(cce => cce.EmpresasId == empresaId).ToList();

            var dtocontaclientecodigo = await _untUnitOfWork.ContaClienteCodigoRepository.GetAllFilterAsync(c => c.ContaClienteID.Equals(contaclienteobj.ID) && c.CodigoPrincipal.Equals(false));
            contaclienteobj.OutrosCodigo = dtocontaclientecodigo.MapTo<List<ContaClienteCodigoDto>>();

            return contaclienteobj;
        }

        public async Task<ContaClienteDto> GetByCodePrincipal(string code)
        {
            var contacliente = await _untUnitOfWork.ContaClienteRepository.GetAsync(c => c.CodigoPrincipal.Equals(code));
            return contacliente.MapTo<ContaClienteDto>();
        }

        public async Task<Guid?> GetIdByCode(string code)
        {
            code = code.PadLeft(10, '0');
            var contaClienteCodigo = await _untUnitOfWork.ContaClienteCodigoRepository.GetAsync(c => c.Codigo.Equals(code));
            return contaClienteCodigo?.ContaClienteID;
        }

        public async Task<string> UpdateAsync(ContaClienteAlteracaoDadosPessoaisDto obj)
        {
            var contacliente = await _untUnitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(obj.ID));
            List<string> retorno = new List<string>();

            if (contacliente.SegmentoID != obj.SegmentoID)
            {
                var segmento = await _untUnitOfWork.SegmentoRepository.GetAsync(c => c.ID.Equals(obj.SegmentoID));
                retorno.Add("Segmento: " + segmento.Descricao);
            }
            contacliente.SegmentoID = obj.SegmentoID;

            if (contacliente.Apelido != obj.Apelido)
            {
                retorno.Add("Apelido: " + obj.Apelido);
            }
            contacliente.Apelido = obj.Apelido;

            if (contacliente.Endereco != obj.Endereco)
            {
                retorno.Add("Endereço: " + obj.Endereco);
            }
            contacliente.Endereco = obj.Endereco;

            if (contacliente.Numero != obj.Numero)
            {
                retorno.Add("Número: " + obj.Numero);
            }
            contacliente.Numero = obj.Numero;

            if (contacliente.Email != obj.Email)
            {
                retorno.Add("E-mail: " + obj.Email);
            }
            contacliente.Email = obj.Email;

            if (contacliente.Complemento != obj.Complemento)
            {
                retorno.Add("Complemento: " + obj.Complemento);
            }
            contacliente.Complemento = obj.Complemento;

            if (contacliente.CEP != obj.CEP)
            {
                retorno.Add("CEP: " + obj.CEP);
            }
            contacliente.CEP = obj.CEP;

            if (contacliente.Bairro != obj.Bairro)
            {
                retorno.Add("Bairro: " + obj.Bairro);
            }
            contacliente.Bairro = obj.Bairro;

            if (contacliente.Cidade != obj.Cidade)
            {
                retorno.Add("Cidade: " + obj.Cidade);
            }
            contacliente.Cidade = obj.Cidade;

            if (contacliente.UF != obj.UF)
            {
                retorno.Add("UF: " + obj.UF);
            }
            contacliente.UF = obj.UF;

            if (contacliente.ClientePremium != obj.ClientePremium)
            {
                retorno.Add("Cliente Premium: " + (obj.ClientePremium ? "Sim" : "Não"));
            }
            contacliente.ClientePremium = obj.ClientePremium;

            if (contacliente.Telefone != obj.Telefone)
            {
                retorno.Add("Telefone: " + obj.Telefone);
            }
            contacliente.Telefone = obj.Telefone;

            if (contacliente.TipoClienteID != obj.TipoClienteID)
            {
                var tipoCliente = await _untUnitOfWork.TipoClienteRepository.GetAsync(c => c.ID.Equals(obj.TipoClienteID));
                retorno.Add("Tipo Cliente: " + tipoCliente.Nome);
            }
            contacliente.TipoClienteID = obj.TipoClienteID;

            if (contacliente.AdiantamentoLC != obj.AdiantamentoLC)
            {
                retorno.Add("Considerar Adiantamentos Como LC: " + (obj.AdiantamentoLC ? "Sim" : "Não"));
            }
            contacliente.AdiantamentoLC = obj.AdiantamentoLC;

            if (contacliente.LiberacaoManual != obj.LiberacaoManual)
            {
                retorno.Add("Apenas Liberações Manuais: " + (obj.LiberacaoManual ? "Sim" : "Não"));
            }
            contacliente.LiberacaoManual = obj.LiberacaoManual;

            if (contacliente.TOP10 != obj.TOP10)
            {
                retorno.Add("Cliente TOP 10: " + (obj.TOP10 ? "Sim" : "Não"));
            }
            contacliente.TOP10 = obj.TOP10;

            if (contacliente.DataNascimentoFundacao != obj.DataNascimentoFundacao)
            {
                retorno.Add("Nascimento/Fundação: " + obj.DataNascimentoFundacao.ToShortDateString());
            }
            contacliente.DataNascimentoFundacao = Convert.ToDateTime(obj.DataNascimentoFundacao);

            contacliente.DataAlteracao = DateTime.Now;
            contacliente.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            contacliente.PropostaAlcadaID = obj.PropostaAlcadaID;
            contacliente.PropostaAlcadaStatusID = obj.PropostaAlcadaStatusID;

            _untUnitOfWork.ContaClienteRepository.Update(contacliente);

            _untUnitOfWork.Commit();

            return String.Join(" - ", retorno);
        }

        public async Task<bool> UpdateAsyncManualLock(BloqueioManualContaClienteDto obj)
        {
            var contacliente = await _untUnitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(obj.ID));
            contacliente.ID = obj.ID;
            contacliente.BloqueioManual = obj.BloqueioManual;
            contacliente.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            contacliente.DataAlteracao = DateTime.Now;

            _untUnitOfWork.ContaClienteRepository.Update(contacliente);

            var processamentocarteira = new ProcessamentoCarteiraDto();
            var usuario = await _untUnitOfWork.UsuarioRepository.GetAsync(c => c.ID.Equals(obj.UsuarioIDAlteracao));
            processamentocarteira.ID = Guid.NewGuid();
            processamentocarteira.DataHora = DateTime.Now;
            processamentocarteira.Status = 2;

            processamentocarteira.Motivo = obj.BloqueioManual ? "Bloqueio de Conta Cliente" : "Liberação de Bloqueio da Conta Cliente";

            var mensagem = obj.BloqueioManual ? "bloqueiada" : "liberada";

            processamentocarteira.Detalhes =
                $"Conta Cliente do código {contacliente.CodigoPrincipal} foi {mensagem} pelo usuário {usuario.Nome}";

            processamentocarteira.Cliente = contacliente.CodigoPrincipal;
            processamentocarteira.EmpresaID = obj.EmpresaID;

            _untUnitOfWork.ProcessamentoCarteiraRepository.Insert(processamentocarteira.MapTo<ProcessamentoCarteira>());

            return _untUnitOfWork.Commit();
        }

        public async Task<IEnumerable<TitulosGrupoEconomicoMembrosDto>> TitulosGrupoEconomicoMembroContaCliente(Guid contaClienteId, string empresa)
        {
            var titulosMembros = await _untUnitOfWork.ContaClienteRepository.TitulosGrupoEconomicoMembroContaCliente(contaClienteId, empresa);
            return Mapper.Map<IEnumerable<TitulosGrupoEconomicoMembrosDto>>(titulosMembros);
        }

        public async Task<PropostaAtualDto> ValidProposalReturn(Guid id, string empresa)
        {
            var retorno = new PropostaAtualDto();

            var propostaAlcada = await _untUnitOfWork.PropostaAlcadaComercial.GetAllFilterAsync(c => c.ContaClienteID.Equals(id) && c.EmpresaID.Equals(empresa));
            var ultimaproposta = propostaAlcada.OrderByDescending(c => c.NumeroInternoProposta).FirstOrDefault();

            if (ultimaproposta != null)
            {
                retorno.PropostaAlcadaID = ultimaproposta.ID;
                retorno.PropostaAlcadaStatus = ultimaproposta.PropostaCobrancaStatusID;
            }

            var propostalc = await _untUnitOfWork.PropostaLCRepository.GetAllFilterAsync(c => c.ContaClienteID.Equals(id) && c.EmpresaID.Equals(empresa));
            var ultimapropostaLC = propostalc.OrderByDescending(c => c.NumeroInternoProposta).FirstOrDefault();

            if (ultimapropostaLC != null)
            {
                retorno.PropostaLCID = ultimapropostaLC.ID;
                retorno.PropostaLCStatus = ultimapropostaLC.PropostaLCStatusID;
            }

            var propostalcadicional = await _untUnitOfWork.PropostaLCAdicionalRepository.GetAllFilterAsync(c => c.ContaClienteID.Equals(id) && c.EmpresaID.Equals(empresa));
            var ultimapropostalcadicional = propostalcadicional.OrderByDescending(c => c.NumeroInternoProposta).FirstOrDefault();

            if (ultimapropostalcadicional != null)
            {
                retorno.PropostaLCAdicionalID = ultimapropostalcadicional.ID;
                retorno.PropostaLCAdicionalStatus = ultimapropostalcadicional.PropostaLCStatusID;
            }

            return retorno;
        }

        public async Task<bool> ReavaliarContaCliente(Guid id, string empresa)
        {
            var contaCliente = await _untUnitOfWork.ContaClienteRepository.GetAsync(c => c.ID == id);

            ProcessamentoCarteira pc = new ProcessamentoCarteira()
            {
                ID = Guid.NewGuid(),
                Cliente = contaCliente.CodigoPrincipal,
                DataHora = DateTime.Now,
                EmpresaID = empresa,
                Motivo = "Reavaliação Manual pela Conta Cliente",
                Detalhes = "Reavaliação Manual pela Conta Cliente",
                Status = 2

            };

            _untUnitOfWork.ProcessamentoCarteiraRepository.Insert(pc);            

            return _untUnitOfWork.Commit();
        }
    }
}