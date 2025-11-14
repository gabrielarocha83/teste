using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceClassificacaoGrupoEconomico
    {
        Task<IEnumerable<ClassificacaoGrupoEconomicoDto>> GetAll();
        Task<ClassificacaoGrupoEconomicoDto> GetbyID(int ID);
    }
}
