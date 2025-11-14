using System;

namespace Yara.Domain.Entities
{
    public class PropostaAlcadaComercialProdutoServico
    {
        public Guid ID { get; set; }
        public Guid? ProdutoServicoID { get; set; }
        public virtual ProdutoServico ProdutoServico { get; set; }
        public Guid PropostaAlcadaComercialID { get; set; }
        public PropostaAlcadaComercial PropostaAlcadaComercial { get; set; }
    }
}