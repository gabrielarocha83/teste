using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class ResumoAnaliseAprovacaoDto
    {
        public string Descricao { get; set; }
        public decimal Totalizador { get; set; }
        public IEnumerable<BuscaPropostaLCPorStatusDto> PropostasLC { get; set; }
    }
}
