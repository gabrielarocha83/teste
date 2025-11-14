using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yara.AppService.Interfaces;
using Yara.AppService.RFCSap;
using Yara.Domain.Repository;

namespace Yara.AppService
{
    public class AppServiceRFCSap : IAppServiceRFCSap
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceRFCSap(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AbonarTitulos(Guid PropostaID)
        {
            ISapComm cn = new SapCommClient();

            var proposta = await _unitOfWork.PropostaAbonoRepository.GetAsync(c => c.ID.Equals(PropostaID));

            var parametroconta = await _unitOfWork.ParametroSistemaRepository.GetAsync(c => c.EmpresasID.Equals(proposta.EmpresaID) && c.Chave.Equals("contasap"));

            var titulos = await _unitOfWork.PropostaAbonoTituloRepository.GetAllFilterAsync(c => c.PropostaAbonoID.Equals(PropostaID));

            var titulossap = from t in titulos
                             select new SoapAbonarTitulo()
                             {
                                 Empresa = t.Empresa,
                                 Linha = t.Linha,
                                 NumeroDocumento = t.NumeroDocumento,
                                 AnoExercicio = t.AnoExercicio,
                                 Conta = parametroconta.Valor
                             };

            foreach (var titulo in titulossap)
            {
                await cn.AbonarTituloAsync(titulo);
            }
        }

        public async Task EnvioTitulosJuridico(Guid PropostaID)
        {
            ISapComm cn = new SapCommClient();

            var titulos = await _unitOfWork.PropostaJuridicoTituloRepository.GetAllFilterAsync(c => c.PropostaJuridicoID.Equals(PropostaID));

            try
            {
                var titulossap = from t in titulos
                                 select new SoapBloquearTitulo()
                                 {
                                     Empresa = t.Empresa,
                                     Linha = t.Linha,
                                     NumeroDocumento = t.NumeroDocumento,
                                     AnoExercicio = t.AnoExercicio
                                 };

                foreach (var titulo in titulossap)
                {
                    await cn.BloquearTituloAsync(titulo);
                }
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Problema ao enviar titulos no SAP.");
            }
        }

        public async Task ProrrogarTitulos(Guid PropostaID)
        {
            ISapComm cn = new SapCommClient();

            var titulos = await _unitOfWork.PropostaProrrogacaoTitulo.GetAllFilterAsync(c => c.PropostaProrrogacaoID.Equals(PropostaID));

            try
            {
                var titulossap = from t in titulos
                                 select new SoapProrrogarTitulo()
                                 {
                                     Empresa = t.Empresa,
                                     Linha = t.Linha,
                                     NumeroDocumento = t.NumeroDocumento,
                                     AnoExercicio = t.AnoExercicio,
                                     DataProrrogada = t.NovoVencimento
                                 };

                foreach (var titulo in titulossap)
                {
                    await cn.ProrrogarTituloAsync(titulo);
                }
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Problema ao enviar titulos no SAP.");
            }
        }

        public async Task EnviarFixacaoLimite(Guid PropostaID)
        {
            var proposta = await _unitOfWork.PropostaLCRepository.GetAsync(c => c.ID.Equals(PropostaID));

            if (proposta.GrupoEconomicoID.HasValue)
            {
                var grupo = await _unitOfWork.GrupoEconomicoReporitory.GetAsync(c => c.ID.Equals(proposta.GrupoEconomicoID.Value));

                var membros = await _unitOfWork.GrupoEconomicoMembroReporitory.GetAllFilterAsync(c => c.GrupoEconomicoID.Equals(proposta.GrupoEconomicoID.Value));

                if (grupo.ClassificacaoGrupoEconomicoID == 1)
                {
                    var soap = new SoapFixarLimite();

                    var contacodigos = new List<string>();

                    var codigoprincial = Guid.Empty;

                    foreach (var membro in membros)
                    {
                        if (membro.MembroPrincipal)
                        {
                            codigoprincial = membro.ContaClienteID;
                        }

                        var codigos = await _unitOfWork.ContaClienteCodigoRepository.GetAllFilterAsync(c => c.ContaClienteID.Equals(membro.ContaClienteID));

                        contacodigos.AddRange(codigos.Select(c => c.Codigo));
                    }

                    var conta = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(codigoprincial));

                    var dadofinanceiro = await _unitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.ContaClienteID.Equals(conta.ID) && c.EmpresasID.Equals(proposta.EmpresaID));

                    soap.Codigos = contacodigos.ToArray();
                    soap.CodigoPrincipal = conta.CodigoPrincipal;
                    soap.ValorLimite = dadofinanceiro.LC ?? 0;
                    soap.Vigencia = dadofinanceiro.VigenciaFim.Value;
                    soap.YaraGalvani = dadofinanceiro.EmpresasID;
                    soap.Bloquear = false;

                    await ServicoFixar(soap);
                }
                else
                {
                    foreach (var membro in membros)
                    {
                        await ChamaServicoFixar(membro.ContaClienteID, proposta.EmpresaID);
                    }
                }
            }
            else
            {
                await this.ChamaServicoFixar(proposta.ContaClienteID, proposta.EmpresaID);
            }
        }

        public async Task EnviarFixacaoLimiteAlcada(Guid PropostaID)
        {
            var proposta = await _unitOfWork.PropostaAlcadaComercial.GetAsync(c => c.ID.Equals(PropostaID));
            await ChamaServicoFixar(proposta.ContaClienteID, proposta.EmpresaID);
        }

        private async Task ChamaServicoFixar(Guid ContaCLienteID, string EmpresaID)
        {
            ISapComm cn = new SapCommClient();

            var conta = await _unitOfWork.ContaClienteRepository.GetAsync(c => c.ID.Equals(ContaCLienteID));

            var dadosfinanceiros = await _unitOfWork.ContaClienteFinanceiroRepository.GetAsync(c => c.ContaClienteID.Equals(conta.ID) && c.EmpresasID.Equals(EmpresaID));

            if (dadosfinanceiros != null && dadosfinanceiros.LC.HasValue && dadosfinanceiros.VigenciaFim.HasValue)
            {
                var codigos = await _unitOfWork.ContaClienteCodigoRepository.GetAllFilterAsync(c => c.ContaClienteID.Equals(ContaCLienteID));

                var soap = new SoapFixarLimite
                {
                    CodigoPrincipal = conta.CodigoPrincipal,
                    Codigos = codigos.Select(c => c.Codigo).ToArray(),
                    ValorLimite = dadosfinanceiros.LC ?? 0,
                    Vigencia = dadosfinanceiros.VigenciaFim.Value,
                    YaraGalvani = EmpresaID
                };

                await ServicoFixar(soap);
            }
        }

        private async Task ServicoFixar(SoapFixarLimite soap)
        {
            ISapComm cn = new SapCommClient();
            await cn.FixarLimiteAsync(soap);
        }
    }
}