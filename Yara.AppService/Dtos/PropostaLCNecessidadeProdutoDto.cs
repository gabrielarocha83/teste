using System;
using Newtonsoft.Json;

namespace Yara.AppService.Dtos
{
    public class PropostaLCNecessidadeProdutoDto
    {

        public Guid ID { get; set; }
        public Guid PropostaLCID { get; set; }
        public Guid? ProdutoServicoID { get; set; }
        public decimal? Quantidade { get; set; }

        [JsonIgnore]
        public PropostaLCDto PropostaLC { get; set; }

        public virtual ProdutoServicoDto ProdutoServico { get; set; }

        public PropostaLCNecessidadeProdutoDto()
        {
            ProdutoServico = new ProdutoServicoDto();
            
        }
    }
}