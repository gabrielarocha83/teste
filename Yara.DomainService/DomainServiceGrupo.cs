using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain;
using Yara.Domain.Repository;
using Yara.DomainService.Interfaces;

namespace Yara.DomainService
{
    internal class DomainServiceGrupo : IDomainServiceGrupo
    {

        private readonly IRepositoryGrupo _repository;

        public DomainServiceGrupo(IRepositoryGrupo repository)
        {
            _repository = repository;
        }
        public async Task<Grupo> GetIdAsync(Guid ID)
        {
            return await _repository.GetIdAsync(ID);
        }

        public async Task<IEnumerable<Grupo>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public bool Insert(Grupo obj)
        {
            return _repository.Insert(obj);
        }

        public bool Update(Grupo obj)
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