using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceEstruturaComercial
    {
        

        Task<IEnumerable<EstruturaComercialDto>> GetAllFilterAsync(Expression<Func<EstruturaComercialDto, bool>> expression);

     
        //Task<IEnumerable<T>> GetAllOrderAndPaginationAsync<TKey>(Expression<Func<T, bool>> expression,
        //    Expression<Func<T, TKey>> order, int Page, int Skip, bool OrderAscending);
        Task<bool> Insert(EstruturaComercialDiretoriaDto obj);
        Task<bool> Update(EstruturaComercialDiretoriaDto obj);
        Task<IEnumerable<EstruturaComercialDto>> GetEstruturaComercialByPaper(string sigla);
        Task<EstruturaComercialDto> GetEstruturaComercialByID(string sigla);
    }
}
