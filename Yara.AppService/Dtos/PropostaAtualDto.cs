using System;

namespace Yara.AppService.Dtos
{
    public class PropostaAtualDto
    {
        public Guid? PropostaLCID { get; set; }
        public string PropostaLCStatus { get; set; }
        public Guid? PropostaAlcadaID { get; set; }
        public string PropostaAlcadaStatus { get; set; }
        public Guid? PropostaLCAdicionalID { get; set; }
        public string PropostaLCAdicionalStatus { get; set; }
    }
}
