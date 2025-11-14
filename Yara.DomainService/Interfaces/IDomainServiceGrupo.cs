using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain;

namespace Yara.DomainService.Interfaces
{
    public interface IDomainServiceGrupo 
    {
        Task<Grupo> GetIdAsync(Guid ID);
        Task<IEnumerable<Grupo>> GetAllAsync();
        bool Insert(Grupo obj);
        bool Update(Grupo obj);
        Task<bool> Inactive(Guid ID);

    }
}