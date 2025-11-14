using System;
using System.Collections.Generic;
using System.Linq;
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
    public class AppServicePerfil : IAppServicePerfil
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServicePerfil(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PerfilDto> GetAsync(Expression<Func<PerfilDto, bool>> expression)
        {
            var grupos = await _unitOfWork.PerfilRepository.GetAsync(Mapper.Map<Expression<Func<Perfil, bool>>>(expression));

            return Mapper.Map<PerfilDto>(grupos);
        }

        public async Task<IEnumerable<PerfilDto>> GetAllFilterAsync(Expression<Func<PerfilDto, bool>> expression)
        {
            var grupos = await _unitOfWork.PerfilRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<Perfil, bool>>>(expression));

            return Mapper.Map<IEnumerable<PerfilDto>>(grupos);
        }

        public async Task<IEnumerable<PerfilDto>> GetAllAsync()
        {
            var grupos = await _unitOfWork.PerfilRepository.GetAllFilterAsync(c=>c.Ativo);
            return Mapper.Map<IEnumerable<PerfilDto>>(grupos.OrderBy(c => c.Ordem));
        }

        public Task<IEnumerable<PerfilDto>> GetAllFilterAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(PerfilDto obj)
        {
            var permissao = obj.MapTo<Perfil>();
            permissao.ID = Guid.NewGuid();
            permissao.DataCriacao = DateTime.Now;
            //return _domainService.Insert(grupo);
            _unitOfWork.PerfilRepository.Insert(permissao);
            return  _unitOfWork.Commit();
        }

        public async Task<bool> Update(PerfilDto obj)
        {
            var permissao = await _unitOfWork.PerfilRepository.GetAsync(c => c.ID.Equals(obj.ID));
            permissao.Descricao = obj.Descricao;
            permissao.Ativo = obj.Ativo;
            permissao.DataAlteracao = DateTime.Now;
            permissao.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            //return _domainService.Update(grupo);
            _unitOfWork.PerfilRepository.Update(permissao);
            return  _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id)
        {
            var permissao = await _unitOfWork.PerfilRepository.GetAsync(c=>c.ID.Equals(id));
            permissao.Ativo = false;
            _unitOfWork.PerfilRepository.Update(permissao);
            return  _unitOfWork.Commit();
        }

        public async Task<Guid> GetPerfilID(string descricao)
        {
            return await _unitOfWork.PerfilRepository.GetPerfilID(descricao);
        }
    }
}