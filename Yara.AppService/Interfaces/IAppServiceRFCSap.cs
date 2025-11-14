using System;
using System.Threading.Tasks;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceRFCSap
    {
        Task AbonarTitulos(Guid PropostaID);
        Task EnvioTitulosJuridico(Guid PropostaID);
        Task ProrrogarTitulos(Guid PropostaID);
        Task EnviarFixacaoLimite(Guid PropostaID);
        Task EnviarFixacaoLimiteAlcada(Guid PropostaID);
    }
}