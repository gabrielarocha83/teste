using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;

namespace Yara.Domain.Repository
{
    public interface IRepositoryTituloComentario : IRepositoryBase<TituloComentario>
    {
       Task<IEnumerable<BuscaTituloComentario>> GetAllComments(string numeroDocumento, string linha, string anoExercicio, string empresa);
    }
}
