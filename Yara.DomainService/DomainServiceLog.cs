using System;
using Yara.Domain;
using Yara.Domain.Repository;
using Yara.DomainService.Interfaces;

namespace Yara.DomainService
{
    internal class DomainServiceLog : IDomainServiceLog
    {
        private readonly IRepositoryLog _repository;

        public DomainServiceLog(IRepositoryLog repository)
        {
            _repository = repository;
        }

        public bool Create(Log obj)
        {
            obj.ID = Guid.NewGuid();
            obj.DataCriacao = DateTime.Now;
            
            return _repository.Insert(obj);
        }
    }
}