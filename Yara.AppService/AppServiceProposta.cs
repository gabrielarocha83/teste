using System;
using System.Linq;
using System.Threading.Tasks;
using Yara.AppService.Interfaces;
using Yara.Domain.Repository;

namespace Yara.AppService
{
    public class AppServiceProposta : IAppServiceProposta
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceProposta(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> ExistePropostaEmAndamentoAsync(Guid contaClienteID, string empresaID)
        {
            var retorno = "";

            try
            {
                var statusPropostaLC_A = new[] { "AA", "XE", "XR" };

                //Proposta de Limite de Crédito
                var ultimaPropostaLC = await _unitOfWork.PropostaLCRepository.GetLatest(c => c.ContaClienteID.Equals(contaClienteID) && c.EmpresaID.Equals(empresaID));
                if (ultimaPropostaLC != null && !statusPropostaLC_A.Contains(ultimaPropostaLC.PropostaLCStatusID))
                    retorno = $"Proposta {(ultimaPropostaLC.Ecomm ? "EC" : "LC")}{ultimaPropostaLC.NumeroInternoProposta:00000}/{ultimaPropostaLC.DataCriacao:yyyy} em andamento.";
                else
                {
                    //Proposta de Limite de Crédito Adicional
                    var ultimaPropostaLA = await _unitOfWork.PropostaLCAdicionalRepository.GetLatest(c => c.ContaClienteID.Equals(contaClienteID) && c.EmpresaID.Equals(empresaID));
                    if (ultimaPropostaLA != null && !statusPropostaLC_A.Contains(ultimaPropostaLA.PropostaLCStatusID))
                        retorno = $"Proposta LA{ultimaPropostaLA.NumeroInternoProposta:00000}/{ultimaPropostaLA.DataCriacao:yyyy} em andamento.";
                    else
                    {
                        //Proposta de Alçada Comercial 
                        var statusPropostaAC = new[] { "AA", "AP", "EN", "RE" };
                        var ultimaPropostaAC = await _unitOfWork.PropostaAlcadaComercial.GetLatest(c => c.ContaClienteID.Equals(contaClienteID) && c.EmpresaID.Equals(empresaID));
                        if (ultimaPropostaAC != null && !statusPropostaAC.Contains(ultimaPropostaAC.PropostaCobrancaStatus.ID))
                            retorno = $"Proposta AC{ultimaPropostaAC.NumeroInternoProposta:00000}/{ultimaPropostaAC.DataCriacao:yyyy} em andamento.";
                    }
                }
            }
            catch (Exception e)
            {
                retorno = e.Message;
            }

            return retorno;
        }
    }
}
