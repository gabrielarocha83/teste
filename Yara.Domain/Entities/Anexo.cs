using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class Anexo : Base
    {
        public string Descricao { get; set; }
        public bool Obrigatorio { get; set; }
        public string LayoutsProposta { get; set; }
        public int CategoriaDocumento { get; set; }
        public bool Ativo { get; set; }
    }
}
