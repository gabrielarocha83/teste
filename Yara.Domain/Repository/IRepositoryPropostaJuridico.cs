using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;

namespace Yara.Domain.Repository
{
    public interface IRepositoryPropostaJuridico : IRepositoryBase<PropostaJuridico>
    {
        int GetMaxNumeroInterno();
        Task<PropostaJuridico> CriaPropostaJuridica(Guid contaClienteId, Guid usuarioId, string empresaId);
        Task<IEnumerable<ControleCobrancaEnvioJuridico>> BuscaControleCobranca(string empresaId, string diretoriaCodigo);
    }
}
