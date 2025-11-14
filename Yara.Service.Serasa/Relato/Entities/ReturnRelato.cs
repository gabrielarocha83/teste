using System;
using System.Collections.Generic;

namespace Yara.Service.Serasa.Relato.Entities
{
    public class ReturnRelato
    {
        // Props
        public string CNPJ { get; set; }
        public string Situacao { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? AtualizacaoParticipacoes { get; set; }
        public DateTime? AtualizacaoAdministracao { get; set; }
        public string HoraCriacao { get; set; }
        public DateTime DataSituacao { get; set; }
        public DateTime? DataControleSocietario { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string TipoSociedade { get; set; }
        public string Site { get; set; }
        public string Registro { get; set; }
        public DateTime? DataRegistro { get; set; }
        public string QuantidadeFiliais { get; set; }
        public string QuantidadeFuncionarios { get; set; }
        public string NIRE { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public string Telefone { get; set; }
        public string Fax { get; set; }
        public DateTime? Fundacao { get; set; }
        public string Filiais { get; set; }
        public string Ramo { get; set; }
        public string CodSerasa { get; set; }
        public string CNAE { get; set; }
        public string OpcaoTributaria { get; set; }
        public string PendenciasFinanceiras { get; set; }
        public string Concentre { get; set; }
        public string ReCheque { get; set; }
        public string Antecessora { get; set; }
        public string PendenciasReCheque { get; set; }
        public DateTime? DataFimAntecessora { get; set; }
        public Pefin Pefin { get; set; }
        public Refin Refin { get; set; }
        public Historico HistoricoPagamento { get; set; }
        public ControleSocietario ControleSocietario { get; set; }

        // Lists
        public List<Socios> Socios { get; set; }
        public List<Administracao> Administracao { get; set; }
        public List<Participacao> Participacao { get; set; }
        public List<ParticipanteAnotacao> ParticipantesAnotacoes { get; set; }
        public List<Grafia> ConcentreGrafias { get; set; }
        public List<ConcentreResumo> ConcentreResumos { get; set; }

        public List<ProtestoConcentre> ProtestosConcentre { get; set; }
        public List<AcaoJudicial> AcoesJudiciais { get; set; }
        public List<ParticipacaoFalencia> ParticipacoesFalencia { get; set; }
        public List<FalenciaConcordata> FalenciaConcordatas { get; set; }
        public List<DividaVencida> DividasVencidas { get; set; }
        public List<ChequeSemFundo> ChequesSemFundo { get; set; }
        public List<ChequeCCF> ChequesCCF { get; set; }

        public ReturnRelato()
        {
            ControleSocietario = new ControleSocietario();

            Socios = new List<Socios>();
            Administracao = new List<Administracao>();
            Participacao = new List<Participacao>();
            ParticipantesAnotacoes = new List<ParticipanteAnotacao>();
            ConcentreGrafias = new List<Grafia>();
            ConcentreResumos = new List<ConcentreResumo>();

            ProtestosConcentre = new List<ProtestoConcentre>();
            AcoesJudiciais = new List<AcaoJudicial>();
            ParticipacoesFalencia = new List<ParticipacaoFalencia>();
            FalenciaConcordatas = new List<FalenciaConcordata>();
            DividasVencidas = new List<DividaVencida>();
            ChequesSemFundo = new List<ChequeSemFundo>();
            ChequesCCF = new List<ChequeCCF>();
        }

    }

    public class Pefin
    {
        public int Quantidade { get; set; }
        public decimal Total { get; set; }
        public List<PendenciaFinanceira> PendenciaPefin { get; set; }

        public Pefin()
        {
            PendenciaPefin = new List<PendenciaFinanceira>();
        }
    }

    public class Refin
    {
        public int Quantidade { get; set; }
        public decimal Total { get; set; }
        public List<PendenciaFinanceira> PendenciaRefin { get; set; }

        public Refin()
        {
            PendenciaRefin = new List<PendenciaFinanceira>();
        }
    }

    public class Historico
    {
        public int Fontes { get; set; }
        public List<Titulos> Titulos { get; set; }

        public Historico()
        {
            Titulos = new List<Titulos>();
        }
    }

    public class Titulos
    {
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public int Percentual { get; set; }
    }
}