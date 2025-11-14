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

namespace Yara.AppService
{
    public class AppServiceTipoEndividamento : IAppServiceTipoEndividamento
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceTipoEndividamento(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<TipoEndividamentoDto> GetAsync(Expression<Func<TipoEndividamentoDto, bool>> expression)
        {
            var cult = await _unitOfWork.TipoEndividamentoRepository.GetAsync(Mapper.Map<Expression<Func<TipoEndividamento, bool>>>(expression));
            return Mapper.Map<TipoEndividamentoDto>(cult);
        }

        public async Task<IEnumerable<TipoEndividamentoDto>> GetAllFilterAsync(Expression<Func<TipoEndividamentoDto, bool>> expression)
        {
            var cult = await _unitOfWork.TipoEndividamentoRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<TipoEndividamento, bool>>>(expression));
            return Mapper.Map<IEnumerable<TipoEndividamentoDto>>(cult);
        }

        public async Task<IEnumerable<TipoEndividamentoDto>> GetAllAsync()
        {
            var cult = await _unitOfWork.TipoEndividamentoRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<TipoEndividamentoDto>>(cult);
        }

        public bool Insert(TipoEndividamentoDto obj)
        {
            var cult = obj.MapTo<TipoEndividamento>();
            cult.ID = Guid.NewGuid();
            cult.DataCriacao = DateTime.Now;
            cult.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.TipoEndividamentoRepository.Insert(cult);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(TipoEndividamentoDto obj)
        {
            var cult = await _unitOfWork.TipoEndividamentoRepository.GetAsync(c => c.ID.Equals(obj.ID));

            cult.Tipo = obj.Tipo;
            cult.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            cult.Ativo = obj.Ativo;
            _unitOfWork.TipoEndividamentoRepository.Update(cult);
            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(TipoEndividamentoDto obj)
        {
            var cult = obj.MapTo<TipoEndividamento>();
            var exist = await _unitOfWork.TipoEndividamentoRepository.GetAsync(c => c.Tipo.Equals(obj.Tipo));

            if (exist != null)
                throw new Exception("Tipo de Cultura já está cadastrado.");

            cult.ID = Guid.NewGuid();
            cult.DataCriacao = DateTime.Now;
            cult.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.TipoEndividamentoRepository.Insert(cult);
            return _unitOfWork.Commit();
        }
    }
}
