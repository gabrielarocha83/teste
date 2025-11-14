using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class AnexoDto : BaseDto
    {
        public string Descricao { get; set; }
        public bool Obrigatorio { get; set; }
        public string LayoutsProposta { get; set; }
        public int CategoriaDocumento { get; set; }
        public bool Ativo { get; set; }

    }
}
