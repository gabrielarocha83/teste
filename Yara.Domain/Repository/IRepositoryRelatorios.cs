using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities.Procedures;

namespace Yara.Domain.Repository
{
    public interface IRepositoryRelatorios
    {
        Task<IEnumerable<BuscaPropostas>> GetConsultaProposta(BuscaPropostasSearch filter);
        Task<IEnumerable<string>> GetStatus();
    }
}
