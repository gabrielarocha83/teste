using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceTituloComentario 
    {
        Task<IEnumerable<BuscaTituloComentarioDto>>GetAllComments(string numeroDocumento, string linha, string anoExercicio, string empresa);
    }
}
