using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceEstado
    {
        Task<IEnumerable<EstadoDto>> GetAllStates();
        Task<EstadoDto> GetById(Guid ID);
        Task<IEnumerable<EstadoDto>> GetAllStatesByRegion(Guid RegionID);
    }
}
