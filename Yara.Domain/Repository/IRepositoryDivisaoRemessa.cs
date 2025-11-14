using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;

namespace Yara.Domain.Repository
{
    public interface IRepositoryDivisaoRemessa : IRepositoryBase<DivisaoRemessa>
    {
        Task<IEnumerable<DivisaoRemessaCockPit>> GetAllPendencyByUser(Guid UserID, string EmpresaID);
        Task<IEnumerable<DivisaoRemessaCockPit>> GetAllPendencyByAccountClient(Guid ContaClienteID, string EmpresaID);
        Task<DivisaoRemessa> GetDivisaoAsync(int Divisao, int Item, string Numero);
        Task<IEnumerable<DivisaoRemessaLogFluxo>> GetAllLogByDivisao(int Divisao, int Item, string Numero, Guid SolicitanteID);
        void UpdateDivisao(Guid SolicitanteID);
    }
}
