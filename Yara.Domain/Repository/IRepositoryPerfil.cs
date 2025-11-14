using System;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPerfil : IRepositoryBase<Perfil>
    {
        Task<Guid> GetPerfilID(string descricao);
    }
}