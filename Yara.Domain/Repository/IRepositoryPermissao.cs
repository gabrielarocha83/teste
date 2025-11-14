using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPermissao : IRepositoryBase<Permissao>
    {
        Task<List<Permissao>> ListaPermissoes(Guid idUsuarioGuid);
    }
}