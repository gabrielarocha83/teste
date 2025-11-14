using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaLCComite : IRepositoryBase<PropostaLCComite>
    {
        Task<IEnumerable<PropostaLCComite>> InsertPropostaLCComite(Guid id, Guid SegmentoID, string CodigoSAP, Guid usuarioID, string EmpresaID);
        Task<PropostaLCComite> InsertFluxo(PropostaLCComite comite);
        Task<bool> AbortaComite(Guid propostaId);
    }
}
