using System;

namespace Yara.Domain.Entities
{
    public class ProdutividadeMedia : Base
    {
        public string Nome { get; set; }
        public Guid RegiaoID { get; set; }
        public virtual Regiao Regiao { get; set; }
        public bool Ativo { get; set; }
    }
}
