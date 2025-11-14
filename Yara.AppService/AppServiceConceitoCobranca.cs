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

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.AppService
{
    public class AppServiceConceitoCobranca : IAppServiceConceitoCobranca
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceConceitoCobranca(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public  async Task<ConceitoCobrancaDto> GetAsync(Expression<Func<ConceitoCobrancaDto, bool>> expression)
        {
            var conceitocobranca = await _unitOfWork.ConceitoCobrancaRepository.GetAsync(Mapper.Map<Expression<Func<ConceitoCobranca, bool>>>(expression));

            return AutoMapper.Mapper.Map<ConceitoCobrancaDto>(conceitocobranca);
        }

        public async Task<IEnumerable<ConceitoCobrancaDto>> GetAllFilterAsync(Expression<Func<ConceitoCobrancaDto, bool>> expression)
        {
            var conceitocobranca = await _unitOfWork.ConceitoCobrancaRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<ConceitoCobranca, bool>>>(expression));

            return AutoMapper.Mapper.Map<IEnumerable<ConceitoCobrancaDto>>(conceitocobranca);
        }

        public async Task<IEnumerable<ConceitoCobrancaDto>> GetAllAsync()
        {
            var conceitocobrancas = await _unitOfWork.ConceitoCobrancaRepository.GetAllAsync();
            
            // var grupos = await _unitOfWork.GrupoRepository.GetAllPaginationAsync(null,page,skip,false);
            return AutoMapper.Mapper.Map<IEnumerable<ConceitoCobrancaDto>>(conceitocobrancas);
         
        }

        public bool Insert(ConceitoCobrancaDto obj)
        {
            var conceitocobranca = obj.MapTo<ConceitoCobranca>();
            
            conceitocobranca.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            conceitocobranca.DataCriacao = DateTime.Now;
            _unitOfWork.ConceitoCobrancaRepository.Insert(conceitocobranca);
            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(ConceitoCobrancaDto obj)
        {
            var conceitocobranca = obj.MapTo<ConceitoCobranca>();
            var exists = await _unitOfWork.ConceitoCobrancaRepository.GetAsync(c => c.Nome.Equals(obj.Nome));
            if (exists != null) throw new Exception("Conceito de cobrança já cadastrado");
            conceitocobranca.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            conceitocobranca.DataCriacao = DateTime.Now;
            _unitOfWork.ConceitoCobrancaRepository.Insert(conceitocobranca);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(ConceitoCobrancaDto obj)
        {
            var conceitocobranca = await _unitOfWork.ConceitoCobrancaRepository.GetAsync(c => c.ID.Equals(obj.ID));
            conceitocobranca.Nome = obj.Nome;
            conceitocobranca.Descricao = obj.Descricao;
            conceitocobranca.Ativo = obj.Ativo;
            conceitocobranca.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            conceitocobranca.DataAlteracao = DateTime.Now;
            _unitOfWork.ConceitoCobrancaRepository.Update(conceitocobranca);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id)
        {
            var conceitocobranca = await _unitOfWork.ConceitoCobrancaRepository.GetAsync(c => c.ID.Equals(id));
            conceitocobranca.Ativo = false;
            _unitOfWork.ConceitoCobrancaRepository.Update(conceitocobranca);
            return _unitOfWork.Commit();
        }
    }
}