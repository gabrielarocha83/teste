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
    public class AppServiceExperiencia : IAppServiceExperiencia
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceExperiencia(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ExperienciaDto> GetAsync(Expression<Func<ExperienciaDto, bool>> expression)
        {
            var experiencia = await _unitOfWork.ExperienciaRepository.GetAsync(Mapper.Map<Expression<Func<Experiencia, bool>>>(expression));
            return Mapper.Map<ExperienciaDto>(experiencia);
        }

        public async Task<IEnumerable<ExperienciaDto>> GetAllFilterAsync(Expression<Func<ExperienciaDto, bool>> expression)
        {
            var experiencia = await _unitOfWork.ExperienciaRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<Experiencia, bool>>>(expression));
            return Mapper.Map<IEnumerable<ExperienciaDto>>(experiencia);
        }

        public async Task<IEnumerable<ExperienciaDto>> GetAllAsync()
        {
            var experiencia = await _unitOfWork.ExperienciaRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<ExperienciaDto>>(experiencia);
        }

        public bool Insert(ExperienciaDto obj)
        {
            var experiencia = obj.MapTo<Experiencia>();
            experiencia.DataCriacao = DateTime.Now;
            experiencia.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.ExperienciaRepository.Insert(experiencia);
            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(ExperienciaDto obj)
        {
            var experiencia = obj.MapTo<Experiencia>();
            var exist = await _unitOfWork.ExperienciaRepository.GetAsync(c => c.Descricao.Equals(obj.Descricao));

            if (exist != null)
                throw new Exception("Usuário já cadastrado com esse login");
            
            experiencia.DataCriacao = DateTime.Now;
            experiencia.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            _unitOfWork.ExperienciaRepository.Insert(experiencia);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(ExperienciaDto obj)
        {
            var experiencia = await _unitOfWork.ExperienciaRepository.GetAsync(c => c.ID.Equals(obj.ID));
            experiencia.Descricao = obj.Descricao;
            experiencia.Ativo = obj.Ativo;
            experiencia.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            experiencia.DataAlteracao = obj.DataAlteracao;
            _unitOfWork.ExperienciaRepository.Update(experiencia);
            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id)
        {
            var experiencia = await _unitOfWork.ExperienciaRepository.GetAsync(c => c.ID.Equals(id));
            experiencia.Ativo = false;
            _unitOfWork.ExperienciaRepository.Update(experiencia);
            return _unitOfWork.Commit();
        }
    }
}
