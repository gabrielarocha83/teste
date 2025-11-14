using System;

namespace Yara.AppService.Dtos
{
    public class EstruturaPerfilUsuarioDto : BaseDto
    {
        public string CodigoSap { get; set; }
        public Guid PerfilId { get; set; }
        public Guid? UsuarioId { get; set; }

        public virtual EstruturaComercialDto EstruturaComercial { get; set; }
        public virtual PerfilDto Perfil { get; set; }
        public virtual UsuarioDto Usuario { get; set; }
    }
}
