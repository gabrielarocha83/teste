using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceEstruturaComercialPapel
    {
        Task<IEnumerable<EstruturaComercialPapelDto>> GetAllFilterAsync(Expression<Func<EstruturaComercialPapelDto, bool>> expression);
        Task<EstruturaComercialPapelDto> GetByPaper(string sigla);

    }
}
