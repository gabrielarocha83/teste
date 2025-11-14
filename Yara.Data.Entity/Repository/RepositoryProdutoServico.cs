using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryProdutoServico : RepositoryBase<ProdutoServico>, IRepositoryProdutoServico
    {
        public RepositoryProdutoServico(DbContext context) : base(context)
        {
        }
    }
}
