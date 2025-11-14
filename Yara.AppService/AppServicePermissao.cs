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
    public class AppServicePermissao : IAppServicePermissao
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServicePermissao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PermissaoDto> GetAsync(Expression<Func<PermissaoDto, bool>> expression)
        {
            var grupos = await _unitOfWork.PermissaoRepository.GetAsync(Mapper.Map<Expression<Func<Permissao,bool>>>(expression));

            return Mapper.Map<PermissaoDto>(grupos);
        }

        public async Task<IEnumerable<PermissaoDto>> GetAllFilterAsync(Expression<Func<PermissaoDto, bool>> expression)
        {
            var grupos = await _unitOfWork.PermissaoRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<Permissao, bool>>>(expression));

            return Mapper.Map<IEnumerable<PermissaoDto>>(grupos);
        }

        public async Task<IEnumerable<PermissaoDto>> GetAllAsync()
        {
            //var grupos = await _domainService.GetAllAsync();
            var grupos = await _unitOfWork.PermissaoRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<PermissaoDto>>(grupos);
        }

        public Task<IEnumerable<PermissaoDto>> GetAllFilterAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(PermissaoDto obj)
        {
            var permissao = obj.MapTo<Permissao>();
            //return _domainService.Insert(grupo);
            _unitOfWork.PermissaoRepository.Insert(permissao);
            return  _unitOfWork.Commit();
        }

        public async Task<bool> Update(PermissaoDto obj)
        {
            var permissao = await _unitOfWork.PermissaoRepository.GetAsync(c => c.Nome.Equals(obj.Nome));
            permissao.Nome = obj.Nome;
            permissao.Ativo = obj.Ativo;
            //return _domainService.Update(grupo);
            _unitOfWork.PermissaoRepository.Update(permissao);
            return  _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(string nome)
        {
            var permissao = await _unitOfWork.PermissaoRepository.GetAsync(c=>c.Nome.Equals(nome));
            permissao.Ativo = false;
            _unitOfWork.PermissaoRepository.Update(permissao);
            return  _unitOfWork.Commit();
        }

        public async Task<List<PermissaoDto>> GetListPermissao(Guid idUsuarioGuid)
        {
            var permissoes = await _unitOfWork.PermissaoRepository.ListaPermissoes(idUsuarioGuid);
            return Mapper.Map<List<PermissaoDto>>(permissoes);
        }
    }
}