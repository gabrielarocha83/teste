using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Servicos.LiberacaoAutomatica.YaraSAP;

namespace Yara.Servicos.LiberacaoAutomatica.Repositorio
{
    public interface ILiberacaoCarteiras
    {
        Task<IEnumerable<SoapAtualizaBloqueioORDEM_VENDA>> ObterDivisoesLiberadasSAP();
    }
}