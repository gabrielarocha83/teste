using System;

namespace Yara.Service.Serasa.Relato.Entities
{
    public class ChequeCCF
    {
        public int Quantidade { get; set; }
        public DateTime? Data { get; set; }
        public string Numero { get; set; }
        public int QuantidadeBanco { get; set; }
        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Natureza { get; set; }

    }
}