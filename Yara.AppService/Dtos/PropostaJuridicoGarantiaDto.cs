using System;

namespace Yara.AppService.Dtos
{
    public class PropostaJuridicoGarantiaDto : BaseDto
    {
        public Guid PropostaJuridicoID { get; set; }
        public Guid TipoGarantiaID { get; set; }
        public string Nome { get; set; }

        public TipoGarantiaDto TipoGarantia { get; set; }
        public PropostaJuridicoDto PropostaJuridico { get; set; }
    }
}
