using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class PropostaLcTodasReceitasDto
    {
        public List<PropostaLCCulturaDto> Cultura { get; set; }
        public List<PropostaLCPecuariaDto> Pecuaria { get; set; }
        public List<PropostaLCOutraReceitaDto> OutraReceita { get; set; }
        public List<PropostaLCTipoEndividamentoDto> TipoEndividamento { get; set; }

        public PropostaLcTodasReceitasDto()
        {
            Cultura = new List<PropostaLCCulturaDto>();
            Pecuaria = new List<PropostaLCPecuariaDto>();
            OutraReceita = new List<PropostaLCOutraReceitaDto>();
            TipoEndividamento = new List<PropostaLCTipoEndividamentoDto>();
        }
    }
}
