using System;


namespace Yara.AppService.Dtos
{
    public class PropostaAlcadaComercialProdutoServicoDto
    {
        public Guid ID { get; set; }
        public Guid? ProdutoServicoID { get; set; }
        public string ProdutoServicoNome { get; set; }
       
    }
}