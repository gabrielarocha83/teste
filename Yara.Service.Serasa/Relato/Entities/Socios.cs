using System;

namespace Yara.Service.Serasa.Relato.Entities
{
    public class Socios
    {
        public string Documento { get; set; }
        public string SocioAcionista { get; set; }
        public DateTime? Entrada { get; set; }
        public string Nacionalidade { get; set; }
        public float Votante { get; set; }
        public float Total { get; set; }
    }
}