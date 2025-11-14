using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaProrrogacao : IRepositoryBase<PropostaProrrogacao>
    {
        int GetMaxNumeroInterno();
        Task<IEnumerable<BuscaDetalhesPropostaProrrogacaoTitulo>> BuscaDetalhesTitulos(Guid propostaProrrogacaoId, string empresaId);
        Task<PropostaProrrogacaoComite> AprovaReprovaProrrogacao(Guid id, Guid Usuario, bool NovasLiberacoes, bool Aprovado, float taxa, string Descricao, string EmpresaID);
        Task<PropostaProrrogacaoComite> InsertPropostaProrrogacaoComite(Guid id, Guid segmentoId, string codigoSap, Guid usuarioId, string EmpresaID);
        Task<IEnumerable<BuscaPropostaProrrogacao>> BuscaPendenciasProrrogacao(Guid usuarioID, string EmpresaID, bool Acompanhar);
    }
}
