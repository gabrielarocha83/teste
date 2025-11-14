using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryAnexoArquivoCobranca : RepositoryBase<AnexoArquivoCobranca>, IRepositoryAnexoArquivoCobranca
    {
        public RepositoryAnexoArquivoCobranca(DbContext context) : base(context)
        {
        }
    }
}
