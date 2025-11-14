using System;

namespace Yara.Service.Serasa.Relato.Entities
{
    public class Administracao
    {
        public string Documento { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public string Nacionalidade { get; set; }
        public string EstadoCivil { get; set; }
        public DateTime? Entrada { get; set; }
        public string Mandato { get; set; }
    }
}