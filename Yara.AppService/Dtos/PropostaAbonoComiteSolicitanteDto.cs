using System;


namespace Yara.AppService.Dtos
{
    public class PropostaAbonoComiteSolicitanteDto
    {
        public Guid ID { get; set; }
        public Guid UsuarioID { get; set; }
        public virtual UsuarioDto Usuario { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}