using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class PermissaoDto
    {

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Processo { get; set; }
        public bool Ativo { get; set; }

        public ICollection<GrupoDto> Grupos{ get; set; }

        public PermissaoDto()
        {
           Grupos = new List<GrupoDto>();
        }

    }
}