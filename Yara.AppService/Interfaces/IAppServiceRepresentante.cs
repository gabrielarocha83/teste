using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceRepresentante
    {
        Task<IEnumerable<RepresentanteDto>> GetAllRepresentation();
        Task<IEnumerable<RepresentanteDto>> GetAllRepresentationByAccountClient(Guid contaID, string EmpresaID);
    }
}
