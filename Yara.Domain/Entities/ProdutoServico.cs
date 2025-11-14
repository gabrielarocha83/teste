using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class ProdutoServico : Base
    {
        public string Nome { get; set; }
        public bool Ativo { get; set; }

        public ICollection<PropostaLCNecessidadeProduto> PropostaLCNecessidadesProdutos { get; set; }
    }
}
