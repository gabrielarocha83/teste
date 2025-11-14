using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaLCAdicionalComite : IRepositoryBase<PropostaLCAdicionalComite>
    {
        Task<IEnumerable<PropostaLCAdicionalComite>> InsertPropostaLCAdicionalComite(Guid id, Guid SegmentoID, string CodigoSAP, Guid usuarioID, string EmpresaID);
        Task<PropostaLCAdicionalComite> InsertFluxo(PropostaLCAdicionalComite comite);
        Task<bool> AbortaComite(Guid propostaId);
    }
}
