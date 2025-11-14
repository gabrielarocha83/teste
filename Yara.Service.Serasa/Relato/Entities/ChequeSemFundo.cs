using System;

namespace Yara.Service.Serasa.Relato.Entities
{
    public class ChequeSemFundo
    {
        public int Quantidade { get; set; }
        public DateTime? Data { get; set; }
        public string Numero { get; set; }
        public string Aliena { get; set; }
        public int QuantidadeBanco { get; set; }
        public string Moeda { get; set; }
        public decimal Valor { get; set; }
        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Natureza { get; set; }
        public Decimal Total => Quantidade * Valor;
    }
}