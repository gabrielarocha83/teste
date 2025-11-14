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
    public class AppServiceOrdemVenda : IAppServiceOrdemVenda
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppServiceEnvioEmail _email;
        private readonly IAppServiceUsuario _usuario;

        public AppServiceOrdemVenda(IUnitOfWork unitOfWork, IAppServiceEnvioEmail email, IAppServiceUsuario usuario)
        {
            _unitOfWork = unitOfWork;
            _email = email;
            _usuario = usuario;
        }

        public async Task<IEnumerable<BuscaOrdemVendaDto>> ConsultaOrdem()
        {
            try
            {
                var ordem = await _unitOfWork.OrdemVendaRepository.ConsultaOrdem();
                return ordem.MapTo<IEnumerable<BuscaOrdemVendaDto>>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<OrdemVendaDto> GetAsync(Expression<Func<OrdemVendaDto, bool>> expression)
        {
            var ordem = await _unitOfWork.OrdemVendaRepository.GetAsync(expression.MapTo<Expression<Func<OrdemVenda, bool>>>());
            return ordem.MapTo<OrdemVendaDto>();
        }

        public Task<IEnumerable<OrdemVendaDto>> GetAllFilterAsync(Expression<Func<OrdemVendaDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrdemVendaDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public bool Insert(OrdemVendaDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(OrdemVendaDto obj)
        {
            var ordem = await _unitOfWork.OrdemVendaRepository.GetAsync(c => c.Numero.Equals(obj.Numero));
            ordem.BloqueioRemessa = obj.BloqueioRemessa;
            _unitOfWork.OrdemVendaRepository.Update(ordem);
            return _unitOfWork.Commit();
        }

        //public async Task<bool> UpdateAsync(StatusOrdemVendasDto obj)
        //{
        //    //var ordem = await _unitOfWork.OrdemVendaRepository.GetAsync(c => c.Numero.Equals(obj.Numero));
        //    //ordem.Numero = obj.Numero;
        //    //ordem.Pagador = obj.Pagador;
        //    //ordem.BloqueioRemessa = obj.BloqueioRemessa;

        //    //_unitOfWork.OrdemVendaRepository.Update(ordem);
        //    //return _unitOfWork.Commit();
        //}

        private Guid InsertSolicitante(CriaFluxoOrdemVendaDto fluxo)
        {
            var solicitanteId = Guid.NewGuid();
            var nextNumber = _unitOfWork.OrdemVendaRepository.GetMaxNumeroInterno();
            var solicitante = new SolicitanteFluxo
            {
                ID = solicitanteId,
                UsuarioIDCriacao = fluxo.UsuarioID,
                DataCriacao = DateTime.Now,
                AcompanharProposta = fluxo.AcompanharProposta,
                EmpresasId = fluxo.EmpresaID,
                Comentario = fluxo.Comentario,
                Total = fluxo.Total,
                ContaClienteID = fluxo.ContaClienteID,
                NumeroInternoProposta = nextNumber
            };
            _unitOfWork.OrdemVendaRepository.InsertSolicitante(solicitante);

            return solicitanteId;
        }

        public async Task<bool> GerarFluxo(CriaFluxoOrdemVendaDto fluxo, string URL)
        {
            var id = InsertSolicitante(fluxo);

            var segmentoid = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(fluxo.ContaClienteID));

            var fluxos = await _unitOfWork.FluxoLiberacaoOrdemVendaRepository.GetAllFilterAsync(c => c.SegmentoID == segmentoid.SegmentoID.Value && c.Ativo && c.EmpresaID.Equals(fluxo.EmpresaID));
            if (!fluxos.Any())
                throw new ArgumentException($"Verifique as configurações de Fluxo de Ordem de venda, pois, não possuí configurações para este segmento");

            var nivel01 = fluxos.FirstOrDefault(c => c.Nivel.Equals(1));

            var responsavel = await _unitOfWork.EstruturaPerfilUsuarioRepository.GetAsync(c => c.CodigoSap.Equals(fluxo.CodigoSap) && c.PerfilId.Equals(nivel01.PerfilID));

            if (responsavel.UsuarioId == null)
                throw new ArgumentException($"A estrutura {fluxo.CodigoSap} não possuí responsaveis para o fluxo de ordem de venda");

            // Buscar itens de faturamento antecipado
            if (fluxo.Ordens.Any(o => o.Divisao == 0))
            {
                var ordensFatAntecipado = fluxo.Ordens.Where(o => o.Divisao == 0).Select(o => o.OrdemVendaNumero+o.ItemOrdemVenda ).Distinct().ToList();
                var itensFatAntecipado = await _unitOfWork.DivisaoRemessaRepository.GetAllFilterAsync(drx => ordensFatAntecipado.Contains(drx.OrdemVendaNumero + drx.ItemOrdemVendaItem.ToString()));

                fluxo.Ordens.RemoveAll(o => o.Divisao == 0);
                itensFatAntecipado.ToList().ForEach(ifa => fluxo.Ordens.Add(new OrdemVendaFluxoDto() { Divisao = ifa.Divisao, ItemOrdemVenda = ifa.ItemOrdemVendaItem, OrdemVendaNumero = ifa.OrdemVendaNumero }));
            }

            fluxo.Ordens.ForEach(c => c.EmpresasId = fluxo.EmpresaID);
            fluxo.Ordens.ForEach(c => c.UsuarioIDCriacao = fluxo.UsuarioID);
            fluxo.Ordens.ForEach(c => c.SolicitanteFluxoID = id);

            _unitOfWork.OrdemVendaFluxoRepository.InsertRange(fluxo.Ordens.MapTo<List<OrdemVendaFluxo>>());
            var statuspendente = await _unitOfWork.StatusOrdemVendaRepository.GetAsync(c => c.Status.Equals("OP"));

            var statuanalise = await _unitOfWork.StatusOrdemVendaRepository.GetAsync(c => c.Status.Equals("EA"));
            var existfluxo = fluxos.Where(c => c.ValorAte <= fluxo.Total).OrderBy(c => c.Nivel).ToList();

            if (existfluxo.Count == 0)
                existfluxo = new List<FluxoLiberacaoOrdemVenda> { nivel01 };

            var liberar = new LiberacaoOrdemVendaFluxo();

            foreach (var item in existfluxo)
            {
                if (item.Nivel.Equals(1))
                {
                    liberar = new LiberacaoOrdemVendaFluxo()
                    {
                        ID = Guid.NewGuid(),
                        UsuarioID = responsavel.UsuarioId,
                        DataCriacao = DateTime.Now,
                        EmpresasId = fluxo.EmpresaID,
                        CodigoSap = fluxo.CodigoSap,
                        FluxoLiberacaoOrdemVendaID = item.ID,
                        SolicitanteFluxoID = id,
                        StatusOrdemVendasID = statuanalise.ID,
                        UsuarioIDCriacao = fluxo.UsuarioID
                    };
                }
                else
                {
                    liberar = new LiberacaoOrdemVendaFluxo()
                    {
                        ID = Guid.NewGuid(),
                        DataCriacao = DateTime.Now,
                        UsuarioID = null,
                        EmpresasId = fluxo.EmpresaID,
                        FluxoLiberacaoOrdemVendaID = item.ID,
                        CodigoSap = fluxo.CodigoSap,
                        SolicitanteFluxoID = id,
                        StatusOrdemVendasID = statuspendente.ID,
                        UsuarioIDCriacao = fluxo.UsuarioID
                    };
                }
                _unitOfWork.LiberacaoOrdemVendaFluxoRepository.Insert(liberar);
            }

            try
            {
                var email = new AppServiceEnvioEmail(_unitOfWork);
                await email.SendMailLiberacaoManual(id,responsavel.Usuario.MapTo<UsuarioDto>(), fluxo.Comentario, fluxo.EmpresaID, URL);
            }
            catch
            {

            }

            var retorno = _unitOfWork.Commit();

            _unitOfWork.DivisaoRemessaRepository.UpdateDivisao(id);

            return retorno;
        }

        public bool GerarBloqueioFluxo(CriaFluxoOrdemVendaDto fluxo)
        {
            var id = InsertSolicitante(fluxo);

            fluxo.Ordens.ForEach(c => c.EmpresasId = fluxo.EmpresaID);
            fluxo.Ordens.ForEach(c => c.UsuarioIDCriacao = fluxo.UsuarioID);
            fluxo.Ordens.ForEach(c => c.SolicitanteFluxoID = id);

            _unitOfWork.OrdemVendaFluxoRepository.InsertRange(fluxo.Ordens.MapTo<List<OrdemVendaFluxo>>());

            var retorno = _unitOfWork.Commit();

            var ordens = _unitOfWork.OrdemVendaFluxoRepository.BloqueioFluxoOrdem(id, fluxo.UsuarioID, fluxo.EmpresaID);

            return retorno;
        }

        public async Task<OrdemVendaDto> GetOrdemAsync(string Documento)
        {
            var ordem = await _unitOfWork.OrdemVendaRepository.GetOrdemAsync(Documento);
            var ordemDto = ordem.MapTo<OrdemVendaDto>();

            // Add descrição de Ctc e Gc
            var estruturaCtc = await _unitOfWork.EstruturaComercialRepository.GetAsync(c => c.CodigoSap.Equals(ordem.CodigoCtc));
            if (estruturaCtc != null)
                ordemDto.NomeCtc = estruturaCtc.Nome;

            var estruturaGc = await _unitOfWork.EstruturaComercialRepository.GetAsync(c => c.CodigoSap.Equals(ordem.CodigoGc));
            if (estruturaGc != null)
                ordemDto.NomeGc = estruturaGc.Nome;

            return ordemDto;
        }

        public async Task<SolicitanteFluxoDto> GetSolicitanteAsync(Guid SolicitanteID, string EmpresaID)
        {
            var solicitante = await _unitOfWork.SolicitanteFluxoRepository.GetAsync(c => c.ID.Equals(SolicitanteID));

            var solicitantedto = solicitante.MapTo<SolicitanteFluxoDto>();

            if (solicitantedto == null)
                return null;

            var dadosfinanceiros = await _unitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.ContaClienteID.Equals(solicitantedto.ContaClienteID) && c.EmpresasID.Equals(EmpresaID));
            solicitantedto.Exposicao = dadosfinanceiros?.Exposicao ?? 0;

            var ordens = await _unitOfWork.OrdemVendaFluxoRepository.OrdensAprovacao(SolicitanteID, EmpresaID);
            solicitantedto.Ordens = ordens.MapTo<List<BuscaOrdemVendasAVistaDto>>();

            var usuario = await _unitOfWork.UsuarioRepository.GetAsync(c => c.ID.Equals(solicitante.UsuarioIDCriacao));
            solicitantedto.NomeSolicitante = usuario.Nome;

            //Busca Informações adicionais
            var info = await _unitOfWork.SolicitanteFluxoRepository.BuscaInformacaoCliente(solicitantedto.ID, EmpresaID);
            solicitantedto.NomeCliente = info.Nome;
            solicitantedto.ApelidoCliente = info.Apelido;
            solicitantedto.DocumentoCliente = info.Documento;
            solicitantedto.CodigoCliente = info.CodigoPrincipal;
            solicitantedto.PendenciaSerasa = info.PendenciaSerasa;
            solicitantedto.Conceito = info.Conceito;
            solicitantedto.DescricaoConceito = info.DescricaoConceito;
            solicitantedto.BloqueioManual = info.BloqueioManual;
            solicitantedto.NomeCtc = info.NomeCtc;
            solicitantedto.NomeSupervisor = info.NomeSupervisor;
            solicitantedto.NomeDiretoria = info.NomeDiretoria;
            solicitantedto.DividaAtiva = info.DividaAtiva;
            solicitantedto.LC = info.LC;
            solicitantedto.Vigencia = info.Vigencia;
            solicitantedto.VigenciaFim = info.VigenciaFim;

            return solicitantedto;
        }

        public async Task<IEnumerable<LiberacaoOrdemVendaFluxoDto>> GetFluxoSolicitanteAsync(Guid SolicitanteID, string EmpresaID)
        {
            var fluxo = await
                _unitOfWork.LiberacaoOrdemVendaFluxoRepository.GetAllFilterAsync(
                    c => c.SolicitanteFluxoID.Equals(SolicitanteID) && c.EmpresasId.Equals(EmpresaID));

            return fluxo.OrderBy(c => c.FluxoLiberacaoOrdemVenda.Nivel).MapTo<IEnumerable<LiberacaoOrdemVendaFluxoDto>>();
        }

        public async Task<bool> SolicitarBloqueioCarregamento(Guid usuarioId, string empresaId, List<SolicitacaoBloqueioRemessaDto> remessas)
        {
            List<string> drLista = new List<string>();
            remessas.ForEach(r => drLista.Add(r.Numero + "|" + r.Item.ToString() + "|" + r.Divisao.ToString()));

            var divisoesRemessa = await _unitOfWork.DivisaoRemessaRepository.GetAllFilterAsync(dr => drLista.Contains(dr.OrdemVendaNumero+"|"+dr.ItemOrdemVendaItem.ToString() + "|" + dr.Divisao.ToString()));

            if (divisoesRemessa != null && divisoesRemessa.Any())
            {
                // Buscar código do cliente pagador
                var numeroPrimeiraOrdemVenda = divisoesRemessa.First().OrdemVendaNumero;
                var primeiraOrdemVenda = await _unitOfWork.OrdemVendaRepository.GetAsync(ov => ov.Numero.Equals(numeroPrimeiraOrdemVenda));

                // Criar Processamento Carteira (Reavaliar Carteira Cliente)
                ProcessamentoCarteira pc = new ProcessamentoCarteira();
                pc.ID = Guid.NewGuid();
                pc.Cliente = primeiraOrdemVenda.Pagador;
                pc.DataHora = DateTime.Now;
                pc.Status = 2;
                pc.Motivo = string.Format("Divisões de remessa bloqueadas para carregamento.");
                pc.Detalhes = pc.Motivo;
                pc.EmpresaID = empresaId;
                _unitOfWork.ProcessamentoCarteiraRepository.Insert(pc);

                foreach (var dr in divisoesRemessa)
                {
                    if (dr.BloqueioCarregamento == false)
                    {
                        // Criar Registro de Envio ao SAP
                        BloqueioLiberacaoCarregamento blc = new BloqueioLiberacaoCarregamento();
                        blc.ID = Guid.NewGuid();
                        blc.ProcessamentoCarteiraID = pc.ID;
                        blc.Divisao = dr.Divisao;
                        blc.Item = dr.ItemOrdemVendaItem;
                        blc.Numero = dr.OrdemVendaNumero;
                        blc.EnviadoSAP = false;
                        blc.Bloqueada = true;
                        blc.UsuarioIDCriacao = usuarioId;
                        blc.DataCriacao = DateTime.Now;
                        _unitOfWork.BloqueioLiberacaoCarregamentoRepository.Insert(blc);

                        LogDivisaoRemessaLiberacao ldrl = new LogDivisaoRemessaLiberacao();
                        ldrl.ID = Guid.NewGuid();
                        ldrl.ProcessamentoCarteiraID = pc.ID;
                        ldrl.OrdemVendaNumero = dr.OrdemVendaNumero;
                        ldrl.OrdemVendaItem = dr.ItemOrdemVendaItem;
                        ldrl.OrdemDivisao = dr.Divisao;
                        ldrl.Restricao = "Bloqueada para Carregamento";
                        ldrl.UsuarioIDCriacao = usuarioId;
                        ldrl.DataCriacao = DateTime.Now;
                        _unitOfWork.LogDivisaoRemessaLiberacaoRepository.Insert(ldrl);

                        // Alterar Status da Divisao de Remessa
                        dr.BloqueioCarregamento = true;
                        _unitOfWork.DivisaoRemessaRepository.Update(dr);
                    }
                }

                _unitOfWork.Commit();

                return true;
            }

            return false;
        }

        public async Task<bool> LiberarBloqueioCarregamento(Guid usuarioId, string empresaId, List<SolicitacaoBloqueioRemessaDto> remessas)
        {

            List<string> drLista = new List<string>();
            remessas.ForEach(r => drLista.Add(r.Numero + "|" + r.Item.ToString() + "|" + r.Divisao.ToString()));

            var divisoesRemessa = await _unitOfWork.DivisaoRemessaRepository.GetAllFilterAsync(dr => drLista.Contains(dr.OrdemVendaNumero + "|" + dr.ItemOrdemVendaItem.ToString() + "|" + dr.Divisao.ToString()));

            if (divisoesRemessa != null && divisoesRemessa.Any())
            {

                // Buscar código do cliente pagador
                var numeroPrimeiraOrdemVenda = divisoesRemessa.First().OrdemVendaNumero;
                var primeiraOrdemVenda = await _unitOfWork.OrdemVendaRepository.GetAsync(ov => ov.Numero.Equals(numeroPrimeiraOrdemVenda));

                // Criar Processamento Carteira (Reavaliar Carteira Cliente)
                ProcessamentoCarteira pc = new ProcessamentoCarteira();
                pc.ID = Guid.NewGuid();
                pc.Cliente = primeiraOrdemVenda.Pagador;
                pc.DataHora = DateTime.Now;
                pc.Status = 2;
                pc.Motivo = string.Format("Divisões de remessa liberadas para carregamento.");
                pc.Detalhes = pc.Motivo;
                pc.EmpresaID = empresaId;
                _unitOfWork.ProcessamentoCarteiraRepository.Insert(pc);

                foreach (var dr in divisoesRemessa)
                {

                    if (dr.BloqueioCarregamento == true)
                    {

                        // Criar Registro de Envio ao SAP
                        BloqueioLiberacaoCarregamento blc = new BloqueioLiberacaoCarregamento();
                        blc.ID = Guid.NewGuid();
                        blc.ProcessamentoCarteiraID = pc.ID;
                        blc.Divisao = dr.Divisao;
                        blc.Item = dr.ItemOrdemVendaItem;
                        blc.Numero = dr.OrdemVendaNumero;
                        blc.EnviadoSAP = false;
                        blc.Bloqueada = false;
                        blc.UsuarioIDCriacao = usuarioId;
                        blc.DataCriacao = DateTime.Now;
                        _unitOfWork.BloqueioLiberacaoCarregamentoRepository.Insert(blc);

                        LogDivisaoRemessaLiberacao ldrl = new LogDivisaoRemessaLiberacao();
                        ldrl.ID = Guid.NewGuid();
                        ldrl.ProcessamentoCarteiraID = pc.ID;
                        ldrl.OrdemVendaNumero = dr.OrdemVendaNumero;
                        ldrl.OrdemVendaItem = dr.ItemOrdemVendaItem;
                        ldrl.OrdemDivisao = dr.Divisao;
                        ldrl.Restricao = "Liberada para Carregamento";
                        ldrl.UsuarioIDCriacao = usuarioId;
                        ldrl.DataCriacao = DateTime.Now;
                        _unitOfWork.LogDivisaoRemessaLiberacaoRepository.Insert(ldrl);

                        // Alterar Status da Divisao de Remessa
                        dr.BloqueioCarregamento = false;
                        _unitOfWork.DivisaoRemessaRepository.Update(dr);

                    }

                }

                _unitOfWork.Commit();

                return true;

            }

            return false;
        }

        public async Task<IEnumerable<LogEnvioOrdensSAPDto>> GetLogEnvioOrdensSAP(string empresaId)
        {
            var logs = await _unitOfWork.OrdemVendaRepository.GetLogEnvioOrdensSAP(empresaId);
            return logs.MapTo<IEnumerable<LogEnvioOrdensSAPDto>>();
        }

        public async Task<IEnumerable<DivisaoRemessaCockPitDto>> BuscaOrdemVenda(Guid ContaClienteID, string EmpresaID)
        {
            var propostas = await _unitOfWork.DivisaoRemessaRepository.GetAllPendencyByAccountClient(ContaClienteID, EmpresaID);
            return propostas.MapTo<IEnumerable<DivisaoRemessaCockPitDto>>();
        }

        public async Task<IEnumerable<BuscaRemessasClienteDto>> BuscaRemessasCliente(Guid accountClientID, string empresaID, string tipoRemessa)
        {
            var remessas = await _unitOfWork.OrdemVendaRepository.GetClientDeliveries(accountClientID, empresaID, tipoRemessa);
            return remessas.MapTo<IEnumerable<BuscaRemessasClienteDto>>();
        }
    }
}
