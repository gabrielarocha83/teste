
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaAbono : IRepositoryBase<PropostaAbono>
    {
        int GetMaxNumeroInterno();

        Task<PropostaAbonoComite> InsertPropostaAbonoComite(Guid id, Guid segmentoId, string codigoSap,
            Guid usuarioId, string EmpresaID);

        Task<PropostaAbonoComite> AprovaReprovaAbono(Guid id, Guid Usuario, bool Conceito, bool Aprovado, string Descricao, string EmpresaID);
        Task<IEnumerable<BuscaPropostaAbono>> BuscaPendenciasAbono(Guid usuarioID, string EmpresaID, bool Acompanhar);
        Task<Decimal> Total(Guid PropostaID);
    }
}
