using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class StatusCobrancaDto : BaseDto
    {
        public bool Ativo { get; set; }
        public string Descricao { get; set; }
        public bool CobrancaEfetiva { get; set; }
        public bool Padrao { get; set; }
        public bool NaoCobranca { get; set; }
        public bool ContaExposicao { get; set; }
        public bool BloqueioOrdem { get; set; }
        public ICollection<TituloDto> Titulos { get; set; }

        public StatusCobrancaDto()
        {
            CobrancaEfetiva = false;
            Padrao = false;
            NaoCobranca = false;
            ContaExposicao = false;
            BloqueioOrdem = false;
        }
    }
}
