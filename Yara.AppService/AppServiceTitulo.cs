using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.AppService
{
    public class AppServiceTitulo : IAppServiceTitulo
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceTitulo(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<TituloDto> GetAsync(Expression<Func<TituloDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TituloDto>> GetAllFilterAsync(Expression<Func<TituloDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TituloDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public bool Insert(TituloDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(TituloDto obj)
        {
            var titulo = await _unitOfWork.TituloRepository.GetAsync(c => c.NumeroDocumento.Equals(obj.NumeroDocumento));
            _unitOfWork.TituloRepository.Update(titulo);
            return _unitOfWork.Commit();
        }

        public Task<bool> InsertAsync(TituloDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateList(IEnumerable<TituloDto> obj)
        {
            try
            {
                foreach (var item in obj)
                {
                    var titulo = await _unitOfWork.TituloRepository.GetAsync(c => c.Empresa.Equals(item.Empresa) && c.NumeroDocumento.Equals(item.NumeroDocumento) && c.Linha.Equals(item.Linha) && c.AnoExercicio.Equals(item.AnoExercicio));

                    if (item.IncluiuPefin)
                        titulo.DataPefinInclusao = DateTime.Now;

                    if (item.ExcluiuPefin)
                        titulo.DataPefinExclusao = DateTime.Now;

                    if (item.IncluiuProtesto)
                        titulo.DataProtesto = DateTime.Now;

                    if (item.RealizouProtesto)
                        titulo.DataProtestoRealizado = DateTime.Now;

                    if (item.EmitiuDuplicata)
                    {
                        if (titulo.DataDuplicata == null)
                            titulo.DataDuplicata = DateTime.Now;
                        else
                            titulo.DataTriplicata = DateTime.Now;
                    }

                    titulo.DataUltimaAtualizacao = DateTime.Now;

                    _unitOfWork.TituloRepository.Update(titulo);
                }

                return _unitOfWork.Commit();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> UpdateStatus(TituloAtualizacaoStatus obj, string empresa)
        {
            try
            {
                List<string> clientesReprocessar = new List<string>();

                foreach (var item in obj.TituloAtualizacaoStatusChaves)
                {
                    #region Titulo

                    var titulo = await _unitOfWork.TituloRepository.GetAsync(c => c.NumeroDocumento.Equals(item.NumeroDocumento) && c.Linha.Equals(item.Linha) && c.AnoExercicio.Equals(item.AnoExercicio) && c.Empresa.Equals(item.Empresa));

                    var oldStatus = titulo.StatusCobrancaID;

                    titulo.DataUltimaAtualizacao = DateTime.Now;
                    titulo.StatusCobrancaID = obj.StatusCobrancaID ?? titulo.StatusCobrancaID;
                    titulo.DataPrevisaoPagamento = obj.DataPrevisaoPagamento ?? titulo.DataPrevisaoPagamento;
                    titulo.DataAceite = obj.DataAceite ?? titulo.DataAceite;
                    titulo.TaxaJuros = obj.TaxaJuros ?? titulo.TaxaJuros;

                    if (!string.IsNullOrEmpty(obj.Texto))
                    {
                        titulo.TextoComentario = obj.Texto.Length > 100 ? obj.Texto.Substring(0, 100) : obj.Texto;

                        #region Titulo Comentario

                        var titComentario = new TituloComentario();
                        titComentario.ID = Guid.NewGuid();
                        titComentario.DataCriacao = DateTime.Now;
                        titComentario.NumeroDocumento = item.NumeroDocumento;
                        titComentario.Linha = item.Linha;
                        titComentario.AnoExercicio = item.AnoExercicio;
                        titComentario.Empresa = item.Empresa;
                        titComentario.UsuarioID = obj.UsuarioCriacao;
                        titComentario.UsuarioIDCriacao = obj.UsuarioCriacao;
                        titComentario.Texto = obj.Texto;
                        _unitOfWork.TituloComentarioRepository.Insert(titComentario);

                        #endregion
                    }

                    _unitOfWork.TituloRepository.Update(titulo);

                    #endregion

                    // Se houve mudança de status, reprocessar cliente.
                    if (oldStatus != titulo.StatusCobrancaID && !clientesReprocessar.Contains(titulo.CodigoCliente))
                    {
                        clientesReprocessar.Add(titulo.CodigoCliente);
                    }
                }

                // Marcar clientes para reprocessamento.
                if (clientesReprocessar.Count > 0)
                {
                    foreach (var cli in clientesReprocessar)
                    {
                        var pc = new ProcessamentoCarteira()
                        {
                            ID = Guid.NewGuid(),
                            Cliente = cli,
                            DataHora = DateTime.Now,
                            Status = 2,
                            Motivo = "Status de títulos atualizados pela cobrança.",
                            Detalhes = "Status de títulos atualizados pela cobrança.",
                            EmpresaID = empresa
                        };

                        _unitOfWork.ProcessamentoCarteiraRepository.Insert(pc);
                    }
                }

                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
