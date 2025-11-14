using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.AppService
{
    public class AppServiceTituloComentario : IAppServiceTituloComentario
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceTituloComentario(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<TituloComentarioDto> GetAsync(Expression<Func<TituloComentarioDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TituloComentarioDto>> GetAllFilterAsync(Expression<Func<TituloComentarioDto, bool>> expression)
        {
            var comentario = await _unitOfWork.TituloComentarioRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<TituloComentario, bool>>>(expression));
            return Mapper.Map<IEnumerable<TituloComentarioDto>>(comentario);
        }

        public async Task<IEnumerable<TituloComentarioDto>> GetAllAsync()
        {
            var comentario = await _unitOfWork.TituloComentarioRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<TituloComentarioDto>>(comentario);
        }

        public bool Insert(TituloComentarioDto obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(TituloComentarioDto obj)
        {
            throw new NotImplementedException();
        }
    }
}
