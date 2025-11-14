using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Domain.Repository
{
    public interface IRepositoryNotificacoes
    {
        Task<IEnumerable<NotificacaoUsuario>> BuscaUsuariosCockpit(string EmpresaID);
    }
}
