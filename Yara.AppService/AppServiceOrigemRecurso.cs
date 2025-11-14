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
    public class AppServiceOrigemRecurso : IAppServiceOrigemRecurso
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceOrigemRecurso(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OrigemRecursoDto>> GetAllAsync()
        {
            var origem = await _unitOfWork.OrigemRecursoRepository.GetAllAsync();
            return AutoMapper.Mapper.Map<IEnumerable<OrigemRecursoDto>>(origem);
        }

        public async Task<IEnumerable<OrigemRecursoDto>> GetAllFilterAsync(Expression<Func<OrigemRecursoDto, bool>> expression)
        {
            var origemRecurso = await _unitOfWork.OrigemRecursoRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<OrigemRecurso, bool>>>(expression));
            return AutoMapper.Mapper.Map<IEnumerable<OrigemRecursoDto>>(origemRecurso);
        }

        public async Task<OrigemRecursoDto> GetAsync(Expression<Func<OrigemRecursoDto, bool>> expression)
        {
            var origemRecurso = await _unitOfWork.OrigemRecursoRepository.GetAsync(Mapper.Map<Expression<Func<OrigemRecurso, bool>>>(expression));
            return AutoMapper.Mapper.Map<OrigemRecursoDto>(origemRecurso);
        }

        public bool Insert(OrigemRecursoDto obj)
        {
            var origemRecurso = obj.MapTo<OrigemRecurso>();
            origemRecurso.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            origemRecurso.DataCriacao = DateTime.Now;

            _unitOfWork.OrigemRecursoRepository.Insert(origemRecurso);

            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(OrigemRecursoDto obj)
        {
            var origemRecurso = obj.MapTo<OrigemRecurso>();

            var exists = await _unitOfWork.OrigemRecursoRepository.GetAsync(c => c.Nome.Equals(obj.Nome));
            if (exists != null) throw new Exception("Origem do Recurso já cadastrada");

            origemRecurso.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            origemRecurso.DataCriacao = DateTime.Now;

            _unitOfWork.OrigemRecursoRepository.Insert(origemRecurso);

            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(OrigemRecursoDto obj)
        {
            var origemRecurso = await _unitOfWork.OrigemRecursoRepository.GetAsync(c => c.ID.Equals(obj.ID));
            origemRecurso.Nome = obj.Nome;
            origemRecurso.Ativo = obj.Ativo;
            origemRecurso.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            origemRecurso.DataAlteracao = DateTime.Now;

            _unitOfWork.OrigemRecursoRepository.Update(origemRecurso);

            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id)
        {
            var origemRecurso = await _unitOfWork.OrigemRecursoRepository.GetAsync(c => c.ID.Equals(id));
            origemRecurso.Ativo = false;

            _unitOfWork.OrigemRecursoRepository.Update(origemRecurso);

            return _unitOfWork.Commit();
        }
    }
}