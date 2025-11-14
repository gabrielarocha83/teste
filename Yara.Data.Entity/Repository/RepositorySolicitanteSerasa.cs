using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositorySolicitanteSerasa : RepositoryBase<SolicitanteSerasa>, IRepositorySolicitanteSerasa
    {
        public RepositorySolicitanteSerasa(DbContext context) : base(context)
        {

        }
    }
}
