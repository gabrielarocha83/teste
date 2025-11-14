using System;

namespace Yara.Service.Serasa.Relato.Entities
{
    public class ProtestoConcentre
    {
        public int Quantidade { get; set; }
        public string Moeda { get; set; }
        public string Mensagem { get; set; }
        public DateTime? Data { get; set; }
        public decimal Valor { get; set; }
        public string Cartorio { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public decimal Total { get; set; }
    }
}