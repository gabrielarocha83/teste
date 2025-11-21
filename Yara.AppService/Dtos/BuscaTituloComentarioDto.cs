using System;

namespace Yara.AppService.Dtos
{
    public class BuscaTituloComentarioDto
    {       
        public string NumeroDocumento { get; set; }
        public string Linha { get; set; }
        public string AnoExercicio { get; set; }
        public string Empresa { get; set; }
        public string Texto { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Usuario { get; set; }
    }
}
