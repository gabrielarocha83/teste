using System;
using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class GrupoDto:BaseDto
    {
       
        public String Nome { get; set; }
        public Boolean Ativo { get; set; }
        public bool IsProcesso { get; set; }
        //public virtual ICollection<UsuarioDto> Usuarios { get; set; }
        public virtual ICollection<PermissaoDto> Permissoes { get; set; }
        public GrupoDto()
        {
           // Usuarios = new List<UsuarioDto>();
            Permissoes = new List<PermissaoDto>();
        }
    }
}