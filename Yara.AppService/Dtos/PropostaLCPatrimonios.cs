using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class PropostaLCPatrimoniosDto
    {
        public decimal PotencialPatrimonial { get; set; }
        public decimal PotencialReceita { get; set; }

        public List<PropostaLCBemRuralDto> BemRural { get; set; }
        public List<PropostaLCBemUrbanoDto> BemUrbano { get; set; }
        public List<PropostaLCMaquinaEquipamentoDto> MaquinasEquipamentos { get; set; }
        public List<PropostaLCPrecoPorRegiaoDto> PrecosPorRegiao { get; set; }

        public string Nome { get; set; }
        //public TYPE Type { get; set; }

        public PropostaLCPatrimoniosDto()
        {
            BemRural = new List<PropostaLCBemRuralDto>();
            BemUrbano = new List<PropostaLCBemUrbanoDto>();
            MaquinasEquipamentos = new List<PropostaLCMaquinaEquipamentoDto>();
            PrecosPorRegiao = new List<PropostaLCPrecoPorRegiaoDto>();
        }
    }
}