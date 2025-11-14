using System;
using System.Collections.Generic;
using System.Data.Entity;
using Yara.Data.Entity.Context;
using Yara.Data.Entity.Repository;
using Yara.Data.Entity.Repository.Procedures;
using Yara.Domain.Repository;
using Yara.Domain.Repository.Procedures;

namespace Yara.Data.Entity.IoC
{
    public static class Resolver
    {

        public static Dictionary<Type, Type> GetTypes()
        {
            var ioc = new Dictionary<Type, Type>
            {
                { typeof(DbContext), typeof(YaraDataContext) },
                { typeof(IRepositoryLog), typeof(RepositoryLog)},
                { typeof(IRepositoryGrupo), typeof(RepositoryGrupo)},
                { typeof(IRepositoryPermissao), typeof(RepositoryPermissao)},
                { typeof(IRepositoryContaClienteTelefone), typeof(RepositoryContaClienteTelefone) },
                { typeof(IRepositoryContaClienteCodigo), typeof(RepositoryContaClienteCodigo) },
                { typeof(IRepositoryUsuario), typeof(RepositoryUsuario)},
                { typeof(IRepositorySegmento), typeof(RepositorySegmento) },
                { typeof(IRepositoryTipoCliente), typeof(RepositoryTipoCliente) },
                { typeof(IRepositoryContaClienteComentario), typeof(RepositoryContaClienteComentario) },
                { typeof(IRepositoryContaCliente), typeof(RepositoryContaCliente) },
                { typeof(IRepositoryContaClienteFinanceiro), typeof(RepositoryContaClienteFinanceiro) },
                { typeof(IRepositoryConceitoCobranca), typeof(RepositoryConceitoCobranca) },
                { typeof(IRepositoryBuscaContaCliente), typeof(RepositoryBuscaContaCliente) },
                { typeof(IRepositoryExperiencia), typeof(RepositoryExperiencia) },
                { typeof(IRepositoryCultura), typeof(RepositoryCultura) },
                { typeof(IRepositoryTipoEmpresa), typeof(RepositoryTipoEmpresa) },
                { typeof(IRepositoryTipoGarantia), typeof(RepositoryTipoGarantia) },
                { typeof(IRepositoryAreaIrrigada), typeof(RepositoryAreaIrrigada) },
                { typeof(IRepositoryProdutoServico), typeof(RepositoryProdutoServico) },
                { typeof(IRepositoryIdadeMediaCanavial), typeof(RepositoryIdadeMediaCanavial) },
                { typeof(IRepositoryPorcentagemQuebra), typeof(RepositoryPorcentagemQuebra) },
                { typeof(IRepositoryProdutividadeMedia), typeof(RepositoryProdutividadeMedia) },
                { typeof(IRepositoryMediaSaca), typeof(RepositoryMediaSaca) },
                { typeof(IRepositoryRegiao), typeof(RepositoryRegiao) },
                { typeof(IRepositoryCustoHaRegiao), typeof(RepositoryCustoHaRegiao) },
                { typeof(IRepositoryTipoPecuaria), typeof(RepositoryTipoPecuaria) },
                { typeof(IRepositoryFeriado), typeof(RepositoryFeriado) },
                { typeof(IRepositoryEstruturaComercial), typeof(RepositoryEstruturaComercial) },
                { typeof(IRepositoryEstruturaComercialPapel), typeof(RepositoryEstruturaComercialPapel) },
                { typeof(IRepositoryParametroSistema), typeof(RepositoryParametroSistema) },
                { typeof(IRepositoryTipoEndividamento), typeof(RepositoryTipoEndividamento) },
                { typeof(IRepositoryAnexo), typeof(RepositoryAnexo) },
                { typeof(IRepositoryFluxoLiberacaoManual), typeof(RepositoryFluxoLiberacaoManual)},
                { typeof(IRepositoryFluxoLiberacaoOrdemVenda), typeof(RepositoryFluxoLiberacaoOrdemVenda)},
                { typeof(IRepositoryFerias), typeof(RepositoryFerias)},
                { typeof(IRepositoryOrdemVenda), typeof(RepositoryOrdemVenda) },
                { typeof(IRepositoryCidade), typeof(RepositoryCidade) },
                { typeof(IRepositoryEstado), typeof(RepositoryEstado) },
                { typeof(IRepositoryPropostaLC), typeof(RepositoryPropostaLC) },
                { typeof(IRepositoryOrdemVendaFluxo), typeof(RepositoryOrdemVendaFluxo) },
                { typeof(IRepositoryDivisaoRemessa), typeof(RepositoryDivisaoRemessa) },
                { typeof(IRepositoryAnexoArquivo), typeof(RepositoryAnexoArquivo) },
                { typeof(IRepositoryHistoricoContaCliente), typeof(RepositoryHistoricoContaCliente) },
                { typeof(IRepositoryLogDivisaoRemessaLiberacao), typeof(RepositoryLogDivisaoRemessaLiberacao) },
                { typeof(IRepositoryContaClienteEstruturaComercial), typeof(RepositoryContaClienteEstruturaComercial) },
                { typeof(IRepositoryContaClienteRepresentante), typeof(RepositoryContaClienteRepresentante) },
                { typeof(IRepositoryRepresentante), typeof(RepositoryRepresentante) },
                { typeof(IRepositoryTipoRelacaoGrupoEconomico), typeof(RepositoryTipoRelacaoGrupoEconomico) },
                { typeof(IRepositoryClassificacaoGrupoEconomico), typeof(RepositoryClassificacaoGrupoEconomico) },
                { typeof(IRepositoryFluxoGrupoEconomico), typeof(RepositoryFluxoGrupoEconomico) },
                { typeof(IRepositoryLiberacaoGrupoEconomicoFluxo), typeof(RepositoryLiberacaoGrupoEconomicoFluxo) },
                { typeof(IRepositoryGrupoEconomicoMembro), typeof(RepositoryGrupoEconomicoMembro) },
                { typeof(IRepositoryCulturaEstado), typeof(RepositoryCulturaEstado) },
                { typeof(IRepositoryEstruturaPerfilUsuario), typeof(RepositoryEstruturaPerfilUsuario) },
                { typeof(IRepositoryPerfil), typeof(RepositoryPerfil) },
                { typeof(IRepositoryBuscaGrupoCookpit), typeof(RepositoryBuscaGrupoCookpit) },
                { typeof(IRepositorySolicitanteSerasa), typeof(RepositorySolicitanteSerasa) },
                { typeof(IRepositoryPendenciaSerasa), typeof(RepositoryPendenciaSerasa) },
                { typeof(IRepositoryFluxoLiberacaoLimiteCredito), typeof(RepositoryFluxoLiberacaoLimiteCredito) },
                { typeof(IRepositoryPropostaLCHistorico), typeof(RepositoryPropostaLCHistorico) },
                { typeof(IRepositoryBlog), typeof(RepositoryBlog) },
                { typeof(IRepositoryPropostaLCAcompanhamento), typeof(RepositoryPropostaLCAcompanhamento) },
                { typeof(IRepositoryPropostaLCComite), typeof(RepositoryPropostaLCComite) },
                { typeof(IRepositoryPropostaLCStatusComite), typeof(RepositoryPropostaLCStatusComite) },
                { typeof(IRepositoryMotivoAbono), typeof(RepositoryMotivoAbono) },
                { typeof(IRepositoryPropostaLCComiteSolicitante), typeof(RepositoryPropostaLCComiteSolicitante) },
                { typeof(IRepositoryPropostaLCGrupoEconomico), typeof(RepositoryPropostaLCGrupoEconomico) },
                { typeof(IRepositoryFluxoLiberacaoProrrogacao), typeof(RepositoryFluxoLiberacaoProrrogacao) },
                { typeof(IRepositoryContaClienteVisita), typeof(RepositoryContaClienteVisita) },
                { typeof(IRepositoryContaClienteBuscaBens), typeof(RepositoryContaClienteBuscaBens) },
                { typeof(IRepositoryTitulo), typeof(RepositoryTitulo) },
                { typeof(IRepositoryTituloComentario), typeof(RepositoryTituloComentario) },
                { typeof(IRepositoryPropostaJuridico), typeof(RepositoryPropostaJuridico) },
                { typeof(IRepositoryPropostaJuridicoHistoricoPagamento), typeof(RepositoryPropostaJuridicoHistoricoPagamento) },
                { typeof(IRepositoryPropostaAbono), typeof(RepositoryPropostaAbono) },
                { typeof(IRepositoryPropostaAbonoTitulo), typeof(RepositoryPropostaAbonoTitulo) },
                { typeof(IRepositoryPropostaJuridicoTitulo), typeof(RepositoryPropostaJuridicoTitulo) },
                { typeof(IRepositoryAnexoArquivoCobranca), typeof(RepositoryAnexoArquivoCobranca) },
                { typeof(IRepositoryContaClienteGarantia), typeof(RepositoryContaClienteGarantia) },
                { typeof(IRepositoryContaClienteParticipanteGarantia), typeof(RepositoryContaClienteParticipanteGarantia) },
                { typeof(IRepositoryContaClienteResponsavelGarantia), typeof(RepositoryContaClienteResponsavelGarantia) },
                { typeof(IRepositoryPropostaAbonoAcompanhamento), typeof(RepositoryPropostaAbonoAcompanhamento) },
                { typeof(IRepositoryPropostaProrrogacao), typeof(RepositoryPropostaProrrogacao) },
                { typeof(IRepositoryPropostaProrrogacaoTitulo), typeof(RepositoryPropostaProrrogacaoTitulo) },
                { typeof(IRepositoryPropostaProrrogacaoComite), typeof(RepositoryPropostaProrrogacaoComite) },
                { typeof(IRepositoryPropostaProrrogacaoAcompanhamento), typeof(RepositoryPropostaProrrogacaoAcompanhamento) },
                { typeof(IRepositoryPropostaProrrogacaoParcelamento), typeof(RepositoryPropostaProrrogacaoParcelamento) },
                { typeof(IRepositoryPropostaAlcadaComercial), typeof(RepositoryPropostaAlcadaComercial) },
                { typeof(IRepositoryPropostaAlcadaComercialCultura), typeof(RepositoryPropostaAlcadaComercialCultura) },
                { typeof(IRepositoryPropostaAlcadaComercialRestricao), typeof(RepositoryPropostaAlcadaComercialRestricao) },
                { typeof(IRepositoryPropostaAlcadaComercialParceriaAgricola), typeof(RepositoryPropostaAlcadaComercialParceriaAgricola) },
                { typeof(IRepositoryPropostaAlcadaComercialProdutoServico), typeof(RepositoryPropostaAlcadaComercialProdutoServico) },
                { typeof(IRepositoryPropostaAlcadaComercialAcompanhamento), typeof(RepositoryPropostaAlcadaComercialAcompanhamento) },
                { typeof(IRepositoryRelatorios), typeof(RepositoryRelatorios) },
                { typeof(IRepositoryFluxoRenovacaoVigenciaLC), typeof(RepositoryFluxoRenovacaoVigenciaLC) },
                { typeof(IRepositoryPropostaRenovacaoVigenciaLC), typeof(RepositoryPropostaRenovacaoVigenciaLC) },
                { typeof(IRepositoryPropostaRenovacaoVigenciaLCComite), typeof(RepositoryPropostaRenovacaoVigenciaLCComite) },
                { typeof(IRepositoryFluxoAlcadaAnalise), typeof(RepositoryFluxoAlcadaAnalise) },
                { typeof(IRepositoryFluxoAlcadaAprovacao), typeof(RepositoryFluxoAlcadaAprovacao) },
                { typeof(IRepositoryTribunalJustica), typeof(RepositoryTribunalJustica) },
                { typeof(IRepositoryNotificacoes), typeof(RepositoryNotificacoes) },
                { typeof(IUnitOfWork), typeof(UnitOfWork) }
            };

            return ioc;
        }

    }
}