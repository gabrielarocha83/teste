using System;
using System.Threading.Tasks;
using Yara.Domain.Entities;
// ReSharper disable InconsistentNaming

namespace Yara.Domain.Repository
{
    public interface IRepositoryLiberacaoGrupoEconomicoFluxo : IRepositoryBase<LiberacaoGrupoEconomicoFluxo>
    {
        Task<FluxoGrupoEconomico> AprovaReprovaLiberacaoGrupoEconomico(bool aprovar, Guid GrupoEconomicoID, Guid UsuarioID, int ClassificacaoID, string EmpresaID);
    }
}