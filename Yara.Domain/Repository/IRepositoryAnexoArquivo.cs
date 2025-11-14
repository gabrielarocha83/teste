using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryAnexoArquivo : IRepositoryBase<AnexoArquivo>
    {
        Task<IEnumerable<AnexoArquivo>> CustomGetAllFilterAsync(Expression<Func<AnexoArquivo, bool>> expression);
    }
}
