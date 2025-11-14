using System;

namespace Yara.Service.Serasa.Concentre.Entities
{
    public class ChequeAcheiDetail
    {
        public DateTime DataOcorrencia { get; set; }
        public int Banco { get; set; }
        public int Agencia { get; set; }
        public int ContaCorrente { get; set; }
        public string Natureza { get; set; }
        public decimal Valor { get; set; }
        public string Praca { get; set; }
        public string Uf { get; set; }
        public string NomeBanco { get; set; }
        public string NumeroCheque { get; set; }
        public string Cidade { get; set; }

        public decimal Total => Valor;
    }
}
