using System;
using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class BuscaCTCPerfilUsuarioDto
    {
        public string CodCTC { get; set; }
        public string CTC { get; set; }
        public string GC { get; set; }
        public string DI { get; set; }
        public List<EstruturaPerfilUsuarioDto> Perfis { get; set; }
    }
}