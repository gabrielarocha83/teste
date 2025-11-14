using System;

namespace Yara.AppService.Dtos
{
    public class TituloComentarioDto 
    {
        public Guid ID { get; set; }

        public Guid UsuarioIDCriacao { get; set; }
        public Guid? UsuarioIDAlteracao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public string Usuario { get; set; }
        public string NumeroDocumento { get; set; }
        public string Linha { get; set; }
        public string AnoExercicio { get; set; }
        public string Empresa { get; set; }
        public string Texto { get; set; }
    }
}
