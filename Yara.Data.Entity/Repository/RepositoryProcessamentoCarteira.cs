using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryProcessamentoCarteira : RepositoryBase<ProcessamentoCarteira>, IRepositoryProcessamentoCarteira
    {
        public RepositoryProcessamentoCarteira(DbContext context) : base(context)
        {
        }
    }
}

