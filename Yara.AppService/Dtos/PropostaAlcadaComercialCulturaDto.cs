using System;

namespace Yara.AppService.Dtos
{
    public class PropostaAlcadaComercialCulturaDto
    {
        public Guid ID { get; set; }
        public Guid? CulturaID { get; set; }
        public string CulturaNome { get; set; }
        public Guid? EstadoID { get; set; }
        public string EstadoNome { get; set; }
        public Guid? CidadeID { get; set; }
        public string CidadeNome { get; set; }
        public string Documento { get; set; }
        public decimal? AreaPropria { get; set; }
        public decimal? AreaArrendada { get; set; }
    }
}