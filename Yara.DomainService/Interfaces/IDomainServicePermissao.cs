using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain;

namespace Yara.DomainService.Interfaces
{
    public interface IDomainServicePermissao
    {
        Task<Permissao> GetIdAsync(Guid ID);
        Task<IEnumerable<Permissao>> GetAllAsync();
        bool Insert(Permissao obj);
        bool Update(Permissao obj);
        Task<bool> Inactive(Guid ID);

    }
}