using System.Security.AccessControl;
using Yara.AppService.Dtos;

namespace Yara.AppService.Dtos
{
    public class TipoPecuariaDto : BaseDto
    {
        public string Tipo { get; set; }
        public string UnidadeMedida { get; set; }
        public bool Ativo { get; set; }
        
    }
}