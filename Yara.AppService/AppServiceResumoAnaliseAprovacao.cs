using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Repository;
using System.Linq;
using Yara.Domain.Entities.Procedures;

namespace Yara.AppService
{
    public class AppServiceResumoAnaliseAprovacao : IAppServiceResumoAnaliseAprovacao
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceResumoAnaliseAprovacao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ResumoAnaliseAprovacaoDto>> BuscaResumoAnalise(Guid usuarioID, string empresaID, List<string> gcs)
        {
            var resumoAnaliseAprovacaoDtos = new List<ResumoAnaliseAprovacaoDto>();
            var permissoes = await _unitOfWork.PermissaoRepository.ListaPermissoes(usuarioID);
            
            var propostaLCStatusIDs = new List<string> { "FA", "FP", "FE", "FF", "EA" };

            var propostasLC = await BuscaPropostaLCPorStatus(empresaID, propostaLCStatusIDs, gcs);

            //a) Leadtime médio geral de pré-análise
            var resumoAnalises_LeadtimePreAnalise = permissoes.FirstOrDefault(c => c.Nome.Equals("ResumoAnalises_LeadtimePreAnalise"));
            if (resumoAnalises_LeadtimePreAnalise != null)
            {
                var leadtimePreAnalise = ObterResumoAnaliseAprovacao(propostasLC, new List<string> { "FA", "FP" }, ObterDescricao(resumoAnalises_LeadtimePreAnalise.Descricao), false, false);
                leadtimePreAnalise.Totalizador = leadtimePreAnalise.Totalizador == 0 ? 0 : Math.Round(leadtimePreAnalise.PropostasLC.Sum(c => c.LeadTime) / leadtimePreAnalise.Totalizador, 2, MidpointRounding.AwayFromZero);

                resumoAnaliseAprovacaoDtos.Add(leadtimePreAnalise);
            }

            //b) Leadtime médio geral de análise
            var resumoAnalises_LeadtimeAnalise = permissoes.FirstOrDefault(c => c.Nome.Equals("ResumoAnalises_LeadtimeAnalise"));
            if (resumoAnalises_LeadtimeAnalise != null)
            {
                var leadtimeAnalise = ObterResumoAnaliseAprovacao(propostasLC, new List<string> { "FE", "FF", "EA" }, ObterDescricao(resumoAnalises_LeadtimeAnalise.Descricao), false, false);
                leadtimeAnalise.Totalizador = leadtimeAnalise.Totalizador == 0 ? 0 : Math.Round(leadtimeAnalise.PropostasLC.Sum(c => c.LeadTime) / leadtimeAnalise.Totalizador, 2, MidpointRounding.AwayFromZero);

                resumoAnaliseAprovacaoDtos.Add(leadtimeAnalise);
            }

            //c) Quantidade de propostas em pré-análise
            var resumoAnalises_PropostasEmPreAnalise = permissoes.FirstOrDefault(c => c.Nome.Equals("ResumoAnalises_PropostasEmPreAnalise"));
            if (resumoAnalises_PropostasEmPreAnalise != null)
            {
                var propostasEmPreAnalise = ObterResumoAnaliseAprovacao(propostasLC, new List<string> { "FP" }, ObterDescricao(resumoAnalises_PropostasEmPreAnalise.Descricao), false, false);
                resumoAnaliseAprovacaoDtos.Add(propostasEmPreAnalise);
            }

            //d) Quantidade de propostas enviadas para pré-análise
            var resumoAnalises_PropostasEnviadasPreAnalise = permissoes.FirstOrDefault(c => c.Nome.Equals("ResumoAnalises_PropostasEnviadasPreAnalise"));
            if (resumoAnalises_PropostasEnviadasPreAnalise != null)
            {
                var propostasEnviadasPreAnalise = ObterResumoAnaliseAprovacao(propostasLC, new List<string> { "FA" }, ObterDescricao(resumoAnalises_PropostasEnviadasPreAnalise.Descricao), false, false);
                resumoAnaliseAprovacaoDtos.Add(propostasEnviadasPreAnalise);
            }

            //e) Quantidade de propostas em análise;
            var resumoAnalises_PropostasEmAnalise = permissoes.FirstOrDefault(c => c.Nome.Equals("ResumoAnalises_PropostasEmAnalise"));
            if (resumoAnalises_PropostasEmAnalise != null)
            {
                var propostasEmAnalise = ObterResumoAnaliseAprovacao(propostasLC, new List<string> { "FF", "EA" }, ObterDescricao(resumoAnalises_PropostasEmAnalise.Descricao), false, true);
                resumoAnaliseAprovacaoDtos.Add(propostasEmAnalise);
            }

            //f) Quantidade de propostas enviadas para análise
            var resumoAnalises_PropostasEnviadasAnalise = permissoes.FirstOrDefault(c => c.Nome.Equals("ResumoAnalises_PropostasEnviadasAnalise"));
            if (resumoAnalises_PropostasEnviadasAnalise != null)
            {
                var propostasEnviadasAnalise = ObterResumoAnaliseAprovacao(propostasLC, new List<string> { "FE" }, ObterDescricao(resumoAnalises_PropostasEnviadasAnalise.Descricao), false, true);
                resumoAnaliseAprovacaoDtos.Add(propostasEnviadasAnalise);
            }

            //g) Quantidade de propostas por alçada de análise
            var resumoAnalises_PropostasAlcadaAnalise = permissoes.FirstOrDefault(c => c.Nome.Equals("ResumoAnalises_PropostasAlcadaAnalise"));
            if (resumoAnalises_PropostasAlcadaAnalise != null)
            {
                var fluxoAlcadaAnalise = await _unitOfWork.FluxoAlcadaAnaliseRepository.GetAllFilterAsync(c => c.EmpresaID == empresaID && c.Ativo);
                if (fluxoAlcadaAnalise.Count() > 0)
                {
                    var fluxoAlcadaAnaliseDescricoes = fluxoAlcadaAnalise
                        .OrderBy(c => c.Nivel)
                        .Select(c => c.Perfil?.Descricao)
                        .Distinct();

                    var descricaoPadraoResumo = ObterDescricao(resumoAnalises_PropostasAlcadaAnalise.Descricao);
                    var propostasAlcadaAnalise = ObterResumoAnaliseAprovacao(propostasLC, new List<string> { "FA", "FP", "FE", "FF", "EA" }, descricaoPadraoResumo, true, false);

                    foreach (var fluxoAlcadaAnaliseDescricao in fluxoAlcadaAnaliseDescricoes)
                    {
                        var propostasDaAlcada = propostasAlcadaAnalise.PropostasLC.Where(c => c.AlcadaAnalise == fluxoAlcadaAnaliseDescricao);

                        resumoAnaliseAprovacaoDtos.Add(new ResumoAnaliseAprovacaoDto()
                        {
                            Descricao = $"{descricaoPadraoResumo}: {fluxoAlcadaAnaliseDescricao}",
                            PropostasLC = propostasDaAlcada,
                            Totalizador = propostasDaAlcada.Count()
                        });
                    }
                }
            }

            // TODO: Ordenar.

            return resumoAnaliseAprovacaoDtos;
        }

        public async Task<IEnumerable<ResumoAnaliseAprovacaoDto>> BuscaResumoAprovacao(Guid usuarioID, string empresaID, List<string> gcs)
        {
            var resumoAnaliseAprovacaoDtos = new List<ResumoAnaliseAprovacaoDto>();
            var permissoes = await _unitOfWork.PermissaoRepository.ListaPermissoes(usuarioID);

            var propostaLCStatusIDs = new List<string> { "FC", "WA", "AA", "AP", "AG" };

            var propostasLC = await BuscaPropostaLCPorStatus(empresaID, propostaLCStatusIDs, gcs);

            //a) Leadtime médio geral em comitê
            var resumoAprovacoes_LeadtimeComite = permissoes.FirstOrDefault(c => c.Nome.Equals("ResumoAprovacoes_LeadtimeComite"));
            if (resumoAprovacoes_LeadtimeComite != null)
            {
                var leadtimeComite = ObterResumoAnaliseAprovacao(propostasLC, new List<string> { "FC" }, ObterDescricao(resumoAprovacoes_LeadtimeComite.Descricao), true, true);
                leadtimeComite.Totalizador = leadtimeComite.Totalizador == 0 ? 0 : Math.Round(leadtimeComite.PropostasLC.Sum(c => c.LeadTime) / leadtimeComite.Totalizador, 2, MidpointRounding.AwayFromZero);

                resumoAnaliseAprovacaoDtos.Add(leadtimeComite);
            }

            //b) Quantidade de propostas aguardando ação do analista
            var resumoAprovacoes_PropostasAguardandoAcaoAnalista = permissoes.FirstOrDefault(c => c.Nome.Equals("ResumoAprovacoes_PropostasAguardandoAcaoAnalista"));
            if (resumoAprovacoes_PropostasAguardandoAcaoAnalista != null)
            {
                var propostasAguardandoAcaoAnalista = ObterResumoAnaliseAprovacao(propostasLC, new List<string> { "WA" }, ObterDescricao(resumoAprovacoes_PropostasAguardandoAcaoAnalista.Descricao), false, false);
                resumoAnaliseAprovacaoDtos.Add(propostasAguardandoAcaoAnalista);
            }

            //c) Quantidade de propostas aprovadas
            var resumoAprovacoes_PropostasAprovadas = permissoes.FirstOrDefault(c => c.Nome.Equals("ResumoAprovacoes_PropostasAprovadas"));
            if (resumoAprovacoes_PropostasAprovadas != null)
            {
                var propostasAprovadas = ObterResumoAnaliseAprovacao(propostasLC, new List<string> { "AA" }, ObterDescricao(resumoAprovacoes_PropostasAprovadas.Descricao), true, true);
                resumoAnaliseAprovacaoDtos.Add(propostasAprovadas);
            }

            //d) Quantidade de propostas aprovadas com pendência
            var resumoAprovacoes_PropostasAprovadasPendencia = permissoes.FirstOrDefault(c => c.Nome.Equals("ResumoAprovacoes_PropostasAprovadasPendencia"));
            if (resumoAprovacoes_PropostasAprovadasPendencia != null)
            {
                var propostasAprovadasPendencia = ObterResumoAnaliseAprovacao(propostasLC, new List<string> { "AP" }, ObterDescricao(resumoAprovacoes_PropostasAprovadasPendencia.Descricao), true, true);
                resumoAnaliseAprovacaoDtos.Add(propostasAprovadasPendencia);
            }

            //e) Quantidade de propostas aprovadas com pendência de garantia
            var resumoAprovacoes_PropostasAprovadasPendenciaGarant = permissoes.FirstOrDefault(c => c.Nome.Equals("ResumoAprovacoes_PropostasAprovadasPendenciaGarant"));
            if (resumoAprovacoes_PropostasAprovadasPendenciaGarant != null)
            {
                var propostasAprovadasPendenciaGarant = ObterResumoAnaliseAprovacao(propostasLC, new List<string> { "AG" }, ObterDescricao(resumoAprovacoes_PropostasAprovadasPendenciaGarant.Descricao), true, true);
                resumoAnaliseAprovacaoDtos.Add(propostasAprovadasPendenciaGarant);
            }

            //f) Quantidade de propostas em aprovação
            var resumoAprovacoes_PropostasEmAprovacao = permissoes.FirstOrDefault(c => c.Nome.Equals("ResumoAprovacoes_PropostasEmAprovacao"));
            if (resumoAprovacoes_PropostasEmAprovacao != null)
            {
                var propostasEmAprovacao = ObterResumoAnaliseAprovacao(propostasLC, new List<string> { "FC" }, ObterDescricao(resumoAprovacoes_PropostasEmAprovacao.Descricao), true, true);
                resumoAnaliseAprovacaoDtos.Add(propostasEmAprovacao);
            }

            //g) Quantidade de propostas por alçada de aprovação
            var resumoAprovacoes_PropostasAlcadaAprovacao = permissoes.FirstOrDefault(c => c.Nome.Equals("ResumoAprovacoes_PropostasAlcadaAprovacao"));
            if (resumoAprovacoes_PropostasAlcadaAprovacao != null)
            {
                var fluxoAlcadaAprovacao = await _unitOfWork.FluxoAlcadaAprovacaoRepository.GetAllFilterAsync(c => c.EmpresaID == empresaID && c.Ativo);
                if (fluxoAlcadaAprovacao.Count() > 0)
                {
                    var fluxoAlcadaAprovacaoDescricoes = fluxoAlcadaAprovacao
                        .OrderBy(c => c.Nivel)
                        .Select(c => c.Perfil?.Descricao)
                        .Distinct();

                    var descricaoPadraoResumo = ObterDescricao(resumoAprovacoes_PropostasAlcadaAprovacao.Descricao);
                    var propostasAlcadaAprovacao = ObterResumoAnaliseAprovacao(propostasLC, new List<string> { "FC" }, descricaoPadraoResumo, false, true);

                    foreach (var fluxoAlcadaAprovacaoDescricao in fluxoAlcadaAprovacaoDescricoes)
                    {
                        var propostasDaAlcada = propostasAlcadaAprovacao.PropostasLC.Where(c => c.AlcadaAprovacao == fluxoAlcadaAprovacaoDescricao);

                        resumoAnaliseAprovacaoDtos.Add(new ResumoAnaliseAprovacaoDto()
                        {
                            Descricao = $"{descricaoPadraoResumo}: {fluxoAlcadaAprovacaoDescricao}",
                            PropostasLC = propostasDaAlcada,
                            Totalizador = propostasDaAlcada.Count()
                        });
                    }
                }
            }

            // TODO: Ordenar

            return resumoAnaliseAprovacaoDtos;
        }

        private ResumoAnaliseAprovacaoDto ObterResumoAnaliseAprovacao(IEnumerable<BuscaPropostaLCPorStatus> propostasLC, IEnumerable<string> propostaLCStatusIDs, string descricao, bool incluirAlcadaAnalise, bool incluirAlcadaAprovacao)
        {
            var resumoAnaliseAprovacao = new ResumoAnaliseAprovacaoDto()
            {
                Descricao = descricao,
                PropostasLC = propostasLC.Where(c => propostaLCStatusIDs.Contains(c.PropostaLCStatusID))
                .MapTo<IEnumerable<BuscaPropostaLCPorStatusDto>>(),
            };

            resumoAnaliseAprovacao.Totalizador = resumoAnaliseAprovacao.PropostasLC.Count();

            if (!incluirAlcadaAnalise)
                resumoAnaliseAprovacao.PropostasLC.ToList().ForEach(c => c.AlcadaAnalise = null);

            if (!incluirAlcadaAprovacao)
                resumoAnaliseAprovacao.PropostasLC.ToList().ForEach(c => c.AlcadaAprovacao = null);

            return resumoAnaliseAprovacao;
        }

        private async Task<IEnumerable<BuscaPropostaLCPorStatus>> BuscaPropostaLCPorStatus(string empresaID, IEnumerable<string> propostaLCStatusIDs, List<string> gcs)
        {
            var propostasLC = await _unitOfWork.PropostaLCRepository.BuscaPropostaLCPorStatus(empresaID, propostaLCStatusIDs);

            if (gcs != null && gcs.Count > 0)
                propostasLC = propostasLC.Where(p => gcs.Contains(p.GC));

            return propostasLC;
        }

        private string ObterDescricao(string descricao)
        {
            return descricao.Replace("Visualizar ", "");
        }
    }
}
