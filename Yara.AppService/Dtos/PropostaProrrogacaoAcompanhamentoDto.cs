using System;


namespace Yara.AppService.Dtos
{
    public class PropostaProrrogacaoAcompanhamentoDto
    {
        public PropostaProrrogacaoDto PropostaProrrogacao { get; set; }
        public Guid PropostaProrrogacaoID { get; set; }
        public UsuarioDto Usuario { get; set; }
        public Guid UsuarioID { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}
