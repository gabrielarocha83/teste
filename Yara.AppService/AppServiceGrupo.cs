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
    public class AppServiceGrupo : IAppServiceGrupo
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceGrupo(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public  async Task<GrupoDto> GetAsync(Expression<Func<GrupoDto, bool>> expression)
        {
            var grupo = await _unitOfWork.GrupoRepository.GetAsync(Mapper.Map<Expression<Func<Grupo, bool>>>(expression));
            return AutoMapper.Mapper.Map<GrupoDto>(grupo);
        }

        public async Task<IEnumerable<GrupoDto>> GetAllFilterAsync(Expression<Func<GrupoDto, bool>> expression)
        {
            var grupo = await _unitOfWork.GrupoRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<Grupo, bool>>>(expression));
            return AutoMapper.Mapper.Map<IEnumerable<GrupoDto>>(grupo);
        }

        public async Task<IEnumerable<GrupoDto>> GetAllAsync()
        {
            var grupos = await _unitOfWork.GrupoRepository.GetAllAsync();
            // var grupos = await _unitOfWork.GrupoRepository.GetAllPaginationAsync(null,page,skip,false);
            return AutoMapper.Mapper.Map<IEnumerable<GrupoDto>>(grupos);
        }

        public bool Insert(GrupoDto obj)
        {
            var grupo = obj.MapTo<Grupo>();
            grupo.UsuarioIDCriacao = obj.UsuarioIDCriacao;

            _unitOfWork.GrupoRepository.Insert(grupo);

            return _unitOfWork.Commit();
        }

        public async Task<bool> InsertAsync(GrupoDto obj)
        {
            var grupo = obj.MapTo<Grupo>();
            grupo.Permissoes.Clear();
            
            grupo.UsuarioIDCriacao = obj.UsuarioIDCriacao;
            grupo.DataCriacao = DateTime.Now;

            foreach (var permissao in obj.Permissoes)
            {
                var objPermissao = await _unitOfWork.PermissaoRepository.GetAsync(c => c.Nome.Equals(permissao.Nome));
                grupo.Permissoes.Add(objPermissao);
            }

            _unitOfWork.GrupoRepository.Insert(grupo);

            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(GrupoDto obj)
        {
            var grupo = await _unitOfWork.GrupoRepository.GetAsync(c => c.ID.Equals(obj.ID));
            grupo.Nome = obj.Nome;
            grupo.Ativo = obj.Ativo;
            grupo.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            grupo.DataAlteracao = DateTime.Now;
            grupo.Permissoes.Clear();

            foreach (var item in obj.Permissoes)
            {
                var permissao = await _unitOfWork.PermissaoRepository.GetAsync(c => c.Nome.Equals(item.Nome));
                grupo.Permissoes.Add(permissao);
            }

            _unitOfWork.GrupoRepository.Update(grupo);

            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id)
        {
            var grupo = await _unitOfWork.GrupoRepository.GetAsync(c => c.ID.Equals(id));
            grupo.Ativo = false;

            _unitOfWork.GrupoRepository.Update(grupo);

            return _unitOfWork.Commit();
        }
    }
}