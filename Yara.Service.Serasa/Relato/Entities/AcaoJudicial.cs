using System;

namespace Yara.Service.Serasa.Relato.Entities
{
    public class AcaoJudicial
    {
        public int Quantidade { get; set; }
        public DateTime? Data { get; set; }
        public string Natureza { get; set; }
        public string Avalista { get; set; }
        public string Moeda { get; set; }
        public decimal Valor { get; set; }
        public string Distrito { get; set; }
        public string Vara { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Mensagem { get; set; }
        public decimal Total { get; set; }
    }
}