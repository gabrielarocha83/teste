using System;

namespace Yara.Domain.Entities
{
    public class PropostaLCNecessidadeProduto
    {

        public Guid ID { get; set; }
        public Guid PropostaLCID { get; set; }
        public Guid? ProdutoServicoID { get; set; }
        public decimal? Quantidade { get; set; }

        public PropostaLC PropostaLC { get; set; }
        public virtual ProdutoServico ProdutoServico { get; set; }
    }
}