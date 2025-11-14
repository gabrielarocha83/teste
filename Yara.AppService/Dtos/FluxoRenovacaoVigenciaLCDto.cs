using System;

namespace Yara.AppService.Dtos
{
    public class FluxoRenovacaoVigenciaLCDto : BaseDto
    {

        public int Nivel { get; set; }
        public bool Ativo { get; set; }
        public string EmpresaID { get; set; }
        public Guid UsuarioId { get; set; }

        // Nav Properties
        public virtual UsuarioDto Usuario { get; set; }

    }
}