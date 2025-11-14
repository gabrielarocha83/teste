using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain;
using Yara.Domain.Repository;
using Yara.DomainService.Interfaces;

namespace Yara.DomainService
{
    internal class DomainServiceGrupoPermissao : IDomainServiceGrupoPermissao
    {

        private readonly IRepositoryGrupoPermissao _repository;

        public DomainServiceGrupoPermissao(IRepositoryGrupoPermissao repository)
        {
            _repository = repository;
        }
        public async Task<GrupoPermissao> GetIdAsync(Guid ID)
        {
            return await _repository.GetIdAsync(ID);
        }

        public async Task<IEnumerable<GrupoPermissao>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public bool Insert(GrupoPermissao obj)
        {
            return _repository.Insert(obj);
        }

        public bool Update(GrupoPermissao obj)
        {
            return _repository.Update(obj);
        }

        public async Task<bool> Inactive(Guid ID)
        {
            var inactiveGroup =  await _repository.GetIdAsync(ID);
            inactiveGroup.Ativo = false;
            return this.Update(inactiveGroup);
        }
    }
}