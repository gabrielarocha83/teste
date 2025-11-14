using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceLiberacaoGrupoEconomicoFluxo 
    {
        Task<FluxoGrupoEconomicoDto> AprovaReprovaLiberacaoGrupoEconomico(bool aprovar, Guid GrupoEconomicoID, Guid UsuarioID, int ClassificacaoID, string EmpresaID, string URL);
        Task<KeyValuePair<bool, string>> AprovaReprovaLiberacaoGrupoEconomicoValue(bool aprovar, Guid grupoEconomicoId, Guid usuarioId, int classificacaoId, string empresaId, string URL);
    }
}
