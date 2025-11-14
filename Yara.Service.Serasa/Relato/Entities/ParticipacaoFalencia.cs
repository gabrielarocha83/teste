using System;

namespace Yara.Service.Serasa.Relato.Entities
{
    public class ParticipacaoFalencia
    {
        public int Quantidade { get; set; }
        public string Data { get; set; }
        public string Tipo { get; set; }
        public string CNPJ { get; set; }
        public string Empresa { get; set; }
        public string Natureza { get; set; }

    }
}