using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryPropostaLCDemonstrativo : RepositoryBase<PropostaLCDemonstrativo>, IRepositoryPropostaLCDemonstrativo
    {
        public RepositoryPropostaLCDemonstrativo(DbContext context) : base(context)
        {
        }
        
    }
}
