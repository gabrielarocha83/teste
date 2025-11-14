using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryContaClienteTelefone : RepositoryBase<ContaClienteTelefone>, IRepositoryContaClienteTelefone
    {
        public RepositoryContaClienteTelefone(DbContext context) : base(context)
        {
        }

       
    }
}