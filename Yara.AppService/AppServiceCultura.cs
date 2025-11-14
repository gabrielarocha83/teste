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
    public class AppServiceCultura : IAppServiceCultura
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceCultura(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<CulturaDto> GetAsync(Expression<Func<CulturaDto, bool>> expression)
        {
            var cult = await _unitOfWork.CulturaRepository.GetAsync(Mapper.Map<Expression<Func<Cultura, bool>>>(expression));
            return Mapper.Map<CulturaDto>(cult);
        }

        public async Task<IEnumerable<CulturaDto>> GetAllFilterAsync(Expression<Func<CulturaDto, bool>> expression)
        {
            var cult = await _unitOfWork.CulturaRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<Cultura, bool>>>(expression));
            return Mapper.Map<IEnumerable<CulturaDto>>(cult);
        }

        public async Task<IEnumerable<CulturaDto>> GetAllAsync()
        {
            var cult = await _unitOfWork.CulturaRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<CulturaDto>>(cult);
        }

        public bool Insert(CulturaDto obj)
        {
            var cult = obj.MapTo<Cultura>();
            cult.DataCriacao = DateTime.Now;
            cult.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.CulturaRepository.Insert(cult);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(CulturaDto obj)
        {
            var cult = await _unitOfWork.CulturaRepository.GetAsync(c => c.ID.Equals(obj.ID));

            cult.Descricao = obj.Descricao;
            cult.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            cult.Ativo = obj.Ativo;
            cult.UnidadeMedida = obj.UnidadeMedida;
            _unitOfWork.CulturaRepository.Update(cult);
            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(CulturaDto obj)
        {
            var cult = obj.MapTo<Cultura>();
            var exist = await _unitOfWork.CulturaRepository.GetAsync(c => c.Descricao.Equals(obj.Descricao));

            if (exist != null)
                throw new Exception("Cultura já esta cadastrada.");

            cult.DataCriacao = DateTime.Now;
            cult.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.CulturaRepository.Insert(cult);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id)
        {
            var cult = await _unitOfWork.CulturaRepository.GetAsync(c => c.ID.Equals(id));
            cult.Ativo = false;
            _unitOfWork.CulturaRepository.Update(cult);
            return _unitOfWork.Commit();
        }
    }
}
