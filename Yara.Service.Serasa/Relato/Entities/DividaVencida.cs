using System;

namespace Yara.Service.Serasa.Relato.Entities
{
    public class DividaVencida
    {
        public int Quantidade { get; set; }
        public DateTime? Data { get; set; }
        public string Modalidade { get; set; }
        public string Moeda { get; set; }
        public decimal Valor { get; set; }
        public string Titulo { get; set; }
        public string Instituicao { get; set; }
        public string Local { get; set; }
        public string  Natureza { get; set; }
        public decimal Total { get; set; }
    }
}