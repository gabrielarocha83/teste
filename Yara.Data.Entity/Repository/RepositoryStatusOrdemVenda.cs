using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryStatusOrdemVenda : RepositoryBase<StatusOrdemVendas>, IRepositoryStatusOrdemVenda
    {
        public RepositoryStatusOrdemVenda(DbContext context) : base(context)
        {
        }
    }
}
