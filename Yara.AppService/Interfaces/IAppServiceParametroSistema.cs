using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceParametroSistema 
    {
        Task<ParametroSistemaDto> GetAsync(Expression<Func<ParametroSistemaDto, bool>> expression);
        Task<IEnumerable<ParametroSistemaDto>> GetAllFilterAsync(Expression<Func<ParametroSistemaDto, bool>> expression);
        Task<IEnumerable<ParametroSistemaDto>> GetAllAsync(string empresaID);
        Task<bool> InsertAsync(ParametroSistemaDto obj);
        Task<bool> Inactive(Guid id);
        bool Insert(ParametroSistemaDto obj);
        Task<bool> Update(ParametroSistemaDto obj);
        //Task<bool> Update(List<ParametroSistemaDto> obj);
    }
}
