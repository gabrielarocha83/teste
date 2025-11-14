using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryFerias : IRepositoryBase<Ferias>
    {
        Task<IEnumerable<Ferias>> GetFeriasByIDUser(Guid user);
        Task<IEnumerable<Ferias>> GetStatus(Guid usuarioId, DateTime dataInicio, DateTime dataFim);
    }
}
