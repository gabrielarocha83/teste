using System;

namespace Yara.AppService.Dtos
{
    public class PropostaLCValidacaoDto
    {
        public Guid? PropostaLCID { get; set; }
        public Guid? UsuarioID { get; set; }
        public decimal? LcProposto { get; set; }
        public string Mensagem { get; set; }
    }
}