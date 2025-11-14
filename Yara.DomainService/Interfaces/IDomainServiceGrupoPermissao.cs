using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain;

namespace Yara.DomainService.Interfaces
{
    public interface IDomainServiceGrupoPermissao
    {
        Task<GrupoPermissao> GetIdAsync(Guid ID);
        Task<IEnumerable<GrupoPermissao>> GetAllAsync();
        bool Insert(GrupoPermissao obj);
        bool Update(GrupoPermissao obj);
        Task<bool> Inactive(Guid ID);

    }
}