using System;

namespace Yara.AppService.Dtos
{
    public class StatusGrupoEconomicoFluxoDto
    {
        public string ID { get; set; }
        public Guid UsuarioIDCriacao { get; set; }
        public Guid? UsuarioIDAlteracao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}