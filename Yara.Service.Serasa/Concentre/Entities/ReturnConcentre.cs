using System;
using System.Collections.Generic;
using Yara.Service.Serasa.Relato.Entities;

namespace Yara.Service.Serasa.Concentre.Entities
{
    public class ReturnConcentre
    {
        public string NomeCliente { get; set; }
        public DateTime? DataConfirmacao { get; set; }
        public string Situacao { get; set; }
        public DateTime? DataSituacao { get; set; }
        public string NomeMae { get; set; }
        public DateTime? DataCriacao { get; set; }
        public string DocumentoCliente { get; set; }
        public List<ConcentreResumo> Resumos { get; set; }
        public List<PefinDetail> Pefin { get; set; }
        public List<RefinDetail> Refin { get; set; }
        public List<RefinDetail2> RefinDetail2 { get; set; }
        public List<ProtestosDetail> Protestos { get; set; }
        public List<ChequeAcheiDetail> ChequesAchei { get; set; }
        public List<ChequeSemFundoDetail> ChequesSemFundos { get; set; }
        public List<AcaoJudicialDetail> Acoes  { get; set; }
        public List<FalenciaDetail> Falencia { get; set; }
        public List<ParticipacaoSocietaria> ParticipacaoSocietarias { get; set; }

        public ReturnConcentre()
        {
            Resumos = new List<ConcentreResumo>();
            Pefin = new List<PefinDetail>();
            Refin = new List<RefinDetail>();
            RefinDetail2 = new List<RefinDetail2>();
            Falencia = new List<FalenciaDetail>();
            Protestos = new List<ProtestosDetail>();
            ChequesAchei = new List<ChequeAcheiDetail>();
            Acoes = new List<AcaoJudicialDetail>();
            ChequesSemFundos = new List<ChequeSemFundoDetail>();
            ParticipacaoSocietarias = new List<ParticipacaoSocietaria>();
        }
    }
}
