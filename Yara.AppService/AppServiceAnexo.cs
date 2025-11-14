using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.AppService
{
    public class AppServiceAnexo : IAppServiceAnexo
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceAnexo(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AnexoDto> GetAsync(Expression<Func<AnexoDto, bool>> expression)
        {
            var anexo = await _unitOfWork.AnexoRepository.GetAsync(Mapper.Map<Expression<Func<Anexo, bool>>>(expression));
            return Mapper.Map<AnexoDto>(anexo);
        }

        public async Task<IEnumerable<AnexoDto>> GetAllFilterAsync(Expression<Func<AnexoDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AnexoDto>> GetAllFilterAsyncEspecifico(AnexoArquivoByPropostaContaClienteDto anexoArquivoByPropostaContaCliente)
        {
            Expression<Func<AnexoDto, bool>> expression = dto => dto.Ativo && !string.IsNullOrEmpty(dto.LayoutsProposta);

            IEnumerable<Anexo> anexo = await _unitOfWork.AnexoRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<Anexo, bool>>>(expression));
            List<Anexo> anexoList = new List<Anexo>();

            Anexo anexo1 = null;
            AnexoArquivo anexoArquivo = null;

            //Verifica o status do anexo: 0 = Invalido, 1 = Não Valido, 2 = Valido e a data de validade
            foreach (var item in anexo)
            {
                anexo1 = new Anexo();
                anexoArquivo = new AnexoArquivo();
                
                if (anexoArquivoByPropostaContaCliente.ContaClienteId != Guid.Empty)
                {
                    anexoArquivo = await _unitOfWork.AnexoArquivoRepository.GetAsync(c => c.AnexoID == item.ID && c.ContaClienteID == anexoArquivoByPropostaContaCliente.ContaClienteId && c.Ativo);
                }
                else if (anexoArquivoByPropostaContaCliente.PropostaId != Guid.Empty)
                {
                    anexoArquivo = await _unitOfWork.AnexoArquivoRepository.GetAsync(c => c.AnexoID.Equals(item.ID) && c.PropostaLCID == anexoArquivoByPropostaContaCliente.PropostaId && c.Ativo);
                    anexoArquivo = anexoArquivo ?? await _unitOfWork.AnexoArquivoRepository.GetAsync(c => c.AnexoID.Equals(item.ID) && c.PropostaLCAdicionalID == anexoArquivoByPropostaContaCliente.PropostaId && c.Ativo);
                }

                if (anexoArquivo != null)
                {
                    if (anexoArquivo.Status == 0 || anexoArquivo.Status == 1 || anexoArquivo.Status == null || anexoArquivo.DataValidade < DateTime.Now)
                    {
                        anexo1.ID = item.ID;
                        anexo1.Descricao = item.Descricao;
                        anexo1.Obrigatorio = item.Obrigatorio;
                        anexo1.Ativo = item.Ativo;
                        anexo1.UsuarioIDCriacao = item.UsuarioIDCriacao;
                        anexo1.UsuarioIDAlteracao = item.UsuarioIDAlteracao;
                        anexo1.DataCriacao = item.DataCriacao;
                        anexo1.DataAlteracao = item.DataAlteracao;
                        anexo1.LayoutsProposta = item.LayoutsProposta;
                        anexo1.CategoriaDocumento = item.CategoriaDocumento;
                        anexoList.Add(anexo1);
                    }
                }
                else
                {
                    anexo1.ID = item.ID;
                    anexo1.Descricao = item.Descricao;
                    anexo1.Obrigatorio = item.Obrigatorio;
                    anexo1.Ativo = item.Ativo;
                    anexo1.UsuarioIDCriacao = item.UsuarioIDCriacao;
                    anexo1.UsuarioIDAlteracao = item.UsuarioIDAlteracao;
                    anexo1.DataCriacao = item.DataCriacao;
                    anexo1.DataAlteracao = item.DataAlteracao;
                    anexo1.LayoutsProposta = item.LayoutsProposta;
                    anexo1.CategoriaDocumento = item.CategoriaDocumento;
                    anexoList.Add(anexo1);
                }
            }

            return Mapper.Map<IEnumerable<AnexoDto>>(anexoList);
        }

        public async Task<IEnumerable<AnexoDto>> GetAllAsync()
        {
            var anexo = await _unitOfWork.AnexoRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<AnexoDto>>(anexo);
        }

        public bool Insert(AnexoDto obj)
        {
            var anexo = obj.MapTo<Anexo>();
            anexo.ID = Guid.NewGuid();
            anexo.DataCriacao = DateTime.Now;
            anexo.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.AnexoRepository.Insert(anexo);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(AnexoDto obj)
        {
            var anexo = await _unitOfWork.AnexoRepository.GetAsync(c => c.ID.Equals(obj.ID));

            anexo.Descricao = obj.Descricao;
            anexo.Obrigatorio = obj.Obrigatorio;
            anexo.LayoutsProposta = obj.LayoutsProposta;
            anexo.Ativo = obj.Ativo;
            anexo.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            anexo.DataAlteracao = obj.DataAlteracao;
            anexo.CategoriaDocumento = obj.CategoriaDocumento;
            _unitOfWork.AnexoRepository.Update(anexo);
            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(AnexoDto obj)
        {
            var anexo = obj.MapTo<Anexo>();
            //var exist = await _unitOfWork.AnexoRepository.GetAsync(c => c.Descricao.Equals(obj.Descricao));

            //if (exist != null)
            //    throw new ArgumentException("A descrição " + obj.Descricao + " já está cadastrada.");

            anexo.DataCriacao = DateTime.Now;
            anexo.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.AnexoRepository.Insert(anexo);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id)
        {
            var anexo = await _unitOfWork.AnexoRepository.GetAsync(c => c.ID.Equals(id));
            anexo.Ativo = false;
            _unitOfWork.AnexoRepository.Update(anexo);
            return _unitOfWork.Commit();
        }
    }
}
