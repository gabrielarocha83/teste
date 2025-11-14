using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;
using Yara.Domain.Repository;
using System.Linq;

namespace Yara.AppService
{
    public class AppServicePropostaJuridico : IAppServicePropostaJuridico
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServicePropostaJuridico(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PropostaJuridicoDto> GetAsync(Expression<Func<PropostaJuridicoDto, bool>> expression)
        {
            var proposta = await _unitOfWork.PropostaJuridicoRepository.GetAsync(Mapper.Map<Expression<Func<PropostaJuridico, bool>>>(expression));
            return Mapper.Map<PropostaJuridicoDto>(proposta);
        }

        public Task<IEnumerable<PropostaJuridicoDto>> GetAllFilterAsync(Expression<Func<PropostaJuridicoDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PropostaJuridicoDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public bool Insert(PropostaJuridicoDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(PropostaJuridicoDto obj)
        {
            var proposta = await _unitOfWork.PropostaJuridicoRepository.GetAsync(c => c.ID.Equals(obj.ID));
            proposta.ParecerCobranca = obj.ParecerCobranca;
            proposta.Pedidos = obj.Pedidos;
            proposta.NotaFiscal = obj.NotaFiscal;
            proposta.Comprovante = obj.Comprovante;
            proposta.DataAlteracao = DateTime.Now;
            _unitOfWork.PropostaJuridicoRepository.Update(proposta);
            return _unitOfWork.Commit();
        }

        public Task<bool> InsertAsync(PropostaJuridicoDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<PropostaJuridicoDto> CriaProposta(Guid contaClienteId, Guid usuarioId, string empresaId)
        {
            var propostaInsert = await _unitOfWork.PropostaJuridicoRepository.CriaPropostaJuridica(contaClienteId, usuarioId, empresaId);
            return Mapper.Map<PropostaJuridicoDto>(propostaInsert);
        }

        public async Task<bool> UpdateWithHistoric(PropostaJuridicoDto obj, ContaClienteDto clienteDto)
        {
            var proposta = await _unitOfWork.PropostaJuridicoRepository.GetAsync(c => c.ID.Equals(obj.ID));
            proposta.ParecerCobranca = obj.ParecerCobranca;
            proposta.Pedidos = obj.Pedidos;
            proposta.NotaFiscal = obj.NotaFiscal;
            proposta.Comprovante = obj.Comprovante;
            proposta.PropostaJuridicoStatus = "AP";
            proposta.DataAlteracao = DateTime.Now;
            _unitOfWork.PropostaJuridicoRepository.Update(proposta);
            
            // Incluir Conceito J na Conta Cliente
            var conceito = await _unitOfWork.ConceitoCobrancaRepository.GetAsync(c => c.Nome.Equals("J") && c.Ativo);
            var contaClienteFinanceiro = await _unitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.ContaClienteID.Equals(clienteDto.ID) && c.EmpresasID.Equals(obj.EmpresaID));
            if (contaClienteFinanceiro != null)
            {
                contaClienteFinanceiro.Conceito = false;
                contaClienteFinanceiro.ConceitoCobrancaID = conceito.ID;
                _unitOfWork.ContaClienteFinanceiroRepository.UpdateConceito(contaClienteFinanceiro);
            }
            else
            {
                contaClienteFinanceiro.ContaClienteID = clienteDto.ID;
                contaClienteFinanceiro.EmpresasID = obj.EmpresaID;
                contaClienteFinanceiro.UsuarioIDCriacao = obj.UsuarioIDCriacao;
                contaClienteFinanceiro.DataCriacao = DateTime.Now;
                _unitOfWork.ContaClienteFinanceiroRepository.Insert(contaClienteFinanceiro);
            }

            var pc = new ProcessamentoCarteira();
            pc.ID = Guid.NewGuid();
            pc.Cliente = clienteDto.CodigoPrincipal;
            pc.DataHora = DateTime.Now;
            pc.Status = 2;
            pc.Motivo = "Cliente Enviado ao Juridico";
            pc.Detalhes = "Proposta " + obj.ID + " criado e enviado ao juridico";
            pc.EmpresaID = obj.EmpresaID;
            _unitOfWork.ProcessamentoCarteiraRepository.Insert(pc);

            try
            {
                var commit = _unitOfWork.Commit();

                if (commit)
                { // Envia titulos para o SAP
                    var rfc = new AppServiceRFCSap(_unitOfWork);
                    await rfc.EnvioTitulosJuridico(obj.ID);
                }

                return commit;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<ControleCobrancaEnvioJuridicoDto>> BuscaControleCobranca(string empresaId, string diretoriaCodigo)
        {
            var retorno = new List<ControleCobrancaEnvioJuridico>();

            try
            {
                var controleMesAno = await _unitOfWork.PropostaJuridicoRepository.BuscaControleCobranca(empresaId, diretoriaCodigo);
                retorno.AddRange(controleMesAno);

                var controleAno = controleMesAno.GroupBy(c => c.Ano).Select(g => new ControleCobrancaEnvioJuridico()
                {
                    Mes = String.Empty,
                    Ano = g.Key,
                    Quantidade = g.Sum(c => c.Quantidade),
                    Valor = g.Sum(c => c.Valor),
                }).ToList();
                retorno.AddRange(controleAno);
            }
            catch(Exception e)
            {
                throw e;
            }

            return Mapper.Map<IEnumerable<ControleCobrancaEnvioJuridicoDto>>(retorno);
        }

        public async Task<bool> Inactive(Guid id)
        {
            var proposta = await _unitOfWork.PropostaJuridicoRepository.GetAsync(c => c.ID.Equals(id));
            proposta.PropostaJuridicoStatus = "CA";
            _unitOfWork.PropostaJuridicoRepository.Update(proposta);

            //Altera para NULL os titulos enviados para o Juridico
            var propostaTitulos = await _unitOfWork.PropostaJuridicoTituloRepository.GetAllFilterAsync(c => c.PropostaJuridicoID.Equals(proposta.ID));
            foreach (var item in propostaTitulos)
            {
                var titulo =
                    await _unitOfWork.TituloRepository.GetAsync(c => c.NumeroDocumento == item.NumeroDocumento &&
                                                                     c.Linha == item.Linha &&
                                                                     c.AnoExercicio == item.AnoExercicio && 
                                                                     c.Empresa == item.Empresa);
                if (titulo != null)
                {
                    titulo.PropostaStatus = null;
                }
                _unitOfWork.TituloRepository.Update(titulo);
            }

            try
            {
                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
