using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain;
using Yara.Domain.Repository;
using Yara.DomainService.Interfaces;

namespace Yara.DomainService
{
    internal class DomainServicePermissao : IDomainServicePermissao
    {

        private readonly IRepositoryPermissao _repository;

        public DomainServicePermissao(IRepositoryPermissao repository)
        {
            _repository = repository;
        }
        public async Task<Permissao> GetIdAsync(Guid ID)
        {
            return await _repository.GetIdAsync(ID);
        }

        public async Task<IEnumerable<Permissao>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public bool Insert(Permissao obj)
        {
            return _repository.Insert(obj);
        }

        public bool Update(Permissao obj)
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