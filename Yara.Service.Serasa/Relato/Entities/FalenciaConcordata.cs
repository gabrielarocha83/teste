using System;

namespace Yara.Service.Serasa.Relato.Entities
{
    public class FalenciaConcordata
    {
        public int Quantidade { get; set; }
        public DateTime? Data { get; set; }
        public string Tipo { get; set; }
        public string Origem { get; set; }
        public string Vara { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Natureza { get; set; }
    }
}